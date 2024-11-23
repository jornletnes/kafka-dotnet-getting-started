// See https://aka.ms/new-console-template for more information

using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;

class Streams
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        var config = new StreamConfig<StringSerDes, StringSerDes>();
        config.ApplicationId = "test-app";
        config.BootstrapServers = "localhost:9092";

        StreamBuilder builder = new StreamBuilder();

        var kstream = builder.Stream<string, string>("stream");
        var ktable = builder.Table("table", InMemory.As<string, string>("table-store"));

        kstream.Join(ktable, (v, v1) => $"{v}-{v1}")
            .To("join-topic");

        Topology t = builder.Build();
        KafkaStream stream = new KafkaStream(t, config);

        Console.CancelKeyPress += (o, e) => { stream.Dispose(); };

        await stream.StartAsync();
    }
}