using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class TriggerFunction
{
    private readonly ILogger<TriggerFunction> _logger;

    public TriggerFunction(ILogger<TriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(TriggerFunction))]
    public async Task Run(
        [ServiceBusTrigger("mytopic", "mysubscription", Connection = "ServiceBusConnectionString")]
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