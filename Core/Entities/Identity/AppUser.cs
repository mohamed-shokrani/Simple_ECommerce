using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity;

public class AppUser :IdentityUser
{
    public string Address  { get; set; }
    public List<RefreshToken>? RefreshTokens   { get; set; }

    //  public ICollection<ApplicationUserLogin> Logins { get; set; }


}
