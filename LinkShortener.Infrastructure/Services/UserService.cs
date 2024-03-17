using LinkShortener.Application.Models;
using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public class UserService(IUnitOfWork unitOfWork): IUserService
{
   public async Task<User> GetUserByNameOrEmail(string? name, string? email)
   {
         var userRepo = unitOfWork.GetRepository<User>();
         var users = await userRepo.GetAllAsync();
         return users.ToList().FirstOrDefault(x => x.Name.Equals(name,StringComparison.CurrentCultureIgnoreCase) || x.Email == email);
   }
}