using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Chats.Queries.Models;
using Social_Media.Core.Features.Chats.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Chat;

namespace Social_Media.Core.Features.Chats.Queries.Handlers
{
    public class ChatQueryHandler : ResponseHandler,
        IRequestHandler<GetMessagesBetweenTwoUsersQuery, Response<List<GetMessagesBetweenTwoUsers>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public ChatQueryHandler(IUnitOFWork UnitOFWork, ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }

        public async Task<Response<List<GetMessagesBetweenTwoUsers>>> Handle(GetMessagesBetweenTwoUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Message> AllMessagesBetweenTwoUsers = await UnitOFWork.ChatUnitOFWork.MessageServices.GetMessagesBetweenTwoUsersAsync(SenderId: request.SenderId, ReceiverId: request.ReceiverId);
                List<GetMessagesBetweenTwoUsers> Mapped_Messages = UnitOFWork.Mapper.Map<List<GetMessagesBetweenTwoUsers>>(AllMessagesBetweenTwoUsers);
                return OK(Mapped_Messages);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return BadRequest<List<GetMessagesBetweenTwoUsers>>(ex.Message);
            }
        }
    }
}
