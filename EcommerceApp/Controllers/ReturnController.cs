using EcommerceApp.Helpers;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly IReturnRepository repo;

        public ReturnController(IReturnRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await repo.GetAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await repo.GetByIdAsync(id);
            return brand == null ? NotFound() : Ok(brand);
        }
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Add(ReturnModel model)
        {
            try
            {
                var newBrandId = await repo.AddAsync(model);
                var brand = await repo.GetByIdAsync(newBrandId);
                return brand == null ? NotFound(newBrandId) : Ok(brand);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Update(int id, ReturnModel model)
        {
            await repo.UpdateAsync(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }
    }
}
