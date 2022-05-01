using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;

namespace teamlab
{
    class Manager
    {
        Schedule scheduler = new Schedule();

        public void Parse(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlElement? xRoot = xmlDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement task in xRoot)
                {
                    XmlNodeList? task_childs = task.ChildNodes;
                    /*Assembly asm = Assembly.LoadFrom(task.Attributes.GetNamedItem("assemblyname").Value);
                    Type? taskType = asm.GetType(task.Attributes.GetNamedItem("classname").Value);*/

                    if (task_childs != null)
                    {
                        string typeName = Assembly.GetExecutingAssembly().GetName().Name + "." + 
                            task.Attributes.GetNamedItem("classname").Value;
                        
                        MyTask tmp = (MyTask)Assembly.GetExecutingAssembly().CreateInstance(typeName);
                        foreach (XmlElement attrs in task_childs)
                        {
                            if (attrs.Name == "schedule")
                            {
                                foreach (XmlNode sh in attrs.ChildNodes)
                                {
                                    string schedule_str = sh.InnerText;
                                    if (schedule_str == "OnStart")
                                    {
                                        scheduler.AddOnStart(tmp);
                                    }
                                    else if (schedule_str.Contains("OnTime"))
                                    {
                                        int hours = Int32.Parse(schedule_str.Substring(schedule_str.IndexOf(";") + 1, 2));
                                        int mins = Int32.Parse(schedule_str.Substring(schedule_str.IndexOf(":") + 1, 2));
                                        DateTime time = new DateTime();
                                        time = time.AddHours(hours);
                                        time = time.AddMinutes(mins);
                                        scheduler.AddOnTime(tmp, time);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RunSchedule()
        {
            scheduler.Run();
        }
    }
}
