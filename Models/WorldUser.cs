using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheWorld.Models
{
    // information that we want to store at the user-level
    // we are exending the Identity User
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}