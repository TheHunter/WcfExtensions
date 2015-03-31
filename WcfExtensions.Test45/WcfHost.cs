using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfJsonFormatter;
using WcfJsonFormatter.Ns;

namespace WcfExtensions.Test45
{

    class WcfHost
    {
        static void Main(string[] args)
        {
            WcfHost host = new WcfHost();
            host.Run();
        }

        private void Run()
        {
            Console.WriteLine("Run a ServiceHost via programmatic configuration...");
            Console.WriteLine();

            string baseAddress = "http://" + Environment.MachineName + ":8000/Service.svc";
            Console.WriteLine("BaseAddress: {0}", baseAddress);

            using (ServiceHost serviceHost = new ServiceHost(typeof(Service)))
            {
                Console.WriteLine("Opening the host");
                serviceHost.Open();

                try
                {
                    var service = new Service();
                    Console.WriteLine(service);
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error on initializing the service host.");
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine();
                Console.ReadLine();
                serviceHost.Close();
            }
        }
    }
}
