using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaludTotal_AppWeb.Models;
using System.Text;

namespace SaludTotal_AppWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // URLs base para consumir la API de usuarios y admins
        private readonly string _apiBaseUrl = "https://saludtotalapirest-ame2f3hrd7h5csc4.chilecentral-01.azurewebsites.net/api/usuario";
        private readonly string _apiAdminBaseUrl = "https://saludtotalapirest-ame2f3hrd7h5csc4.chilecentral-01.azurewebsites.net/api/admin";

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // VISTAS (GET) 

        // Muestra la vista principal del panel del administrador (requiere autenticación previa)
        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }

        // Muestra el formulario para ingresar la clave del administrador
        [HttpGet]
        public IActionResult ClaveAdmin()
        {
            return View();
        }

        // Muestra el formulario de registro para usuarios (paciente/profesional)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Muestra el formulario de login de usuario
        [HttpGet]
        public IActionResult Login() => View();

        // ACCIONES (POST)

        // Envia los datos del usuario a la API para validar el login
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View(model);
        }

        // Envía los datos del formulario de registro a la API para crear un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ViewBag.Error = "Error al registrar usuario: " + errorMsg;

            return View(model);
        }

        // Valida la clave del administrador contra la API y redirige al panel si es correcta
        [HttpPost]
        public async Task<IActionResult> AdminLogin(ClaveAdminViewModel model)
        {
            if (!ModelState.IsValid)
                return View("ClaveAdmin", model); // Mostrar la vista original con errores

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiAdminBaseUrl}/validatekey", content);

            if (response.IsSuccessStatusCode)
            {
                // Éxito: Redirigir al panel del administrador
                return RedirectToAction("AdminPanel", "AdminPanel");
            }

            // Si falló, mostrar mensaje de error
            ViewBag.Error = "Clave de administrador incorrecta.";
            return View("ClaveAdmin", model);
        }
    }
}