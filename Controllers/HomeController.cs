using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Repository;

namespace GerenciadorTarefas.Controllers;

public class HomeController : Controller
{
    private readonly TarefaRepository _tarefaRepository;

    public HomeController(TarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<IActionResult> Index()
    {
        var tarefas = await _tarefaRepository.ListarTarefas();
        return View(tarefas);
    }

    [HttpPost]
    public async Task<ActionResult> CriarTarefa([FromBody]Tarefa tarefa)
    {
        await _tarefaRepository.CriarTarefa(tarefa);
        return Ok();

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
