using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface IPacienteRepository
    {
        public ICollection<Paciente> FindAll();

        public Paciente FindById(int id);

        public Paciente Insert(Paciente entity);

        public void Update(Paciente entity);

        public void UpdatePartial(JsonPatchDocument patch, Paciente entity);

        public void Delete(Paciente entity);
    }
}
