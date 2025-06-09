//  to change the color of navbar-iten in hover
 
 const navItems = document.querySelectorAll("#navList .nav-item");

    navItems.forEach(item => {
      item.addEventListener("mouseenter", () => {
        navItems.forEach(other => {
          const link = other.querySelector("a");
          if (other === item) {
            link.classList.remove("text-white-50");
            link.classList.add("text-white");
          } else {
            link.classList.remove("text-white");
            link.classList.add("text-white-50");
          }
        });
      });

      item.addEventListener("mouseleave", () => {
        navItems.forEach(other => {
          const link = other.querySelector("a");
          link.classList.remove("text-white-50");
          link.classList.add("text-white");
        });
      });
    });


    // parallax-img
     const containers = document.querySelectorAll('.parallax-container');

window.addEventListener('scroll', () => {
  const windowHeight = window.innerHeight;

  containers.forEach(container => {
    const image = container.querySelector('.parallax-img');
    const rect = container.getBoundingClientRect();

    if (rect.top < windowHeight && rect.bottom > 0) {
      // progress من 0 إلى 1 داخل نافذة العرض
      let progress = (windowHeight - rect.top) / (windowHeight + rect.height);
      progress = Math.min(Math.max(progress, 0), 1);

      // أقصى تحرك = فرق ارتفاع الصورة و الحاوية
      const maxMove = image.clientHeight - container.clientHeight;

      // تحريك الصورة من 0 إلى -maxMove
      const translateY = -maxMove * progress;

      image.style.transform = `translateY(${translateY}px)`;
    }
  });
});



//  Custom Smooth Scroll JS 

  function slowScroll(event, targetId) {
    event.preventDefault();

    const target = document.getElementById(targetId);
    const offset = 0; // لتفادي تغطية الـ Navbar
    const targetPosition = target.getBoundingClientRect().top + window.pageYOffset - offset;
    const startPosition = window.pageYOffset;
    const distance = targetPosition - startPosition;
    const duration = 1200; // مدة التمرير (أبطأ = أكبر)
    let startTime = null;

    function animation(currentTime) {
      if (startTime === null) startTime = currentTime;
      const timeElapsed = currentTime - startTime;
      const run = easeOutQuad(timeElapsed, startPosition, distance, duration);
      window.scrollTo(0, run);
      if (timeElapsed < duration) requestAnimationFrame(animation);
    }

   function easeOutQuad(t, b, c, d) {
  t /= d;
  return -c * t * (t - 2) + b;
}


    requestAnimationFrame(animation);
  }



  // to change the photo in prodact
    function changeMainImage(mainImageId, thumbnail) {
    const mainImage = document.getElementById(mainImageId);
    mainImage.src = thumbnail.src;
    mainImage.alt = thumbnail.alt;

    const thumbnailsContainer = thumbnail.parentElement;
    [...thumbnailsContainer.children].forEach(img => img.classList.remove('active'));
    thumbnail.classList.add('active');
  }


  // to filter catgry in article

    function filterCategory(category) {
  const articles = document.querySelectorAll('article');
  articles.forEach(article => {
    const cat = article.getAttribute('data-category');
    article.style.display = (category === 'all' || cat === category) ? 'block' : 'none';
  });

  // Update active class
  document.querySelectorAll('.list-group-item').forEach(btn => {
    btn.classList.remove('active');
  });
  const btnText = category === 'all' ? 'all' : category;
  document.querySelectorAll('.list-group-item').forEach(btn => {
    if (btn.textContent.toLowerCase().includes(btnText)) {
      btn.classList.add('active');
    }
  });
}