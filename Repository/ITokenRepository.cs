using Microsoft.EntityFrameworkCore;
using Recruits.API.Models;

namespace Recruits.API.Repository
{
    public interface ITokenRepository
    {
        Tokens Authenticate(Users users);

       // DbSet<Users> Recruit { get; set; }
    }
}
