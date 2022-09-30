using ConsultaAPICodeFirst.Exceptions;
using ConsultaAPICodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsultaAPICodeFirst.Teste.Models
{
    public class TipoUsuarioTests
    {
        [Fact]
        public void testeInstanciacaoRetornaNotNull()
        {
            //Arrange
            TipoUsuario entity;

            //Act
            entity = new TipoUsuario();

            //Assert
            Assert.NotNull(entity);
        }

    }
}
