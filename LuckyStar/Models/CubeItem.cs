using System;

namespace LuckyStar.Models
{
    public class CubeItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CubeItem()
        {
        }

        public CubeItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}