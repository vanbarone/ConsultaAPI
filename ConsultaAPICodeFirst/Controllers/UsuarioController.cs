using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConsultaAPICodeFirst.Controllers
{
    /// <summary>
    /// Usuários
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        IUsuarioRepository repo;

        public UsuarioController(IUsuarioRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Lista de objetos(Usuario)</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var retorno = repo.FindAll();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Mostra o usuário cadastrado com esse Id
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto(Usuario)</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Usuário não cadastrado" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Insere um usuário
        /// </summary>
        /// <param name="entity">Objeto(Usuario)</param>
        /// <returns>Objeto(Usuario)</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpPost]
        public IActionResult Inserir(Usuario entity)
        {
            try
            {
                var retorno = repo.Insert(entity);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de um usuario passando o objeto (Usuario)
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="entity">Objeto(Usuario)</param>
        /// <returns>NoContent</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Usuário não cadastrado" });

                repo.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de um usuário passando o patch
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="patch">Patch com os dados que devem ser alterados</param>
        /// <returns>NoContent</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpPatch("{id}")]
        public IActionResult AlterarPatch(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch == null)
                    return BadRequest(new { message = "Objeto não passado" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Usuário não cadastrado" });

                //Efetua alteração parcial
                repo.UpdatePartial(patch, obj);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>NoContent</returns>
        [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Usuário não cadastrado" });

                //Efetua alteração
                repo.Delete(obj);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });            }
        }
    }
}
