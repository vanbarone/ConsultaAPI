using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace ConsultaAPICodeFirst.Interfaces
{
    public interface IUsuarioRepository:IDisposable
    {
        public ICollection<Usuario> FindAll();

        public Usuario FindById(int id);

        public Usuario Insert(Usuario entity);

        public void Update(Usuario entity);

        public void UpdatePartial(JsonPatchDocument patch, Usuario entity);

        public void Delete(Usuario entity);
    }
}
