﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;

using CBApp3.Domain.Models;

namespace CBApp3.Domain.Services
{
    public class Parser
    {
        public string GetPageText(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Файл не был загружен.");
            }
        }
        public Task<string> GetPageTextAsync(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return sr.ReadToEndAsync();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Файл не был загружен.");
            }
        }

        public EntitiesList ParsePage(string pagePath, bool isGroup)
        {
            string PageText = GetPageText(pagePath);

            EntitiesList entitiesList = new EntitiesList(isGroup,
                (isGroup ? "Список групп" : "Список преподавателей"),
                DateTime.Now.ToString("ddMMyyyy HHmm"));

            List<string> names = new List<string>();
            List<string> dates = new List<string>();
            List<Task<Models.Entity>> tables = new List<Task<Models.Entity>>();

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(req => req.Content(PageText)).Result;
            var doc = document.DocumentElement;

            foreach (var elem in doc.GetElementsByTagName("tbody"))
            {
                tables.Add(Task<Models.Entity>.Run(() => ParseTable(elem)));
            }
            foreach (var elem in doc.GetElementsByTagName("h2"))
            {
                names.Add(elem.Text());
            }
            foreach (var elem in doc.GetElementsByTagName("h3"))
            {
                dates.Add(elem.Text());

            }

            for (int i = 0; i < names.Count; i++)
            {
                Models.Entity entity = tables[i].Result;

                entity.Name = names[i];
                entity.Date = dates[i];
                entity.Number = i;

                entitiesList.Entities.Add(entity);
            }

            return entitiesList;
        }
        public async Task<EntitiesList> ParsePageAsync(string pagePath, bool isGroup)
        {
            string PageText = GetPageText(pagePath);

            EntitiesList entitiesList = new EntitiesList(isGroup,
                (isGroup ? "Список групп" : "Список преподавателей"),
                DateTime.Now.ToString("ddMMyyyy HHmm"));

            List<string> names = new List<string>();
            List<string> dates = new List<string>();
            List<Task<Models.Entity>> tables = new List<Task<Models.Entity>>();

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(PageText));
            var doc = document.DocumentElement;

            foreach (var elem in doc.GetElementsByTagName("tbody"))
            {
                tables.Add(Task<Models.Entity>.Run(() => ParseTable(elem)));
            }
            foreach (var elem in doc.GetElementsByTagName("h2"))
            {
                names.Add(elem.Text());
            }
            foreach (var elem in doc.GetElementsByTagName("h3"))
            {
                dates.Add(elem.Text());

            }

            for (int i = 0; i < names.Count; i++)
            {
                Models.Entity entity = tables[i].Result;

                entity.Name = names[i];
                entity.Date = dates[i];
                entity.Number = i;

                entitiesList.Entities.Add(entity);
            }

            //Console.WriteLine("Парсинг завершён!");
            return entitiesList;
        }

        public Models.Entity ParseTable(IElement table)
        {
            Models.Entity entity = new Models.Entity();
            string[] lesson = new string[2];
            int serialIndex = 0;
            int dayIndex = 0;
            bool flag = true;

            try
            {
                foreach (var row in table.ChildNodes)
                {
                    //пропуск пустых контейнеров от удвоенных ячеек
                    if (row.GetElementCount() == 0) continue;
                    //заполнение заголовков, дней недели
                    else if (row.GetElementCount() == 7 ||
                        row.GetElementCount() == 6)
                    {
                        //пропуск пустой ячейки
                        if (row.Index() == 0) continue;
                        foreach (var elemOfRow in row.ChildNodes)
                        {
                            //пропуск пустых нечётных ячеек
                            if (elemOfRow.Index() % 2 == 0) continue;
                            //пропуск первой ячейки с №
                            if (elemOfRow.Index() == 1) continue;

                            //создание дней по заголовкам столбцов с названиями дней и датами
                            entity.Days.Add(new Day()
                            {
                                Date = elemOfRow.Text().TrimEnd().TrimStart(),
                                Number = serialIndex++
                            });
                        }

                        //сбросить счётчик после нумерации дней
                        serialIndex = 0;
                        continue;
                    }
                    //пропустить заголовки столбцов: "предмет" и "ауд." 
                    else if (row.GetElementCount() == 12 ||
                        row.GetElementCount() == 10) continue;
                    else
                    {
                        //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

                        //переменная отсчитывающая столбцы
                        //сбрасывается в каждой новой строке с занятиямм
                        dayIndex = 0;
                        //переменная отсчитывает строки для нумерации занятий
                        serialIndex++;

                        foreach (var elemOfRow in row.ChildNodes)
                        {
                            if (elemOfRow.Index() == 1) continue;
                            else if (elemOfRow.Index() % 2 == 0) continue;

                            if (elemOfRow.Text() != "")
                            {
                                if (flag)
                                {
                                    //Console.WriteLine("Предмет: " + elemOfRow.Text().TrimEnd().TrimStart());

                                    lesson = new string[2];
                                    lesson[0] = elemOfRow.Text().TrimEnd().TrimStart();

                                    flag = false;
                                }
                                else
                                {
                                    //Console.WriteLine("Кабинет: " + elemOfRow.Text().TrimEnd().TrimStart());

                                    lesson[1] = elemOfRow.Text().TrimEnd().TrimStart();
                                    entity.Days[dayIndex++].Lessons.Add(lesson);

                                    flag = true;
                                }
                            }

                            //Console.Write(elemOfRow.Text());
                        }
                    }

                    //Console.WriteLine("Кол-во в row: " + row.GetElementCount() + ", индекс: " + row.Index());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Исключение: " + e.Message);
            }

            return entity;
        }
    }
}