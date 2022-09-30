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
    /// Especialidades médicas
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        IEspecialidadeRepository repo;

        public EspecialidadeController(IEspecialidadeRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todas as especialidades cadastradas
        /// </summary>
        /// <returns>Lista de objetos(Especialidade)</returns>
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
        /// Mostra a especialidade cadastrada com esse Id
        /// </summary>
        /// <param name="id">Identificador da especialidade</param>
        /// <returns>Objeto(Especialidade)</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Especialidade não cadastrada" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Insere uma especialidade
        /// </summary>
        /// <param name="entity">Objeto(Especialidade)</param>
        /// <returns>Objeto(Especialidade)</returns>
        [HttpPost]
        public IActionResult Inserir([FromForm] Especialidade entity)
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
        /// Altera os dados de uma especialidade passando o objeto (Especialidade)
        /// </summary>
        /// <param name="id">Identificador da especialidade</param>
        /// <param name="entity">Objeto(Especialidade)</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm] Especialidade entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Especialidade não cadastrada" });

                repo.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de uma especialidade passando o patch
        /// </summary>
        /// <param name="id">Identificador da especialidade</param>
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
                    return NotFound(new { Message = "Especialidade não cadastrada" });

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
        /// Exclui uma especialidade
        /// </summary>
        /// <param name="id">Identificador da especialidade</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Especialidade não cadastrada" });

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
