using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using StoreProgram_.Service.Interfaces;

namespace StoreProgram_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Client>>> GetAllGeneric()
        {
            return Ok(await _clientService.GetAll());
        }

        [HttpGet("GeClientByID")]
        public async Task<ActionResult<IEnumerable<ClientResponse>>> GetByClientID(int id)
        {
            return Ok(await _clientService.GetAsync(id));
        }

        [HttpPost("CreateNewClient")]
        public async Task<ActionResult<List<Client>>> AddClient([FromBody] ClientRequest client)
        {

            await _clientService.InsertAsync(client);

            return Ok(await _clientService.GetAll());
        }
        [HttpPut("UpdateClient")]
        public async Task<ActionResult<List<Client>>> UpdateClient(int id ,[FromBody]ClientRequest request)
        {
            await _clientService.UpdateAsync(id, request);

            return Ok(await _clientService.GetAll());
        }
        [HttpDelete("DeleteClient")]
        public async Task<ActionResult<List<Client>>> Delete(int id)
        {
            await _clientService.DeleteAsync(id);

            return Ok(await _clientService.GetAll());
        }

    }
}
