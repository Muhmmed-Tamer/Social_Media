using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class InteractionUnitOFWork : IInteractionUnitOFWork
    {
        public InteractionUnitOFWork(IInteractionNotificationByCommentServices InteractionNotificationByCommentServices, IInteractionNotificationByStoryServices InteractionNotificationByStoryServices, IInteractionNotificationByPostServices InteractionNotificationByPostServices, IInteractionWithCommentServices InteractionWithCommentServices, IInteractionWithStoryServices InteractionWithStoryServices, IInteractionWithPostServices InteractionWithPostServices)
        {
            this.InteractionNotificationByCommentServices = InteractionNotificationByCommentServices;
            this.InteractionNotificationByStoryServices = InteractionNotificationByStoryServices;
            this.InteractionNotificationByPostServices = InteractionNotificationByPostServices;
            this.InteractionWithCommentServices = InteractionWithCommentServices;
            this.InteractionWithStoryServices = InteractionWithStoryServices;
            this.InteractionWithPostServices = InteractionWithPostServices;
        }

        public IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }

        public IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }

        public IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }

        public IInteractionWithCommentServices InteractionWithCommentServices { get; }

        public IInteractionWithStoryServices InteractionWithStoryServices { get; }

        public IInteractionWithPostServices InteractionWithPostServices { get; }
    }
}
