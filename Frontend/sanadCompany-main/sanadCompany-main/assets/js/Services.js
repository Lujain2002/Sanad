document.addEventListener("DOMContentLoaded", () => {
  fetch("http://localhost:5261/api/Services") 
    .then(response => response.json())
    .then(services => {
      const container = document.getElementById("servicesContainer");

      services.forEach(service => {
        const col = document.createElement("div");
        col.className = "col-md-6 col-lg-4";
      
        col.innerHTML = `
          <a href="services.html" class="service-card-v3 hover-service-card d-block p-4 text-white-50 h-100 shadow-sm custom-hover text-decoration-none position-relative">
            <h3 class="heading-h3 mb-3">${service.title}</h3>
            <p>${service.description}</p>
            <img src="./assets/img/arrow.png" alt="Arrow" class="arrow" width="30" />
          </a>
        `;
        container.appendChild(col);
      });
    })
    .catch(error => {
      console.error("Error fetching services:", error);
    });
});
