using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        DataContext ctx;

        public TipoUsuarioRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(TipoUsuario entity)
        {
            ctx.TipoUsuario.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<TipoUsuario> FindAll()
        {
            return ctx.TipoUsuario.ToList();
        }

        public TipoUsuario FindById(int id)
        {
            return ctx.TipoUsuario.Find(id);
        }

        public TipoUsuario FindByTipo(string _tipo)
        {
            return ctx.TipoUsuario.Where(t => t.Tipo == _tipo).FirstOrDefault();
        }

        public TipoUsuario Insert(TipoUsuario entity)
        {
            ctx.TipoUsuario.Add(entity);

            ctx.SaveChanges();

            return entity;
        }

        public void Update(TipoUsuario entity)
        {
            ctx.TipoUsuario.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, TipoUsuario entity)
        {
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
