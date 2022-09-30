using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Exceptions;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {
        DataContext ctx;

        public UsuarioRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Usuario entity)
        {
            ctx.Usuario.Remove(entity);

            ctx.SaveChanges();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public ICollection<Usuario> FindAll()
        {
            return ctx.Usuario
                        .Include(t => t.TipoUsuario)
                        .ToList();
        }

        public Usuario FindById(int id)
        {
            var obj = ctx.Usuario
                            .Include(t => t.TipoUsuario)
                            .FirstOrDefault(u => u.Id == id);

            return obj;
        }

        public Usuario Insert(Usuario entity)
        {            
            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            entity.TipoUsuario = repoTipo.FindById(entity.IdTipoUsuario);

            if (entity.TipoUsuario == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            } else
            {
                if (entity.TipoUsuario.Tipo == "Médico" || entity.TipoUsuario.Tipo == "Paciente")
                {
                    throw new ConstraintException("Tipo de Usuário inválido - Médicos e Pacientes devem ser cadastrados usando seus respectivos controllers");
                }
            }

            //criptografa a senha
            entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //Salva no BD
            ctx.Usuario.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Usuario entity)
        {
            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            if (repoTipo.FindById(entity.IdTipoUsuario) == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            }

            //criptografa a senha
            entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //Salva no BD
            ctx.Usuario.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Usuario entity)
        {
            patch.ApplyTo(entity);

            //criptografa a senha
            entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
