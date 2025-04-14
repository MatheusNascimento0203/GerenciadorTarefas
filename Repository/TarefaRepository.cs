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
            var tarefaExistente = await _context.Tarefas.AnyAsync(t => t.Descricao.ToLower() == tarefa.Descricao.ToLower() && !t.Concluida);

            if (tarefaExistente)
            {
                throw new InvalidOperationException("Já existe uma tarefa com esse nome que ainda não foi concluída.");
            }

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

        public async Task<Tarefa> AlteraConclusaoTarefa(int idTarefa)
        {
            var tarefa = await _context.Tarefas.FindAsync(idTarefa);

            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }


            tarefa.Concluida = !tarefa.Concluida; 
            
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }     

        public async Task<Tarefa> BuscarPorId(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> EditarTarefa(int idTarefa, Tarefa tarefa) 
        {
            var tarefaExistente = await _context.Tarefas.FindAsync(idTarefa);

            if (tarefaExistente == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            var tarefaDuplicada = await _context.Tarefas.AnyAsync(t => t.Id != idTarefa && t.Descricao.ToLower() == tarefa.Descricao.ToLower() && !t.Concluida);

            if (tarefaDuplicada)
            {
                throw new InvalidOperationException("Já existe uma tarefa com esse nome que ainda não foi concluída.");
            }
   
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.DataVencimento = tarefa.DataVencimento;
            tarefaExistente.Urgente = tarefa.Urgente;

            _context.Tarefas.Update(tarefaExistente);
            await _context.SaveChangesAsync();
            return tarefaExistente;
        } 

    }
}