using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;

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
