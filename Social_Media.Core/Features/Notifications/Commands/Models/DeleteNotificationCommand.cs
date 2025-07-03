using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Notifications.Commands.Models
{
    public class DeleteNotificationCommand : IRequest<Response<string>> 
    {

        public int Id { get; set; }
         
        public NotificationType Type { get; set; }
        public DeleteNotificationCommand(int id,NotificationType type)
        { 
            Id = id;
            Type = type;
        
        }

    }
}
