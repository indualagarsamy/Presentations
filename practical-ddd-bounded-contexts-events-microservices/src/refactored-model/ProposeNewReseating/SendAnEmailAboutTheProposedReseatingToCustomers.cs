using System.Threading.Tasks;
using Booking.Commands;
using NServiceBus;
using NServiceBus.Logging;

class SendAnEmailAboutTheProposedReseatingToCustomers :
    IHandleMessages<NotifyCustomerAboutProposedReseating>
{
    public Task Handle(NotifyCustomerAboutProposedReseating message, IMessageHandlerContext context)
    {
        logger.Info("Sending an email to customers...received the NotifyCustomerAboutProposedSeating command");
        return Task.CompletedTask;
    }

    static ILog logger = LogManager.GetLogger<SendAnEmailAboutTheProposedReseatingToCustomers>();
}