using System;
using System.Diagnostics;
using System.IO;

namespace Receiver
{
    class Program
    {
        static string DocumentsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DOC");

        static void Main(string[] args)
        {
            if (args.Length > 0) 
            {
                string cedula = args[0];
                string path = Path.Combine(DocumentsPath, cedula + ".png");
                if (File.Exists(path))
                    Process.Start("cmd", $"/c {path}");
                else
                    Console.WriteLine($"El archivo en la ruta \"{path}\" No existe");
            }
        }
    }
}
