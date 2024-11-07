using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Tema_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Window example = new Window())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}