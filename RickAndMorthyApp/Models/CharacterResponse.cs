using System;
using System.Collections.Generic;

namespace RickAndMorthyApp.Models
{
    public class CharacterResponse
    {
        public ServiceInfo Info { get; set; }
        public List<Character> Results { get; set; }
    }

    public class ServiceInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public CharacterLocation Origin { get; set; }
        public CharacterOrigin Location { get; set; }
        public string Image { get; set; }
        public string[] Episode { get; set; }
        public string Url { get; set; }
        public string Created { get; set; }
    }

    public class CharacterLocation
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class CharacterOrigin
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
