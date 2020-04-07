using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        //propriedade
        public DataContext _context { get; }

        //injeção de dependencia
        //construtor
        public WeatherForecastController(DataContext context)
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
                var results = await _context.Eventos.FirstOrDefaultAsync(x => x.Eventoid == id);
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
