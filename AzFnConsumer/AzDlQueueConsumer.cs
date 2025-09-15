using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFnConsumer;

public class AzDlQueueConsumer
{
    private readonly ILogger<AzDlQueueConsumer> _logger;

    public AzDlQueueConsumer(ILogger<AzDlQueueConsumer> logger)
    {
        _logger = logger;
    }

    [Function(nameof(AzDlQueueConsumer))]
    public async Task Run(
        [ServiceBusTrigger("testazureservicequeue/$DeadLetterQueue", Connection = "AzDlBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}