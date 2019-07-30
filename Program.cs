using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


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
                tarring.StartInfo.Arguments = $"-xf -C {destinationDirectory}";
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
                byte[] bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while(numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    if(n == 0)
                    {
                        break;
                    }

                    numBytesRead +=n;
                    numBytesToRead -=n;
                }

                numBytesToRead = bytes.Length;
                
                stream.WriteAsync(System.Text.Encoding.UTF8.GetString(bytes).ToCharArray(), 0, numBytesToRead);
            }
        }
    }
}
