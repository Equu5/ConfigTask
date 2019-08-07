using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            string path = Path.Combine(projectPath, "Config.txt");

            var config = new TxtReader(path);

            Console.WriteLine(config["id"]);
            Console.WriteLine(config["session"]["timeout"]);
            Console.WriteLine(config["session"]["server"]["1"]["port"]);
            Console.WriteLine(config["image"]["cat"]["width"]);
            Console.WriteLine(config["image"]["folder"]);

            Console.ReadKey();
        }
    }
}
