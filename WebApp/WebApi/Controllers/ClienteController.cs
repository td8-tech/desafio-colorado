using Data.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Services.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClientService _service;        
        public ClienteController(IClientService service)
        {
            _service = service;            
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Clientes", Description = "Lista todas os clientes")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ClientEntity))]        
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Nenhum registro encontrado!")]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("Nenhum registro encontrado!");
        }

        [HttpGet]
        [Route("GetById")]
        [SwaggerOperation(Summary = "Clientes", Description = "Retonar o cliente por id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ClientEntity))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Id não fornecido!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "CXliente não encontrado!")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
                return BadRequest("Id não fornecido!");

            var response = await _service.GetById(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("Cliente não encontrada!");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Inserir Cliente", Description = "Insere um cliente")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ClientEntity))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Cliente não informada!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Não foi possível inserir!")]
        public async Task<IActionResult> Create([FromBody] ClientEntity cliente)
        {
            if (cliente == null)
                BadRequest("Cliente não informado!");

            var response = await _service.Add(cliente);

            if (response != null)
            { 
                return Ok(response);
            }

            return NotFound("Não foi possível inserir");
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Clientes", Description = "Retonar o cliente atualizado")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ClientEntity))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Cliente não fornecido!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Não foi possível atualizar!")]
        public async Task<IActionResult> Update([FromBody] ClientEntity cliente)
        {
            if (cliente == null)
                return BadRequest("Categoria não informada!");

            var response = await _service.Update(cliente);

            if (response != null)
            {  
                return Ok(response);
            }

            return NotFound("Não foi possível atualizar!");
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Delete", Description = "Deletar um cliente")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Id não fornecido!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Não foi encontrado registro para exclusão!")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Id não fornecido!");

            var result = await _service.Delete(id);

            if (result)
            {
                return Ok();
            }

            return NotFound("Não foi encontrado registro para exclusão!");
        }

    }
}
