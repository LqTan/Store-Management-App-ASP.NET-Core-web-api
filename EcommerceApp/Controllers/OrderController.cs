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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repo;

        public OrderController(IOrderRepository repo)
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
        public async Task<IActionResult> Add(OrderModel model)
        {
            try
            {
                var newOrderId = await repo.AddAsync(model);
                var order = await repo.GetByIdAsync(newOrderId);
                return order == null ? NotFound(newOrderId) : Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Update(int id, OrderModel model)
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
