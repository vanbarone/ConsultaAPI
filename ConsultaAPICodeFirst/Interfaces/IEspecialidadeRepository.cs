using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface IEspecialidadeRepository
    {
        public ICollection<Especialidade> FindAll();

        public Especialidade FindById(int id);

        public Especialidade Insert(Especialidade entity);

        public void Update(Especialidade entity);

        public void UpdatePartial(JsonPatchDocument patch, Especialidade entity);

        public void Delete(Especialidade entity);
    }
}
