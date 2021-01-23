using System;
using System.Collections.Generic;
using System.Text;

namespace CBApp3.Domain.Models
{
    public class Day
    {
        public string Date { get; set; }
        public string[][] Lessons { get; set; }

        public Day()
        {
            this.Date = "";
            this.Lessons.Initialize();
        }

        public override string ToString()
        {
            return Date;
        }
    }
}
