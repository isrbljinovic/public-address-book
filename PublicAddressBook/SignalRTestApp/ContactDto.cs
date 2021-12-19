using System;

namespace SignalRTestApp
{
    public class ContactDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}