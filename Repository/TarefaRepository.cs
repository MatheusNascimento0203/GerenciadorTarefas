using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Data;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.Repository
{
    public class TarefaRepository
    {
        
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task <IEnumerable<Tarefa>> ListarTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa> ExcluirTarefa(int idTarefa)
        {
            var tarefa = await _context.Tarefas.FindAsync(idTarefa);
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

    }
}