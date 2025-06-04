using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public interface IFileManager
    {
        public string FolderPath { get;}
        public string FilePath { get;}

        public void SelectFile(string name);
        public void SelectFolder(string path);
    }
}
