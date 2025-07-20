using System;
using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "chat-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        try
        {
            consumer.Subscribe("chat-topic");

            Console.WriteLine("Kafka Chat Consumer started. Listening...");

            while (true)
            {
                var cr = consumer.Consume();
                Console.WriteLine($"Friend: {cr.Message.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            consumer.Close(); 
        }
    }
}
