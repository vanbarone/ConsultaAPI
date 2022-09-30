using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Exceptions;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class PacienteRepository: IPacienteRepository
    {
        DataContext ctx;

        public PacienteRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Paciente entity)
        {
            ctx.Paciente.Remove(entity);

            ctx.SaveChanges();

            //Apaga tb o usuario
            UsuarioRepository repo = new UsuarioRepository(ctx);
            repo.Delete(entity.Usuario);
        }

        public ICollection<Paciente> FindAll()
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            return ctx.Paciente
                        .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                        .Include(c => c.Consultas).ThenInclude(m => m.Medico).ThenInclude(u => u.Usuario)
                        .ToList();
        }

        public Paciente FindById(int id)
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            var obj = ctx.Paciente
                            .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(c => c.Consultas).ThenInclude(m => m.Medico).ThenInclude(u => u.Usuario)
                            .FirstOrDefault(p => p.Id == id);

            return obj;
        }

        public Paciente Insert(Paciente entity)
        {
            //Seta o tipo de usuário como 'médico'
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            entity.Usuario.IdTipoUsuario = repoTipo.FindByTipo("Paciente").Id;

            
            //Salva no BD
            ctx.Paciente.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Paciente entity)
        {
            //Seta o tipo de usuário como 'médico'
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            entity.Usuario.IdTipoUsuario = repoTipo.FindByTipo("Paciente").Id;


            //Salva no BD
            ctx.Paciente.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Paciente entity)
        {
            patch.ApplyTo(entity);

            //Seta o tipo de usuário como 'médico'
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            entity.Usuario.IdTipoUsuario = repoTipo.FindByTipo("Paciente").Id;

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
