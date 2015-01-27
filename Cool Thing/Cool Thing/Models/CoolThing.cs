using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cool_Thing.Models
{
    public class CoolThing
    {
        public ICollection<Description> Descriptions { get; set; }
        public ICollection<User> Users { get; set; }

        public CoolThing()
        {
            Descriptions = new List<Description>();
            Users = new List<User>();

            Users.Add(new User { Name = "Thomas Parrish", Username = "tparrish" });
            Users.Add(new User { Name = "Phil Jones", Username = "pjones" });
            Users.Add(new User { Name = "John Bobbit", Username = "cantfindmything" });
            Users.Add(new User { Name = "Carlos Santana", Username = "guitargod" });
            Users.Add(new User { Name = "Chuck Norris", Username = "mytearscurecancer" });
            Users.Add(new User { Name = "Sarah Palin", Username = "onecrazychick" });
            Users.Add(new User { Name = "Barac Obama", Username = "imtheprezyo" });

            Descriptions.Add(new Description { Username = "pjones", Body = "It's a thing!", StatusCode = 101 });
            Descriptions.Add(new Description { Username = "guitargod", Body = "Cool Runnings", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "tparrish", Body = "Alice is hot!", StatusCode = 103 });
            Descriptions.Add(new Description { Username = "cantfindmything", Body = "I love Alice!", StatusCode = 101 });
            Descriptions.Add(new Description { Username = "imtheprezyo", Body = "My care wins.", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "mytearscurecancer", Body = "Too bad I never cry!", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "imtheprezyo", Body = "Boehner is a noob!", StatusCode = 103 });
            Descriptions.Add(new Description { Username = "mytearscurecancer", Body = "DROPKICK!", StatusCode = 105 });
            Descriptions.Add(new Description { Username = "pjones", Body = "I love lamp.", StatusCode = 103 });
            Descriptions.Add(new Description { Username = "imtheprezyo", Body = "Donkey FTW!", StatusCode = 104 });
            Descriptions.Add(new Description { Username = "tparrish", Body = "I don't get these guys...", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "cantfindmything", Body = "WHERE DID IT GO?!", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "imtheprezyo", Body = "Lost and found..", StatusCode = 101 });
            Descriptions.Add(new Description { Username = "mytearscurecancer", Body = "I don't call the wrong number. You answer the wrong phone.", StatusCode = 101 });
            Descriptions.Add(new Description { Username = "cantfindmything", Body = "Someone said it's on the side of the road somewhere...", StatusCode = 103 });
            Descriptions.Add(new Description { Username = "onecrazychick", Body = "Ya' know..", StatusCode = 104 });
            Descriptions.Add(new Description { Username = "cantfindmything", Body = "And then I became a movie star!", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "guitargod", Body = "Oye Como Va!", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "imtheprezyo", Body = "Do you even know who I am?!", StatusCode = 101 });
            Descriptions.Add(new Description { Username = "tparrish", Body = "Learning to code is so much fun!", StatusCode = 103 });
            Descriptions.Add(new Description { Username = "onecrazychick", Body = "PETA hates me!", StatusCode = 104 });
            Descriptions.Add(new Description { Username = "mytearscurecancer", Body = "Jesus may have walked on water, but I can swim through land!", StatusCode = 105 });
            Descriptions.Add(new Description { Username = "guitargod", Body = "Put your lights on!", StatusCode = 102 });
            Descriptions.Add(new Description { Username = "tparrish", Body = "I quit.", StatusCode = 101 });
        }
    }

    public class Description
    {
        public string Username { get; set; }
        public string Body { get; set; }
        public int StatusCode { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
    }
}