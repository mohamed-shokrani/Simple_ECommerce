using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructre.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;
public class LoginTimeRepository : ILoginTimeRepository
{
    private readonly AppIdentityDbContext _identityContext;

    public LoginTimeRepository(AppIdentityDbContext IdentityDbContext)
    {
        _identityContext = IdentityDbContext;
    }

    public async Task AddLoginTime(AppUser user)
    {
        var applicationUserLogin = new ApplicationUserLogin();
        applicationUserLogin.IdentityUserId = user.Id;
        applicationUserLogin.LoginTime = DateTime.Now;
        await _identityContext.AddAsync(applicationUserLogin);
        await _identityContext.SaveChangesAsync();
    }
}
