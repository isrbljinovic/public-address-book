using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRTestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Listening started...");

            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/contacts")
                .Build();

            connection.On<ContactDto>("CreatedOne",
                (contact => Console.WriteLine($"Created new contact: {JsonSerializer.Serialize(contact)}")));

            connection.On<IEnumerable<ContactDto>>("CreatedMultiple",
                (contacts => Console.WriteLine($"Created multiple contacts: {JsonSerializer.Serialize(contacts)}")));

            connection.On<string>("DeletedOne",
                contactName => Console.WriteLine($"Deleted {contactName} from contacts"));

            connection.On<ContactDto>("UpdatedContact",
                contact => Console.WriteLine($"Contact updated {JsonSerializer.Serialize(contact)}"));

            connection.StartAsync().GetAwaiter();

            Console.ReadLine();
        }
    }
}