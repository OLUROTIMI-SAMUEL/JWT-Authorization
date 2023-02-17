using Microsoft.EntityFrameworkCore;
using Recruits.API.Models;
using Recruits.API.Repository;

namespace Recruits.API
{
    public class TokenDbContext : DbContext
    {
        public TokenDbContext(DbContextOptions<TokenDbContext> options) : base(options)
        {

        }
        public DbSet<Users> RecruitTable { get; set; }
    }
}
