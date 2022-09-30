using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ConsultaAPICodeFirst.Controllers
{
    /// <summary>
    /// Tipos de Usuário
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {

        ITipoUsuarioRepository repo;

        public TipoUsuarioController(ITipoUsuarioRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todos os Tipos de Usuários cadastrados
        /// </summary>
        /// <returns>Lista de objetos(TipoUsuario)</returns>
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
        /// Mostra o tipo de usuário cadastrado com esse Id
        /// </summary>
        /// <param name="id">Identificador do tipo de usuário</param>
        /// <returns>Objeto(TipoUsuario)</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Tipo de Usuário não cadastrado" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Insere um tipo de usuário
        /// </summary>
        /// <param name="entity">Objeto(TipoUsuario)</param>
        /// <returns>Objeto(TipoUsuario)</returns>
        [HttpPost]
        public IActionResult Inserir([FromForm] TipoUsuario entity)
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
        /// Altera os dados de um Tipo de Usuário passando o objeto (TipoUsuario)
        /// </summary>
        /// <param name="id">Identificador do TipoUsuario</param>
        /// <param name="entity">Objeto(TipoUsuario)</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm] TipoUsuario entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Tipo de Usuário não cadastrado" });

                repo.Update(entity);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de um Tipo de Usuário passando o patch
        /// </summary>
        /// <param name="id">Identificador do TipoUsuario</param>
        /// <param name="patch">Patch com os dados que devem ser alterados</param>
        /// <returns>NoContent</returns>
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
                    return NotFound(new { Message = "Tipo de Usuário não cadastrado" });

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
        /// Exclui um Tipo de Usuário
        /// </summary>
        /// <param name="id">Identificador do TipoUsuario</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Tipo de Usuário não cadastrado" });

                //Efetua alteração
                repo.Delete(obj);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }
    }
}
