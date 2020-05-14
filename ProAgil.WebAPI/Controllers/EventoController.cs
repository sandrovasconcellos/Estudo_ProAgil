using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    //a url:http://localhost:5000/api/evento
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        //GET - /api/Evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);
                //ok retorna o status code de 2000
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }            
        }
        
        //GET - /api/Evento/EventoId
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var results = await _repo.GetEventoAsyncById(EventoId, true);
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }            
        }

        //GET - /api/evento/getBytema/Nome do tema *sem aspas
        [HttpGet("getBytema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);

                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }            
        }

        //incluir
        //POST- /api/Evento
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync()) {
                    return Created($"/api/evento/{model.Id}", model);
                } 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }   
            return BadRequest();         
        }

        //alterar
        //PUT- /api/EventoController
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync()) {
                    return Created($"/api/evento/{model.Id}", model);
                } 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }   
            return BadRequest();         
        }

        //excluir
        //DELETE- /api/Evento/EventoId
        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete (int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync()) {
                    return Ok();
                } 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }   
            return BadRequest();         
        }
    }
}