using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.BooksReference;

namespace WPF
{
    internal class Callback : IBookCallback
    {
        MainWindow MainWindowHandle { get; }

        public Callback(MainWindow window)
        {
            MainWindowHandle = window;
        }

        public void ContainsResult(bool result)
        {
            MainWindowHandle.ContainsString = result.ToString();
        }
    }
}
