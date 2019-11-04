using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProductDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string dbName = "products.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string dbPath = Path.Combine(folderPath, dbName);
    }
}
