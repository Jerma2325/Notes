const loginButton = document.querySelector("#login-button");
const logoutButton = document.querySelector("#logout-button");
const usernameDisplay = document.querySelector("#username-display");

loginButton.addEventListener("click", () => {
  const loginForm = document.createElement("form");
  loginForm.innerHTML = `
    <label for="username">Nazwa użytkownika:</label>
    <input type="text" id="username" name="username">
    <br>
    <label for="password">Hasło:</label>
    <input type="password" id="password" name="password">
    <br>
    <input type="submit" value="Zaloguj">
    <input type="button" value="Anuluj" id="cancel-button">
  `;
  document.body.appendChild(loginForm);

  const cancelButton = loginForm.querySelector("#cancel-button");
  cancelButton.addEventListener("click", () => {
    document.body.removeChild(loginForm);
  });

  loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    const username = loginForm.querySelector("#username").value;
    const password = loginForm.querySelector("#password").value;

    const response = await fetch("https://localhost:7202/api/Auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        userName: username,
        password: password
      })
    });

    if (response.ok) {
      const jwt = await response.text();
      localStorage.setItem("jwt", jwt);
      document.body.removeChild(loginForm);
      loginButton.style.display = "none";
      logoutButton.style.display = "inline-block";
      usernameDisplay.textContent = username;
    } else {
      const errorMessage = document.createElement("p");
      errorMessage.textContent = "Błędne dane";
      loginForm.appendChild(errorMessage);
    }
  });
});

logoutButton.addEventListener("click", () => {
  localStorage.removeItem("jwt");
  loginButton.style.display = "inline-block";
  logoutButton.style.display = "none";
  usernameDisplay.textContent = "";
});

if (localStorage.getItem("jwt")) {
  loginButton.style.display = "none";
  logoutButton.style.display = "inline-block";
}
