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
        try
        {
            await _tarefaRepository.CriarTarefa(tarefa);
            return Ok();
            
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            return StatusCode(500, "Ocorreu um erro ao criar a tarefa.");
        }
      
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

    [HttpPost("{tarefaId}/editar-tarefa")]
    public async Task<ActionResult> AtualizarTarefa(int tarefaId, [FromBody] Tarefa tarefa)
    {
        try
        {
            await _tarefaRepository.EditarTarefa(tarefaId, tarefa);
            return Ok();            
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, "Ocorreu um erro ao criar a tarefa.");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
