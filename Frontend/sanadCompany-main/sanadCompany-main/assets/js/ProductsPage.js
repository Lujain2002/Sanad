
document.addEventListener("DOMContentLoaded", () => {
  const imageBaseUrl = "http://localhost:5261/ProductImages/";

  fetch("http://localhost:5261/api/products")
    .then(res => res.json())
    .then(products => {
      const grid = document.getElementById("productGrid");
      grid.innerHTML = "";

      products.forEach((product, index) => {
        const mainImageId = `mainImage${index + 1}`;

       
        const mainImageSrc = imageBaseUrl + product.imageUrl;

      
        const thumbnails = product.thumbnails
          ? JSON.parse(product.thumbnails)
          : [product.imageUrl];

        const thumbnailsHtml = thumbnails.map((thumb, i) => `
          <img src="${imageBaseUrl + thumb}" alt="Thumb ${i + 1}" class="${i === 0 ? 'active' : ''}" onclick="changeMainImage('${mainImageId}', this)" />
        `).join("");

        // tags
        const tags = product.tags ? product.tags.split(",") : [];
        const tagsHtml = tags.map(tag => `<span class="tag">${tag.trim()}</span>`).join("");

       
        const linksHtml = `
          ${product.buyLink ? `<a href="${product.buyLink}" target="_blank" class="btn-link">Buy Now</a>` : ""}
          ${product.detailsLink ? `<a href="${product.detailsLink}" target="_blank" class="btn-link">Details</a>` : ""}
          ${product.demoLink ? `<a href="${product.demoLink}" target="_blank" class="btn-link">Demo</a>` : ""}
        `;

       
        const productHtml = `
          <div class="product">
            <img id="${mainImageId}" class="main-image" src="${mainImageSrc}" alt="${product.title}" />
            <div class="thumbnails" id="thumbnails${index + 1}">
              ${thumbnailsHtml}
            </div>
            <div class="product-content">
              <h3>${product.title}</h3>
              <p>${product.longDescription}</p>
              <div class="tags">${tagsHtml}</div>
              <div class="product-links">${linksHtml}</div>
            </div>
          </div>
        `;

        grid.insertAdjacentHTML("beforeend", productHtml);
      });
    })
    .catch(err => {
      console.error("Error loading products:", err);
      document.getElementById("productGrid").innerHTML = "<p style='color:white;'>Failed to load products.</p>";
    });
});

