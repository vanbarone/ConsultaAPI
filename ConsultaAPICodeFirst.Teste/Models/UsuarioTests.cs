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
    public class UsuarioTests
    {
        [Fact]
        public void testeInstanciacaoRetornaNotNull()
        {
            //Arrange
            Usuario entity;

            //Act
            entity = new Usuario();

            //Assert
            Assert.NotNull(entity);
        }

        [Fact]
        public void testeFormatoDeEmailInvalido()
        {
            //Arrange
            string email = "teste";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Usuario().Email = email
            );
        }

        [Fact]
        public void testeFormatoDeSenhaInvalido()
        {
            //Arrange
            string senha = "12";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Usuario().Senha = senha
            );
        }
    }
}
