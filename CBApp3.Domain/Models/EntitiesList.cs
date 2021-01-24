using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

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
            this.Date = "";
            this.IsGroup = true;
            this.Entities = new List<Entity>();
        }
        public EntitiesList(bool isGroup, string name, string date)
        {
            this.Name = name;
            this.Date = date;
            this.IsGroup = isGroup;
            this.Entities = new List<Entity>();
        }

        public Task Save(string path)
        {
            return Task.Run(() => GetXDocument().Save(path));
        }
        private XDocument GetXDocument()
        {
            XElement entitiesList = new XElement("entities");

            entitiesList.Add(
                new XAttribute("name", this.Name),
                new XAttribute("date", this.Date),
                new XAttribute("isGroup", this.IsGroup.ToString()));

            foreach (var entity in this.Entities)
            {
                var xEntity = new XElement("entity");

                xEntity.Add(new XAttribute("name", entity.Name),
                    new XAttribute("date", entity.Date),
                    new XAttribute("number", entity.Number));

                foreach (var day in entity.Days)
                {
                    var xDay = new XElement("day");

                    xDay.Add(new XAttribute("date", day.Date),
                        new XAttribute("count", day.Lessons.Count.ToString()));

                    foreach (var lesson in day.Lessons)
                    {
                        xDay.Add(new XElement("subject", lesson[0],
                            new XAttribute("room", lesson[1])));
                    }

                    xEntity.Add(xDay);
                }

                entitiesList.Add(xEntity);
            }

            return new XDocument(entitiesList);
        }
        public Task Load(string path)
        {
            return Task.Run(() => SetXDocument(path));
        }
        private void SetXDocument(string path)
        {
            XElement xEntities = XDocument.Load(path).Element("entities");

            this.Name = xEntities.Attribute("name").Value;
            this.Date = xEntities.Attribute("date").Value;
            this.IsGroup = bool.Parse(xEntities.Attribute("isGroup").Value);

            foreach (var xEntity in xEntities.Elements("entity"))
            {
                Entity entity = new Entity()
                {
                    Name = xEntity.Attribute("name").Value,
                    Date = xEntity.Attribute("date").Value,
                    Number = int.Parse(xEntity.Attribute("number").Value)
                };


                foreach (var xDay in xEntity.Elements("day"))
                {
                    Day day = new Day()
                    {
                        Date = xDay.Attribute("date").Value
                    };

                    foreach (var subject in xDay.Elements("subject"))
                    {
                        day.Lessons.Add(new string[]
                        {
                            subject.Value,
                            subject.Attribute("room").Value
                        });
                    }

                    entity.Days.Add(day);
                }

                this.Entities.Add(entity);
            }
        }

        public override string ToString()
        {
            return Date;
        }
    }
}
