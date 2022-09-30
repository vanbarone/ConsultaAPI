using ConsultaAPICodeFirst.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaAPICodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository repository;

        public LoginController(ILoginRepository _repository)
        {
            repository = _repository;
        }


        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var retorno = repository.Logar(email, senha);

            if (retorno == null)
                return Unauthorized();

            return Ok(new { token = retorno });
        }
    }
}
