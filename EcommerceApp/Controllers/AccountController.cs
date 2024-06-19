using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository repo;

        public AccountController(IAccountRepository repo)
        {
            this.repo = repo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await repo.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500, result.Errors);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await repo.SignInAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [HttpPost("GrantAdminRole")]
        public async Task<IActionResult> GrantAdminRole(SignInModel model)
        {
            var result = await repo.GrantRoleAdminAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(SignUpModel model)
        {
            var result = await repo.UpdateAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500, result.Errors);
        }
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model)
        {
            var result = await repo.UpdatePasswordAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500, result.Errors);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            var result = await repo.DeleteAsync(email);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500, result.Errors);
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
        public async Task<IActionResult> GetById(string id)
        {
            var user = await repo.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
    }
}
