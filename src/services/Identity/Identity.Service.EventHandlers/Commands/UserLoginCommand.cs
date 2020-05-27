using Identity.Service.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Service.EventHandlers.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
