// See https://aka.ms/new-console-template for more information

using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.Processors;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;

class Streams
{
    static async Task Main(string[] args)
    {
        var config = new StreamConfig<StringSerDes, StringSerDes>();
        config.ApplicationId = "test-app2";
        config.BootstrapServers = "localhost:9092";

        StreamBuilder builder = new StreamBuilder();

        var kstream = builder.Stream<string, string>("purchases");
        Action<string,string> PeekAction = (key, value) => 
        {
            Console.WriteLine($"Payload: {key} - {value}");
        };
        string ToAction(string key, string value, IRecordContext recordContext)
        {
            Console.WriteLine($"Payload: {value} - {recordContext}");
            return value + "-" + recordContext;
        }

        kstream.Peek(PeekAction);
        kstream.To(ToAction);

        Topology t = builder.Build();
        KafkaStream stream = new KafkaStream(t, config);

        Console.CancelKeyPress += (o, e) => { stream.Dispose(); };

        await stream.StartAsync();
    }
}