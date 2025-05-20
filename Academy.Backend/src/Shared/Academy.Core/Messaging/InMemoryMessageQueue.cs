
using Academy.Core.Abstractions;
using System.Threading.Channels;

namespace Academy.Core.Messaging
{
    public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
    {
        private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

        public async Task<TMessage> ReadAsync(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }

        public async Task WriteAsync(TMessage message, CancellationToken cancellationToken)
        {
            await _channel.Writer.WriteAsync(message, cancellationToken);
        }
    }
}
