using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface IMedicoRepository
    {
        public ICollection<Medico> FindAll();

        public Medico FindById(int id);

        public Medico Insert(Medico entity);

        public void Update(Medico entity);

        public void UpdatePartial(JsonPatchDocument patch, Medico entity);

        public void Delete(Medico entity);
    }
}
