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
    public class ConsultaTests
    {
        [Fact]
        public void testeInstanciacaoRetornaNotNull()
        {
            //Arrange
            Consulta entity;

            //Act
            entity = new Consulta();

            //Assert
            Assert.NotNull(entity);
        }
    }
}
