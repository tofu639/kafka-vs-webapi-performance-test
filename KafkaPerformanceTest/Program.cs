using Confluent.Kafka;

class KafkaProducer
{
    public static void ProduceMessage(string topic, string message)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            producer.Produce(topic, new Message<Null, string> { Value = message });
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        KafkaProducer.ProduceMessage("test-topic", "Hello from Kafka");
        Console.WriteLine("Message sent to Kafka.");
    }
}