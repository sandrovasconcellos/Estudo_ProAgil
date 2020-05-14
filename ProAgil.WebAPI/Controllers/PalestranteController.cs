

using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        //injecao de dependencia
        public IProAgilRepository _repo { get; }
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        

        
    }
}