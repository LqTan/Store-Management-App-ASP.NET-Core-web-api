using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Helpers;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly StoreContext context;

        public AccountRepository
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                RoleManager<IdentityRole> roleManager,
                StoreContext context
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync( model.Email );            
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password );
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }
            var authClaims = new List<Claim> { 
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Name, user.LastName + " " + user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var role in userRoles )
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                // Trả về lỗi nếu email đã tồn tại
                var errorResult = IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Email is already taken."
                });
                return errorResult;
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = (model.Address == null) ? "NONE" : model.Address,
                Phone = (model.Phone == null) ? "NONE" : model.Phone,
                UserName = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);            
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                await userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }

        public async Task<string> GrantRoleAdminAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }
            if (!await roleManager.RoleExistsAsync(AppRole.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
            }
            if (await userManager.IsInRoleAsync(user, AppRole.Admin))
            {
                return "Failed: UserAlreadyInRole";
            }
            var result = await userManager.AddToRoleAsync(user, AppRole.Admin);
            return result.Succeeded ? "Success: Role Admin granted." : "Failed: Could not grant role.";
        }

        public async Task<List<SignUpModel>> GetAllAsync()
        {         
            var users = userManager.Users.ToList();
            var signUpModels = users.Select(user => new SignUpModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone
            }).ToList();
            return signUpModels;            
        }

        public async Task<SignUpModel> GetByIdAsync(string id)
        {   
            var existingUser = await userManager.FindByIdAsync(id);
            if (existingUser != null)
            {
                var signUpModel = new SignUpModel
                {
                    Id = existingUser.Id,
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                    Address = existingUser.Address,
                    Phone = existingUser.Phone
                };
                return signUpModel;
            }
            return null;
        }

        public async Task<IdentityResult> UpdateAsync(SignUpModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Trả về lỗi nếu không tìm thấy user
                var errorResult = IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
                return errorResult;
            }
            if (model.FirstName != null) user.FirstName = model.FirstName;
            if (model.LastName != null) user.LastName = model.LastName;
            if (model.Password != null) user.Password = model.Password;
            if (model.Address != null) user.Address = model.Address;
            if (model.Phone != null) user.Phone = model.Phone;

            var result = await userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Trả về lỗi nếu không tìm thấy user
                var errorResult = IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
                return errorResult;
            }

            var result = await userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> UpdatePasswordAsync(UpdatePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result;
        }
    }
}
