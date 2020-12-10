using MongoDB.Bson;
using System;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
{
    public class UserView
    {
        public ObjectId Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
