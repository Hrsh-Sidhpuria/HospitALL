using HospitALL.Models;
using HospitALL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitALL.Utilities
{
   
    
    public class DbInitializer : IDbInitializer
    {
		UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _DBContext;



		public DbInitializer(
	 UserManager<ApplicationUser> userManager,
	 RoleManager<IdentityRole> roleManager,
	 ApplicationDbContext dBContext)
		{
            _userManager = userManager;
            _roleManager = roleManager;
            _DBContext = dBContext;
        }

        public void initializer()
        {
            try
            {
                if (_DBContext.Database.GetPendingMigrations().Count()>0 )
                {
                    _DBContext.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.Website_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Doctor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_patient)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "harsh",
                    Email = "hrsh@h.com"
                },"h123").GetAwaiter().GetResult();

                var appUser = _DBContext.ApplicationUser.FirstOrDefault(x => x.Email == "hrsh@h.com");
                if(appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser,WebsiteRoles.Website_Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
}
