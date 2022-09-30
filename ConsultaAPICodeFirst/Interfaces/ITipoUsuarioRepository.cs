using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        public ICollection<TipoUsuario> FindAll();

        public TipoUsuario FindById(int id);

        public TipoUsuario FindByTipo(string tipo);

        public TipoUsuario Insert(TipoUsuario entity);

        public void Update(TipoUsuario entity);

        public void UpdatePartial(JsonPatchDocument patch, TipoUsuario entity);

        public void Delete(TipoUsuario entity);
    }
}
