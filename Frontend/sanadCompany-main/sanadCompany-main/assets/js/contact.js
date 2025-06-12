
  document.getElementById("contactForm").addEventListener("submit", async function (e) {
    e.preventDefault(); 

   
    const name = document.getElementById("name").value;
    const userEmail = document.getElementById("email").value;
    const phone = document.getElementById("phone").value;
    const message = document.getElementById("message").value;

   
    const emailData = {
      subject: `New message from ${name}`,
      body: `Name: ${name}\nEmail: ${userEmail}\nPhone: ${phone}\n\nMessage:\n${message}`,
      recivers: "lojienbarrham@gmail.com"
    };

    try {
      const response = await fetch("http://localhost:5261/api/contact/send", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(emailData)
      });

      if (response.ok) {
        alert("✅ Message sent successfully!");
        document.getElementById("contactForm").reset();
      } else {
        const result = await response.json();
        alert("❌ Failed to send message: " + result.message);
      }
    } catch (error) {
      alert("❌ Error: " + error.message);
    }
  });

