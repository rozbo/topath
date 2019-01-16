using System;
using System.IO;
using System.Text;

namespace topath
{
    internal class Program
    {
        private static string fileFullPath;
        private static string cmdName;

        private static void initParam(string[] args)
        {
            String filePath="";
            if (args.Length == 1)
            {
                filePath = args[0];
            }
            else if(args.Length==2)
            {
                cmdName = args[0];
                filePath = args[1];
            }
            else
            {
                throw new Exception("useage: topath [cmd] [filePath]");
            }

            fileFullPath = Path.GetFullPath(filePath);

        }


        private static void checkFile()
        {
            if (!File.Exists(fileFullPath))
            {
                throw new Exception("target file is not exsit!");
            }

            if (String.IsNullOrEmpty(cmdName))
            {
                cmdName = Path.GetFileNameWithoutExtension(fileFullPath);
            }
        }

        private static void coreDeal()
        {
            string cmdFile = $@"D:\Env\bin\{cmdName}.cmd";
            if (File.Exists(cmdFile))
            {
                throw new Exception("target cmd is already exsit");
            }
            var cmd = $"@echo off\r\n" +
                      $"\"{fileFullPath}\" %*";
            var fileStream = File.Create(cmdFile);
            var cmdByte = Encoding.ASCII.GetBytes(cmd);
            fileStream.Write(cmdByte, 0, cmdByte.Length);
            fileStream.Flush();
            fileStream.Close();
        }

        private static void Main(string[] args)
        {
            try
            {
                initParam(args);
                checkFile();
                coreDeal();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.ReadKey();
            } 
        }
    }
}