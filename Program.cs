using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace TarApp
{
    class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sourceDirectory = args[0];
            var destinationDirectory = args[1];

            if(!File.Exists(sourceDirectory))
            {
                Console.WriteLine("Invalid Path!!");
                return 1;
            }

            using(Process tarring = new Process())
            {
                tarring.StartInfo.FileName = "tar";
                tarring.StartInfo.Arguments = $"-xf - -C {destinationDirectory}";
                tarring.StartInfo.RedirectStandardInput = true;
                tarring.StartInfo.UseShellExecute = false;
                tarring.Start();
                DownloadToFile(sourceDirectory, tarring.StandardInput);
            }
            return 0;
        }

        private static void DownloadToFile(string sourceDirectory, StreamWriter stream)
        {
            using(FileStream fs = File.Open(sourceDirectory, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[1024 * 1000];
                int len;
                while( (len = fs.Read(bytes, 0, bytes.Length)) > 0)
                {
                    stream.Write(stream.Encoding.GetChars(bytes,0,len));
                }
            }
        }
    }
}
