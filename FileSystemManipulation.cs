using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FileSystemManipulation
    {
        internal static void Test()
        {
            //TestDirectory();
            //TestFileSystemInfo();
            //TestDriveInfo();
            TestFileSystemWatcher();
        }

static void TestFileSystemWatcher()

        {

            string folderName = @"C:\TestFolder";

            using (var fsw = new FileSystemWatcher(folderName))

            {

                fsw.NotifyFilter = NotifyFilters.FileName

                    | NotifyFilters.DirectoryName

                    | NotifyFilters.CreationTime

                    | NotifyFilters.Size

                    | NotifyFilters.LastWrite

                    | NotifyFilters.LastAccess

                    | NotifyFilters.Attributes

                    | NotifyFilters.Security;

                fsw.Error += (s, e) =>

                {

                    var ex = e.GetException();

                    if (ex != null)

                    {

                        Console.WriteLine($"Message: {ex.Message}\nStack Trace: {ex.StackTrace}");

                    }

                };

                fsw.Renamed += (s, e) =>

                {

                    Console.WriteLine($"Renamed:\n\tOld Name:{e.OldFullPath}\n\tNew Name:{e.FullPath}");

                };

                fsw.Deleted += (s, e) => Console.WriteLine($"Deleted:{e.FullPath}");

                fsw.Created += (s, e) => Console.WriteLine($"Created: {e.FullPath}");

                fsw.Changed += (s, e) =>

                {

                    if (e.ChangeType != WatcherChangeTypes.Changed) return;

                    Console.WriteLine($"Changed: {e.FullPath}");

                };

                fsw.Filter = "*.txt";

                fsw.IncludeSubdirectories = false;

                fsw.EnableRaisingEvents = true;

                Console.WriteLine("Press a key to terminate");

                Console.ReadKey();

            }

        }
        static void TestDirectory()
        {
            string folderName = @"C:\TestFolder";   // Absolute path
                                                    // @-quoted string are not parsed by  the compiler
            if(Directory.Exists(folderName))
            {
                Directory.Delete(path:folderName,recursive: true);
            }
            Directory.CreateDirectory(folderName);

            var currentAppFolder = "../../../";

            // returns the current working directory where the .exe is executed.
            //var currFolder = Directory.GetCurrentDirectory();
            //var anyDir = Environment.CurrentDirectory;

            //var files = Directory.GetFiles(path:currentAppFolder);
            //var counter = 1;
            //foreach (var file in files)
            //{
            //    Console.WriteLine($"{counter++}. {Path.GetFileName(file)}");
            //    Console.WriteLine($"\tFull path [{file}]");
            //}
            for(int i=1;i<11;i++)
            {
                var fileName = Path.Combine(folderName, "_fileName"+i+ ".txt");
                if(File.Exists(fileName))
                   File.Delete(fileName);
                File.Create(fileName).Close(); // touch filename create empty file.
            }

            var sourceFolder = "../../../";
            var files = Directory.GetFiles(path:sourceFolder,searchPattern:"*.cs");
            foreach (var file in files)
            {
                if(Path.GetExtension(file)==".cs")
                {
                    var destinationFileName = Path.Combine(folderName,Path.GetFileName(file));  
                    File.Copy(sourceFileName:file,destFileName:destinationFileName);
                }
            }
        }
        static void TestFileSystemInfo()
        {
            string folderName = @"C:\TestFolder";
            DirectoryInfo dInfo = new DirectoryInfo(folderName);
            FileInfo[] files = dInfo.GetFiles();
            foreach (var file in files)
            {
                Console.WriteLine("{0}, Size:{1},Extension: {2}, Directory:{3}", file.Name, file.Length, file.Extension, file.DirectoryName);

            }
        }
        static void TestDriveInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            StringBuilder sb = new StringBuilder();
            foreach(var drive in drives)
            {
                sb.AppendLine($"Drive Name: {drive.Name}")
                    .AppendLine($"Total Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB")
                    .AppendLine($"Available Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB")
                    .AppendLine($"Volume label: {drive.VolumeLabel}")
                    .AppendLine($"Drive details: {drive.DriveType}-{drive.DriveFormat}")
                    .AppendLine();
            }
            Console.WriteLine( sb.ToString() );
        }
    }
}
