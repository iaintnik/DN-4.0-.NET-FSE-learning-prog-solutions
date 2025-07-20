using System;
using System.Threading.Tasks;
using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        var producer = new ProducerBuilder<Null, string>(config).Build();

        Console.WriteLine("Kafka Chat Producer - Type messages. Type 'exit' to quit.");

        try
        {
            while (true)
            {
                Console.Write("You: ");
                var input = Console.ReadLine();
                if (input == "exit") break;

                // Since we're using Main without async in C# 7.3, call .GetAwaiter().GetResult()
                producer
                    .ProduceAsync("chat-topic", new Message<Null, string> { Value = input })
                    .GetAwaiter()
                    .GetResult();

                Console.WriteLine($"Sent: {input}");
            }

            producer.Flush(TimeSpan.FromSeconds(10));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            producer.Dispose(); // Clean shutdown
        }
    }
}
