using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class UpdateUserPhotoCommand : IRequest<Response<string>>
    {
        public  string UserId { get; set; } 

        public IFormFile Photo { get; set; }    

        public UpdateUserPhotoCommand() { }

        public UpdateUserPhotoCommand(string userId, IFormFile photo)
        {
            UserId = userId;
            Photo = photo;
        }
    }
}
