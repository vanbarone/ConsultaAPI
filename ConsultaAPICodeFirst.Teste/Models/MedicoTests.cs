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
    public class MedicoTests
    {
        [Fact]
        public void testeInstanciacaoRetornaNotNull()
        {
            //Arrange
            Medico entity;

            //Act
            entity = new Medico();

            //Assert
            Assert.NotNull(entity);
        }

    }
}
