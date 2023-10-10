using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity;
public class ApplicationUserLogin
{
    public int Id { get; set; }
    public string IdentityUserId { get; set; }
    public AppUser User { get; set; }
    public DateTime LoginTime { get; set; }

}