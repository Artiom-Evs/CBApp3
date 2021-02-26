using System;
using System.Collections.Generic;

namespace CBApp3.Domain.Models
{
    [Serializable]
    public class Day
    {
        public string Date { get; set; }public int Number { get; set; }
        public List<string[]> Lessons { get; set; }

        public Day()
        {
            this.Date = "";
            this.Lessons = new List<string[]>();
        }

        public Day(string name)
        {
            this.Date = name;
            this.Lessons = new List<string[]>();
        }

        public override string ToString()
        {
            return this.Date;
        }
    }
}
