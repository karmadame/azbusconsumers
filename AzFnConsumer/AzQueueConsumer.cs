using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFnConsumer;

public class AzQueueConsumer(ILogger<AzQueueConsumer> logger)
{
    [Function(nameof(AzQueueConsumer))]
    public async Task Run(
        [ServiceBusTrigger("testazureservicequeue", Connection = "AzBusConnection" )]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.AbandonMessageAsync(message);
    }
}