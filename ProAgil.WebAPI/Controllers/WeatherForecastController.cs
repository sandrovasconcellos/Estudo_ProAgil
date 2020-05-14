using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        //propriedade
        public ProAgilContext _context { get; }
        //injeção de dependencia
        //construtor
        public WeatherForecastController(ProAgilContext context)
        {
            _context = context;
        }        
        

        //GET - /api/WeatherForecastController
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _context.Eventos.ToListAsync();
                //ok retorna o status code de 2000
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }            
        }

        //GET - /api/WeatherForecastController/N
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
             try
            {
                var results = await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);
                //ok retorna o status code de 2000
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }     
        }

        //---------------------------------------------------------------------
        
        
        
        //private readonly ILogger<WeatherForecastController> _logger;    
        // public WeatherForecastController(ILogger<WeatherForecastController> logger)
        // {
        //     _logger = logger;
        // }

    }
}
