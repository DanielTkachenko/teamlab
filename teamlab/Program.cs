using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace teamlab
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = "D:\\tmp\\";
            Manager manager = new Manager();

            if (Directory.Exists(dirPath))
            {
                string[] files = Directory.GetFiles(dirPath, "*.service.config");

                if(files.Length != 0)
                {
                    foreach(var f in files)
                    {
                        manager.Parse(f);
                    }
                }
            }

            manager.RunSchedule();

        }
    }
}
