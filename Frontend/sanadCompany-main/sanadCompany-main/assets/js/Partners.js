document.addEventListener("DOMContentLoaded", () => {
  fetch("http://localhost:5261/api/partners")
    .then(res => res.json())
    .then(partners => {
      const carouselInner = document.querySelector("#partnersCarousel .carousel-inner");
      const indicators = document.querySelector(".carousel-indicators");

   
      for (let i = 0; i < partners.length; i += 3) {
        const chunk = partners.slice(i, i + 3);

        const isActive = i === 0 ? "active" : "";
        const slide = document.createElement("div");
        slide.className = `carousel-item ${isActive}`;
        slide.innerHTML = `
          <div class="row mx-5">
            ${chunk.map(p => `
              <div class="col-4">
                <img src="${p.logoUrl}" class="img-fluid" alt="${p.name}">
              </div>
            `).join("")}
          </div>
        `;
        carouselInner.appendChild(slide);

        const button = document.createElement("button");
        button.type = "button";
        button.setAttribute("data-bs-target", "#partnersCarousel");
        button.setAttribute("data-bs-slide-to", i / 3);
        button.className = isActive ? "active" : "";
        button.setAttribute("aria-label", `Slide ${i / 3 + 1}`);
        if (isActive) button.setAttribute("aria-current", "true");

        indicators.appendChild(button);
      }
    })
    .catch(err => console.error("Error loading partners:", err));
});
