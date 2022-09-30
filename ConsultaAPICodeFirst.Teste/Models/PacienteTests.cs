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
    public class PacienteTests
    {
        [Fact]
        public void testeInstanciacaoRetornaNotNull()
        {
            //Arrange
            Paciente entity;

            //Act
            entity = new Paciente();

            //Assert
            Assert.NotNull(entity);
        }

    }
}
