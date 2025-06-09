document.addEventListener("DOMContentLoaded", () => {
  loadProducts("http://localhost:5261/api/products", "all-products");
  loadProducts("http://localhost:5261/api/products?year=2023", "products-2023");
  loadProducts("http://localhost:5261/api/products?year=2024", "products-2024");
  loadProducts("http://localhost:5261/api/products?category=ai", "products-ai");
  loadProducts("http://localhost:5261/api/products?category=health", "products-health");
});

function loadProducts(endpoint, containerId) {
  fetch(endpoint)
    .then(res => res.json())
    .then(products => {
      const container = document.getElementById(containerId);
      container.innerHTML = "";

      if (products.length === 0) {
        container.innerHTML = `<div class="text-white">No products found.</div>`;
        return;
      }

      products.forEach(product => {
        const card = createProductCard(product);
        container.appendChild(card);
      });
    })
    .catch(err => console.error("Error fetching products:", err));
}

function createProductCard(product) {
  const col = document.createElement("div");
  col.className = "col-md-4 product-card";
  col.innerHTML = `
    <div class="card h-100 bg-dark text-white">
      <img src="http://localhost:5261/ProductImages/${product.imageUrl}" class="card-img-top" alt="${product.title}">
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">${product.title}</h5>
        <p class="card-text">${product.description}</p>
        <a href="prodacts.html" class="btn btn-outline-light mt-auto">Read More</a>
      </div>
    </div>
  `;
  return col;
}
