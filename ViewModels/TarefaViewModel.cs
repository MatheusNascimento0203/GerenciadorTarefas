
using GerenciadorTarefas.Models;
namespace GerenciadorTarefas.ViewModels;

public class TarefaViewModel
{   
    public int Id { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool Concluida { get; set; }
    public bool Urgente { get; set; }

    
    public TarefaViewModel (Tarefa tarefa) 
    {
        Id = tarefa.Id;
        Descricao = tarefa.Descricao;
        DataVencimento = tarefa.DataVencimento;
        Urgente = tarefa.Urgente;
        Concluida = tarefa.Concluida;
    }

    private string ConverterDataVencimentoParaString(DateTime dataVencimento)
    {
      // Array com os nomes dos meses
      string[] meses = {
          "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
          "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
    };

      // Formatando a data
      int dia = dataVencimento.Day;
      int mes = dataVencimento.Month;
      int ano = dataVencimento.Year;

      // Montando a data formatada
      string dataFormatada = $"{dia} de {meses[mes - 1]} de {ano}";

      return dataFormatada;
    }
}