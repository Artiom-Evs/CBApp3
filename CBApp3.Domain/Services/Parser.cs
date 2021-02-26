using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CBApp3.Domain.Models;

namespace CBApp3.Domain.Services
{
    public static class Parser
    {
        public static EntitiesList ParsePage(Task<string> pageText, bool isGroup)
        {
            return ParsePage(pageText.Result, isGroup);
        }

        public static EntitiesList ParsePage(string pageText, bool isGroup)
        {
            EntitiesList entitiesList = new EntitiesList(
                (isGroup ? "Список групп" : "Список преподавателей"),
                isGroup, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

            Queue<Task<Entity>> queue = new Queue<Task<Entity>>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageText);

            //выборка из content-раздела страницы элементов 
            //с тегами h2, h3 и table и создание перечислителя
            var pc = doc.DocumentNode
                .SelectSingleNode("//div[@class='col-sm-9 content']//div")
                .ChildNodes
                .Where(node => node.Name == "h2" || node.Name == "h3" || node.Name == "table")
                .ToList()
                .GetEnumerator();
            
            pc.MoveNext();

            //генерация очереди задач по разбору таблиц
            while (pc.Current != null)
            {
                string elemName = pc.Current.InnerText.Replace("Преподаватель - ", "");
                pc.MoveNext();
                string elemDate = pc.Current.InnerText;
                pc.MoveNext();

                //проверка на наличие таблицы
                if (pc.Current.Name == "table")
                {
                    /*
                     * как я понял, при окончании enum вызывается Dispose
                     * и потому ссылка напрямую взятая из enum 
                     * после окончания последовательности ведёт на null
                     */

                    HtmlNode elemTable = pc.Current;

                    queue.Enqueue(Task<Entity>.Run(() =>
                    {
                        return new Entity()
                        {
                            Name = elemName,
                            Date = elemDate,
                            Days = ParseTable(elemTable)
                        };
                    }));
                    
                    pc.MoveNext();
                }
                else
                {
                    queue.Enqueue(Task<Entity>.Run(() =>
                    {
                        return new Entity()
                        {
                            Name = elemName,
                            Date = elemDate,
                            Days = null
                        };
                    }));
                }
            }

            //выборка из очереди задач готовых элементов
            while (queue.Count > 0)
            {
                entitiesList.Entities.Add(queue.Dequeue().Result);
            }

            return entitiesList;
        }

        private static List<Day> ParseTable(HtmlNode table)
        {
            List<List<string>> grid = new List<List<string>>();

            //выборка всех строк с нечётным количеством элементов
            //строка с заголовками отбрасывается - по элементам она чётная
            grid = table.Element("tbody")
                .Elements("tr")
                .Where(tr => tr.Elements("td").Count() % 2 == 1)
                .Select(tr => tr.Elements("td")
                .Select(td => td.InnerText.Trim().Replace("&nbsp;", " ")).ToList())
                .ToList();
            
            return CreateListDays(grid);
        }
        
        private static List<Day> CreateListDays(List<List<string>> grid)
        {
            List<Day> days = new List<Day>();
            
            try
            {
                //если таблица пуста - вернуть null
                if (grid.Count == 1) return null;

                //в первой строке со второго элемента записаны даты дней
                //по ним создаются объекты представляющие дни в таблице
                for (int i = 1; i < grid[0].Count; i++)
                {
                    days.Add(new Day(grid[0][i]));
                }

                //удаляется уже ненужная строка
                grid.Remove(grid[0]);

                //строки таблицы разбираются по объектам дням
                foreach (var row in grid)
                {
                    for (int i = 1, j = 0; i < row.Count; i++)
                    {
                        Day day = new Day();
                        //в строке таблицы при 6 днях 13 элементов, тут вычисляется индекс дня
                        j = ((i + (i % 2)) / 2) - 1;

                        days[j].Lessons.Add(new string[2] { row[i], row[++i] });
                    }
                }
            }
            catch
            {
                //обобщающая ошибка для данного метода
                //throw new Exception("Не удалось разобрать таблицу");

                return null;
            }

            return days;
        }

    }
}