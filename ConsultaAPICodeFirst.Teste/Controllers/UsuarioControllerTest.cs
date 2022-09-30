using ConsultaAPICodeFirst.Controllers;
using ConsultaAPICodeFirst.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using Moq;

namespace ConsultaAPICodeFirst.Teste.Controllers
{
    public class UsuarioControllerTest
    {
        // Arrange
        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest(ITestOutputHelper _saidaConsoleTeste)
        {
            _mockRepo = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_mockRepo.Object);
        }

        [Fact]
        public void TestActionResultReturnOk()
        {
            // Execução
            var result = _controller.BuscarTodos();

            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
