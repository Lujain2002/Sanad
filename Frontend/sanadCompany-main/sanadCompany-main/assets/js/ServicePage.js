
  async function loadServices() {
    try {
      const res = await fetch('http://localhost:5261/api/Services');
      const services = await res.json();

      const container = document.getElementById('services-section');

      services.forEach(service => {
        
        const h5 = document.createElement('h5');
        h5.className = 'text-white mt-4';
        h5.textContent = service.title;
        container.appendChild(h5);

      
        if (service.details && service.details.length > 0) {
          const ul = document.createElement('ul');
          ul.className = 'text-white-50';

          service.details.forEach(detail => {
            const li = document.createElement('li');
            li.innerText = detail;
            ul.appendChild(li);
          });

          container.appendChild(ul);
        }
      });

      const finalText = document.createElement('p');
      finalText.className = 'text-white-50 mt-4';
      finalText.innerHTML = ` At Sanad Company, we are not just a service provider — we are an active strategic partner in leading your digital success and growth powered by AI. Whether you are starting out or expanding, we offer smart solutions that grow with your business.`;
      container.appendChild(finalText);

    } catch (error) {
      console.error('Failed to load services:', error);
    }
  }

  
  document.addEventListener('DOMContentLoaded', loadServices);

