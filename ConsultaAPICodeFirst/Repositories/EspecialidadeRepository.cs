using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class EspecialidadeRepository: IEspecialidadeRepository
    {
        DataContext ctx;

        public EspecialidadeRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Especialidade entity)
        {
            ctx.Especialidade.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Especialidade> FindAll()
        {
            return ctx.Especialidade.ToList();
        }

        public Especialidade FindById(int id)
        {
            return ctx.Especialidade.Find(id);
        }

        public Especialidade Insert(Especialidade entity)
        {
            ctx.Especialidade.Add(entity);

            ctx.SaveChanges();

            return entity;
        }

        public void Update(Especialidade entity)
        {
            ctx.Especialidade.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Especialidade entity)
        {
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }

    }
}
