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
    /// Médicos
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR, MÉDICO")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        IMedicoRepository repo;

        public MedicoController(IMedicoRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todos os médicos cadastrados
        /// </summary>
        /// <returns>Lista de objetos(Medico)</returns>
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
        /// Mostra o médico cadastrado com esse Id
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>Objeto(Medico)</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Insere um médico
        /// </summary>
        /// <param name="entity">Objeto(Medico)</param>
        /// <returns>Objeto(Medico)</returns>
        [HttpPost]
        public IActionResult Inserir(Medico entity)
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
        /// Altera os dados de um médico passando o objeto (Medico)
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <param name="entity">Objeto(Medico)</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                repo.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de um médico passando o patch
        /// </summary>
        /// <param name="id">Identificador do médico</param>
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
                    return NotFound(new { Message = "Médico não cadastrado" });

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
        /// Exclui um médico
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

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
