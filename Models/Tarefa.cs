using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Models
{
    public class Tarefa
    {   
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Infome a descrição da tarefa")]
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Concluida { get; set; } = false;
        public bool Urgente { get; set; } = false;
        
    }
}