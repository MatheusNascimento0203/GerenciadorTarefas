using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Repository;
using GerenciadorTarefas.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
        return View(tarefas.Select(t => new TarefaViewModel(t)));
    }

    [HttpPost]
    public async Task<ActionResult> CriarTarefa([FromBody]Tarefa tarefa)
    {
        await _tarefaRepository.CriarTarefa(tarefa);
        return Ok();

    }

    [HttpPost("{tarefaId}/remover")]
    public async Task<ActionResult> RemoverTarefa(int tarefaId)
    {
        await _tarefaRepository.ExcluirTarefa(tarefaId);
        return Ok();
    }

    [HttpPost("{tarefaId}/concluir-tarefa")]
    public async Task<ActionResult> ConcluirTarefa(int tarefaId)
    {
        await _tarefaRepository.AlteraConclusaoTarefa(tarefaId);
        return Ok();
    }

    [HttpPost("buscar-tarefa")]
    public async Task<ActionResult<Tarefa>> BuscarTarefaPorId([FromBody] int id)
    {
        var tarefaExistente = await _tarefaRepository.BuscarPorId(id);
        if (tarefaExistente == null)
            return NotFound();

        return Ok(tarefaExistente);
    }

    [HttpPost("{tarefaId}/editar-tarefa")]
    public async Task<ActionResult> AtualizarTarefa(int tarefaId, [FromBody]Tarefa tarefa)
    {
        await _tarefaRepository.EditarTarefa(tarefaId, tarefa);
        return Ok();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
