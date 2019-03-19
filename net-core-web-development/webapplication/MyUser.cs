using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapplication;


namespace webapplication
{
    public class MyUser : IdentityUser
    {
        public DateTime JoinDate { get; set; }
        public DateTime JobTitle { get; set; }
        public string Contract { get; set; }
    }

    public class MyRole : IdentityRole
    {
        public string Description { get; set; }
    }

    public class SecurityContext : IdentityDbContext<MyUser>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {

        }
    }

    //  public class ApothecaricDbContext : IdentityDbContext
    //{
    //  public ApothecaricDbContext(DbContextOptions options) : base(options) { }
    //}


}

public static class Extensions
{
    public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["mysqlconnection:connectionString"];
        services.AddDbContext<SecurityContext>(options => options.UseMySql(connectionString, mySqlOptions =>
        {
            mySqlOptions.ServerVersion(new Version(5, 6, 43), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
        }));
    }
}

