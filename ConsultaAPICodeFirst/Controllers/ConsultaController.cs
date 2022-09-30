using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using ConsultaAPICodeFirst.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ConsultaAPICodeFirst.Controllers
{
    /// <summary>
    /// Consultas teste
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR, DESENVOLVEDOR")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        IConsultaRepository repo;

        public ConsultaController(IConsultaRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todas as consultas cadastradas
        /// </summary>
        /// <returns>Lista de objetos(Consulta)</returns>
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
        /// Mostra a consulta cadastrada com esse Id
        /// </summary>
        /// <param name="id">Identificador da consulta</param>
        /// <returns>Objeto(Consulta)</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Consulta não cadastrada" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Insere uma consulta
        /// </summary>
        /// <param name="entity">Objeto(Consulta)</param>
        /// <returns>Objeto(Consulta)</returns>
        [HttpPost]
        public IActionResult Inserir(Consulta entity)
        {
            try
            {
                //Consistindo dados
                if (entity.Data == DateTime.MinValue)
                    return BadRequest(new { message = "Data não informada" });

                if (entity.IdMedico == 0)
                    return BadRequest(new { message = "Médico não informado" });

                if (entity.IdPaciente == 0)
                    return BadRequest(new { message = "Paciente não informado" });


                //Chamando o repository para salvar no BD
                var retorno = repo.Insert(entity);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de uma consulta passando o objeto (Consulta)
        /// </summary>
        /// <param name="id">Identificador da consulta</param>
        /// <param name="entity">Objeto(Consulta)</param>
        /// <returns>NoContent</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Consulta não cadastrada" });

                repo.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera os dados de uma consulta passando o patch
        /// </summary>
        /// <param name="id">Identificador da consulta</param>
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
                    return NotFound(new { Message = "Consulta não cadastrada" });

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
        /// Exclui uma consulta
        /// </summary>
        /// <param name="id">Identificador da consulta</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Consulta não cadastrada" });

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
