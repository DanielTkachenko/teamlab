using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamlab
{
    class MyTask : IRunable
    {
        public virtual void Run()
        {
            Console.WriteLine("MyTask is running!");
            Console.WriteLine(DateTime.Today);
            Console.WriteLine("MyTask is stopped!");
        }
    }

    class MyTaskOne : MyTask
    {
        public override void Run()
        {
            Console.WriteLine("MyTaskOne is running!");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("MyTaskOne is running!");
        }
    }

    class MyTaskTwo : MyTask
    {
        public override void Run()
        {
            Console.WriteLine("MyTaskTwo is running!");
            Console.WriteLine(DateTime.UtcNow);
            Console.WriteLine("MyTaskTwo is running!");
        }
    }
}
