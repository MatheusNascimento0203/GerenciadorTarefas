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

        public async Task <List<Tarefa>> ListarTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

    }
}