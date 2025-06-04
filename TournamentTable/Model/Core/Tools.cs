using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public static class Tools
    {
        public static void Print(string text)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tournament");
            Directory.CreateDirectory(folderPath); // Создаст папку, если её нет

            string filePath = Path.Combine(folderPath, "Print.txt");

            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(text);
            }
        }
    }
}
