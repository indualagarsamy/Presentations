using System.Threading.Tasks;
using Booking.Commands;
using NServiceBus;
using NServiceBus.Logging;

class SendAnEmailAboutTheProposedRebookingToCustomers :
    IHandleMessages<NotifyCustomerAboutProposedRebooking>
{
    public Task Handle(NotifyCustomerAboutProposedRebooking message, IMessageHandlerContext context)
    {
        logger.Info("Sending an email to customers...received the NotifyCustomerAboutProposedRebooking command");
        return Task.CompletedTask;
    }

    static ILog logger = LogManager.GetLogger<SendAnEmailAboutTheProposedRebookingToCustomers>();
}