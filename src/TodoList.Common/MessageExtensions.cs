using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace TodoList.Common
{
    public static class MessageExtensions
    {
        public static NetMQMessage PrepMessage(object command)
        {

            var assembly = command.GetType().Assembly;
            var type = command.GetType();
            var payload = JsonSerializer.SerializeToUtf8Bytes(command);

            var message = new NetMQMessage();
            message.Append(assembly.ToString());
            message.Append(type.ToString());
            message.Append(payload);
            return message;
        }

        public static async Task<object> Deserialize(NetMQMessage message,CancellationToken cancellationToken = default)
        {
            var assemblyString = message[0].ConvertToString();
            var typeString = message[1].ConvertToString();
            var payload = message[2].ToByteArray();
            var assembly = Assembly.Load(assemblyString);
            var commandType = assembly.GetType(typeString);

            var command = await JsonSerializer.DeserializeAsync(new MemoryStream(payload), commandType, new JsonSerializerOptions(),
                cancellationToken);
            return command;
        }
    }
}