using System;
using System.Collections.Generic;

namespace CBApp3.Domain.Models
{
    [Serializable]
    public class Entity
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public List<Day> Days { get; set; }

        public Entity()
        {
            this.Name = "";
            this.Date = "";
            this.Days = new List<Day>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
