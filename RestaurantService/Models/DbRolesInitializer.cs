using Microsoft.AspNetCore.Identity;
using RestaurantLibrary.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class DbRolesInitializer
    {
        private readonly UsersDbContext dbContext;
        public DbRolesInitializer(UsersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private readonly List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.Admin,
            NormalizedName=ApplicationRoles.Admin.ToUpper(),
            ConcurrencyStamp=Guid.NewGuid().ToString()},

            new IdentityRole{Name = ApplicationRoles.Member,
            NormalizedName=ApplicationRoles.Member.ToUpper(),
            ConcurrencyStamp=Guid.NewGuid().ToString()},
        };

        public void AddRoles()
        {
            if (dbContext.Roles.Any()) return;

            foreach (var item in roles)
            {
                dbContext.Roles.Add(item);
            }
            dbContext.SaveChanges();
        }
    }
}
