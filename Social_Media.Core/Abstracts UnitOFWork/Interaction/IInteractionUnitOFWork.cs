using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IInteractionUnitOFWork
    {
        public IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }
        public IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }

        public IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }

        public IInteractionWithCommentServices InteractionWithCommentServices { get; }
        public IInteractionWithStoryServices InteractionWithStoryServices { get; }

        public IInteractionWithPostServices InteractionWithPostServices { get; }
    }
}
