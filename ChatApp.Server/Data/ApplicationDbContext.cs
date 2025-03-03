using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

    }
}
