using System;
using System.Collections.Generic;

namespace CBApp3.Domain.Models
{
    [Serializable]
    public class EntitiesList
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsGroup { get; set; }
        public List<Entity> Entities { get; set; }

        public EntitiesList()
        {
            this.Name = "";
            this.Date = "";
            this.IsGroup = true;
            this.Entities = new List<Entity>();
        }
        public EntitiesList(string name, bool isGroup)
        {
            this.Name = name;
            this.Date = "";
            this.IsGroup = isGroup;
            this.Entities = new List<Entity>();
        }
        public EntitiesList(string name, bool isGroup, string date)
        {
            this.Name = name;
            this.Date = date;
            this.IsGroup = isGroup;
            this.Entities = new List<Entity>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
