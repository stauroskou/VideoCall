using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideoCall.Core.Entities;

public class User : IdentityUser
{
    
    //public Participant? Participant { get; set; }

    public override string UserName { get => base.UserName!; set => base.UserName = value; }
}
