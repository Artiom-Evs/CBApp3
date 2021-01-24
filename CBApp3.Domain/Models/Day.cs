using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp3.Domain.Models
{
    public class Day
    {
        public string Date { get; set; }public int Number { get; set; }
        public List<string[]> Lessons { get; set; }

        public Day()
        {
            this.Date = "";
            this.Lessons = new List<string[]>();
        }

        public override string ToString()
        {
            return Date;
        }
    }
}
