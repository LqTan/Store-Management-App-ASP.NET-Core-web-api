using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        public Task<string> GrantRoleAdminAsync(SignInModel model);
        public Task<List<SignUpModel>> GetAllAsync();
        public Task<SignUpModel> GetByIdAsync(string id);
        public Task<IdentityResult> UpdateAsync(SignUpModel model);
        public Task<IdentityResult> DeleteAsync(string email);
        public Task<IdentityResult> UpdatePasswordAsync(UpdatePasswordModel model);
    }
}
