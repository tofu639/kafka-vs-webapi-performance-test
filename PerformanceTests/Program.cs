using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Confluent.Kafka;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class PerformanceTest
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string WebApiUrl = "http://localhost:5208/weatherforecast";
    private const string KafkaTopic = "test-topic";

    [Benchmark]
    public async Task TestWebApi()
    {
        var content = new StringContent("\"Hello from Web API\"", Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(WebApiUrl, content);
    }

    [Benchmark]
    public void TestKafka()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            producer.Produce(KafkaTopic, new Message<Null, string> { Value = "Hello from Kafka" });
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<PerformanceTest>();
    }
}