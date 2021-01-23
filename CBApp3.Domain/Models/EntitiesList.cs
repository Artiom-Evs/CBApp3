using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp3.Domain.Models
{
    public class EntitiesList
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsGroup { get; set; }
        public List<Entity> Entities { get; set; }

        public EntitiesList()
        {
            this.Name = "";
            this.Date = DateTime.Now.ToString("ddMMyyyy HHmm");
            this.IsGroup = true;
            this.Entities = new List<Entity>();
        }

        public override string ToString()
        {
            return Date;
        }
    }
}
