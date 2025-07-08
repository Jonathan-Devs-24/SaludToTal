using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaludTotal_AppWeb.Models;
using System.Net.Http;
using System.Text;

public class ProfesionalController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:5065/api/Profesional";

    public ProfesionalController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/paginado?page={page}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
            return View(new List<ProfesionalViewModel>());

        var json = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(json);

        var profesionales = new List<ProfesionalViewModel>();
        foreach (var item in result.data)
        {
            profesionales.Add(new ProfesionalViewModel
            {
                IdProfesional = item.idProfesional,
                Nombre = item.usuario.nombre,
                Apellido = item.usuario.apellido,
                Dni = item.usuario.dni,
                Correo = item.usuario.correo,
                NroTelefono = item.usuario.nroTelefono,
                NroMatricula = item.nroMatricula,
                HorarioAtencion = item.horarioAtencion
            });
        }

        ViewBag.Page = (int)result.page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)result.totalItems / result.pageSize);

        return View(profesionales);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var profesional = JsonConvert.DeserializeObject<ProfesionalViewModel>(json);

        return View(profesional);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var profesional = JsonConvert.DeserializeObject<ProfesionalViewModel>(json);

        return View(profesional);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProfesionalViewModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_apiUrl}/{model.IdProfesional}", content);
        if (!response.IsSuccessStatusCode)
            return View(model);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
        return RedirectToAction("Index");
    }
}
