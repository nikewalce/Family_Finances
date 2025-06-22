using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Family_Finances
{
    static class Program
    {
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (!File.Exists("user.txt"))
            {
                var user = File.Create("user.txt");
                user.Close();
            }
            if (!File.Exists("list.txt"))
            {
                var lst = File.Create("list.txt");
                lst.Close();
            }
            if (!File.Exists("Categories_Income.txt") && !File.Exists("Categories_Costs.txt"))
            {
                var cat_c = File.Create("Categories_Costs.txt");
                cat_c.Close();
                var cat = File.Create("Categories_Income.txt");//создает файл
                cat.Close();//освобождает поток
            }
            
            if (!File.Exists("list_costs.txt"))
            {
                var lst_c = File.Create("list_costs.txt");
                lst_c.Close();
            }

            Application.Run(new Income());
        }
    }
}
