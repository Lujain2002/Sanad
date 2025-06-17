document.addEventListener("DOMContentLoaded", () => {

  loadProducts("http://localhost:5261/api/products", "all-products");


  loadYears();
  loadCategories();
});

function loadYears() {
  fetch("http://localhost:5261/api/productyears")
    .then(res => res.json())
    .then(years => {
      years.forEach(year => {
        const id = `y${year.yearValue}`;
        console.log(year.yearValue);
        addTabAndContent(`tab-${id}`, id, year.yearValue, `products-${id}`);
        loadProducts(`http://localhost:5261/api/products?year=${year.yearValue}`, `products-${id}`);
      });
    })
    .catch(err => console.error("Error loading years:", err));
}

function loadCategories() {
  fetch("http://localhost:5261/api/categories")
    .then(res => res.json())
    .then(categories => {
      categories.forEach(cat => {
        const id = cat.name.toLowerCase().replace(/\s+/g, "-");
        addTabAndContent(`tab-${id}`, id, cat.name, `products-${id}`);
        loadProducts(`http://localhost:5261/api/products?categoryName=${cat.name}`, `products-${id}`);
      });
    })
    .catch(err => console.error("Error loading categories:", err));
}

function addTabAndContent(tabId, tabTargetId, label, containerId) {
  const tabList = document.getElementById("productTabs");
  const tabContent = document.getElementById("productTabsContent");

 
  const li = document.createElement("li");
  li.className = "nav-item";
  li.role = "presentation";

  li.innerHTML = `
    <button class="nav-link text-secondary" id="${tabId}" data-bs-toggle="tab" data-bs-target="#${tabTargetId}" type="button" role="tab" aria-controls="${tabTargetId}" aria-selected="false">${label}</button>
  `;
  tabList.appendChild(li);

  const div = document.createElement("div");
  div.className = "tab-pane fade";
  div.id = tabTargetId;
  div.setAttribute("role", "tabpanel");
  div.setAttribute("aria-labelledby", tabId);

  div.innerHTML = `<div class="row g-4" id="${containerId}"></div>`;
  tabContent.appendChild(div);
}

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
