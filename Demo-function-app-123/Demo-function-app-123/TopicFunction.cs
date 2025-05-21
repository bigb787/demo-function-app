using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Demo_function_app_123;

public class TopicFunction
{
    private readonly ILogger<TopicFunction> _logger;

    public TopicFunction(ILogger<TopicFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(TopicFunction))]
    public async Task Run(
        [ServiceBusTrigger("mytopic", "mysubscription", Connection = "ServiceBusConnection")]
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