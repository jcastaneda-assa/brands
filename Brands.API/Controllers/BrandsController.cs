using Brands.API.Models;
using Brands.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brands.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandsContext _context;

        // Inject the BrandsContext dependency into the controller
        public BrandsController(BrandsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            // For bigger applications I'd implement a service layer and use a repository pattern to abstract the data access logic.
            // For simplicity, I'll directly use the BrandsContext here to fetch the data.
            var result = await _context.Brands.ToListAsync();
            return Ok(result);
        }
    }
}
