using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaludTotal_AppWeb.Models;

namespace SaludTotal_AppWeb.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://saludtotalapirest-ame2f3hrd7h5csc4.chilecentral-01.azurewebsites.net/api/");
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;
            var url = $"usuario/paginados?page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<UsuarioViewModel>>(url);

            if (response == null || response.Data == null)
            {
                ViewBag.Error = "Error al obtener los usuarios.";
                return View(new PaginatedResponse<UsuarioViewModel>()); // Asegura que Model nunca sea null
            }

            return View(response);
        }

        // Paginado para pacientes
        public async Task<IActionResult> Pacientes(int page = 1)
        {
            var url = $"paciente/paginados?page={page}&pageSize=5";
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<PacienteViewModel>>(url);
            return View(response);
        }

        // Paginado para profesionales
        public async Task<IActionResult> Profesionales(int page = 1)
        {
            string url = $"profesional/paginados?page={page}&pageSize=5";
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<ProfesionalViewModel>>(url);

            if (response == null || response.Data == null)
            {
                ViewBag.Error = "Error al obtener los profesionales.";
                return View(new PaginatedResponse<ProfesionalViewModel>());
            }

            return View(response);
        }
    }
}
