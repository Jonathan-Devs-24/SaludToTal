using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaludTotal_AppWeb.Models;
using System.Net.Http;

public class PacienteController : Controller
{
    private readonly HttpClient _httpClient;

    public PacienteController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5065/api/Paciente/paginado?page={page}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
            return View(new List<PacienteViewModel>());

        var json = await response.Content.ReadAsStringAsync();

        dynamic result = JsonConvert.DeserializeObject(json);

        var pacientes = new List<PacienteViewModel>();
        foreach (var item in result.data)
        {
            pacientes.Add(new PacienteViewModel
            {
                Nombre = item.usuario.nombre,
                Apellido = item.usuario.apellido,
                Dni = item.usuario.dni,
                Correo = item.usuario.correo,
                NroTelefono = item.usuario.nroTelefono,
                NumeroAfiliado = item.numeroAfiliado
            });
        }

        ViewBag.Page = (int)result.page;
        ViewBag.TotalPages = (int)result.totalPages;

        return View(pacientes);
    }
}
