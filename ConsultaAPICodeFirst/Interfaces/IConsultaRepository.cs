using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface IConsultaRepository
    {
        public ICollection<Consulta> FindAll();

        public Consulta FindById(int id);

        public Consulta Insert(Consulta entity);

        public void Update(Consulta entity);

        public void UpdatePartial(JsonPatchDocument patch, Consulta entity);

        public void Delete(Consulta entity);
    }
}
