using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF;
using System.ServiceModel.Description;

namespace WCFHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(BookService), new Uri("http://localhost:8733/WCF/Books"));
            host.AddServiceEndpoint(typeof(IBook), new WSDualHttpBinding(), "");
            ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
            serviceBehavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(serviceBehavior);
            try
            {
                host.Open();
                Console.WriteLine("Service started");
                Console.ReadKey();
                host.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
