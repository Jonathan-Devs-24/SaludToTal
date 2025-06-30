using Microsoft.AspNetCore.Mvc;
using SaludTotal_AppWeb.Models;

public class AdminPanelController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminPanelController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://saludtotalapirest-ame2f3hrd7h5csc4.chilecentral-01.azurewebsites.net/"); // Asegurate de que coincida con tu API real
    }

    public IActionResult AdminPanel()
    {
        var model = new PaginatedResponse<PacienteViewModel>
        {
            Data = new List<PacienteViewModel>(),
            Page = 1,
            TotalPages = 1
        };

        return View(model);
    }

    public async Task<IActionResult> Pacientes(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<PacienteViewModel>>(
            $"api/Paciente/paginados?page={page}");

        return View(response);
    }
}
