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
    public class ConsultaRepository: IConsultaRepository
    {
        DataContext ctx;

        public ConsultaRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Consulta entity)
        {
            ctx.Consulta.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Consulta> FindAll()
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            return ctx.Consulta
                            .Include(m => m.Medico).ThenInclude(u => u.Usuario)
                            .Include(m => m.Medico).ThenInclude(e => e.Especialidade)
                            .Include(p => p.Paciente).ThenInclude(u => u.Usuario)
                            .ToList();
        }

        public Consulta FindById(int id)
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            var obj = ctx.Consulta
                            .Include(m => m.Medico).ThenInclude(u => u.Usuario)
                            .Include(m => m.Medico).ThenInclude(e => e.Especialidade)
                            .Include(p => p.Paciente).ThenInclude(u => u.Usuario)
                            .FirstOrDefault(c => c.Id == id);

            return obj;
        }

        public Consulta Insert(Consulta entity)
        {
            //Verifica se o médico existe
            IMedicoRepository repoMedico = new MedicoRepository(ctx);

            if (repoMedico.FindById(entity.IdMedico) == null)
            {
                throw new ConstraintException("Médico não cadastrado");
            }

            //Verifica se o paciente existe
            IPacienteRepository repoPaciente = new PacienteRepository(ctx);

            if (repoPaciente.FindById(entity.IdPaciente) == null)
            {
                throw new ConstraintException("Paciente não cadastrado");
            }


            //Salva no BD
            ctx.Consulta.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Consulta entity)
        {
            //Verifica se o médico existe
            IMedicoRepository repoMedico = new MedicoRepository(ctx);

            if (repoMedico.FindById(entity.IdMedico) == null)
            {
                throw new ConstraintException("Médico não cadastrado");
            }

            //Verifica se o paciente existe
            IPacienteRepository repoPaciente = new PacienteRepository(ctx);

            if (repoPaciente.FindById(entity.IdPaciente) == null)
            {
                throw new ConstraintException("Paciente não cadastrado");
            }


            //Salva no BD
            ctx.Consulta.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Consulta entity)
        {
            //Salva no BD
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
