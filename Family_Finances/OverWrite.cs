using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Family_Finances
{
    class OverWrite
    {
        public static void OverWrite_Costs(string[] line, int a)
        {
            int count = 0;
            if (line.Length == 1)
            {
                File.WriteAllText("list_costs.txt", " ");
            }
            else if (line.Length == 0)
            {
                File.WriteAllText("list_costs.txt", " ");
            }
            else if (line.Length > 1)
            {
                for (int i = 0; i < line.Length - 1; i++)//первый проход - записывает элементы до выбранного
                {


                    //if (i == line.Length - 1)
                    //{
                    //    break;
                    //}
                    if (i == a)
                    {
                        break;
                    }

                    string lst = line[i];
                    using (StreamWriter sw = new StreamWriter("list_costs.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(lst);
                    }
                    count += 1;//сколько элементов добавилось
                }
                for (int i = a + 1; i < line.Length - 1; i++)//второй проход - записывает элементы после выбранного
                {
                    string lst = line[i];
                    using (StreamWriter sw = new StreamWriter("list_costs.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(lst);
                    }
                    count += 1;
                }
                string[] line_new = File.ReadAllLines("list_costs.txt", Encoding.Default);//массив строк после добавления новых строчек
                File.Delete("list_costs.txt");
                for (int i = 0; i < line_new.Length; i++)//третий проход - удаляет файл, создает новый и записывает все элементы, кроме выбранного
                {
                    if (i <= count)
                    {
                        continue;
                    }
                    using (StreamWriter sw = new StreamWriter("list_costs.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(line_new[i]);//перезаписываю файл, удаляя лишние строки 
                    }
                }
            }
        }
        public static void OverWrite_Income(string[] line, int a)
        {
            int count = 0;
            if (line.Length == 1)
            {
                File.WriteAllText("list.txt", " ");
            }
            else if (line.Length == 0)
            {
                File.WriteAllText("list.txt", " ");
            }
            else if (line.Length > 1)
            {
                for (int i = 0; i < line.Length - 1; i++)//первый проход - записывает элементы до выбранного
                {


                    //if (i == line.Length - 1)
                    //{
                    //    break;
                    //}
                    if (i == a)
                    {
                        break;
                    }

                    string lst = line[i];
                    using (StreamWriter sw = new StreamWriter("list.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(lst);
                    }
                    count += 1;//сколько элементов добавилось
                }
                for (int i = a + 1; i < line.Length - 1; i++)//второй проход - записывает элементы после выбранного
                {
                    string lst = line[i];
                    using (StreamWriter sw = new StreamWriter("list.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(lst);
                    }
                    count += 1;
                }
                string[] line_new = File.ReadAllLines("list.txt", Encoding.Default);//массив строк после добавления новых строчек
                File.Delete("list.txt");
                for (int i = 0; i < line_new.Length; i++)//третий проход - удаляет файл, создает новый и записывает все элементы, кроме выбранного
                {
                    if (i <= count)
                    {
                        continue;
                    }
                    using (StreamWriter sw = new StreamWriter("list.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(line_new[i]);//перезаписываю файл, удаляя лишние строки 
                    }
                }
            }
        }
    }
}
