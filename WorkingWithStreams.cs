using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkingWithStreams
    {
        internal static void Test()
        {
            //TestFileStreamObject();
            //TestMemoryStream();
            //TestBasedOperations();
            TestBinaryReaderWriter();
        }
        static void TestBinaryReaderWriter()
        {
            int num = 123;
            double d = 123.4;
            string text = "Hello world";
            string folder = @"C:\TestFolder";
            string fileName = "SampleFile.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using(BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(num);
                    writer.Write(d);
                    writer.Write(text);
                    writer.Flush();
                }
                 fs.Close();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            using(FileStream fs = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int x = reader.ReadInt32();
                    double y = reader.ReadDouble();
                    string s = reader.ReadString();
                    Console.WriteLine($"After int: {x} double: {y} string: {s}");
                }
                fs.Close();
            }
        }
        static void TestBasedOperations()
        {
            string folder = @"C:\TestFolder";
            string fileName = "SampleFile.txt";
            string filePath = Path.Combine(folder, fileName);
            Console.WriteLine("Enter content to be written on to the file");
            string content = Console.ReadLine();
            using(StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(content);
                writer.Flush();
            }
            Console.WriteLine("Content written to file. press a key to begin reading");
            Console.ReadKey();
            using(StreamReader reader = new StreamReader(filePath))
            {
                string line = string.Empty;
                int counter = 1;
                do
                {
                    line = reader.ReadLine();
                    Console.WriteLine($"line {counter++}. {line}");
                }while( !reader.EndOfStream );
                reader.Close();
            }
        }
        static void TestMemoryStream()
        {
            string content = "The quick brown fox jumps over the lazy dog.";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Seek(10, SeekOrigin.Begin);
            buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);
            content = Encoding.UTF8.GetString(buffer);
            Console.WriteLine($"Contents in the memory stream {content}");
            ms.Close();
        }
        static  void TestFileStreamObject()
        {
            string folder = @"C:\TestFolder";
            string fileName = "SampleFile.txt";
            string filePath = Path.Combine(folder, fileName);
            Console.WriteLine("Enter content to be written on to the file");
            string content = Console.ReadLine();

            using(FileStream fs1 = new FileStream(path:filePath,mode:FileMode.Create,access:FileAccess.ReadWrite)) 
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);
                fs1.Write(buffer:bytes, offset:0, count:bytes.Length);
                fs1.Flush();
                fs1.Close();
            }
            Console.WriteLine("File content is written and closed");
            Console.WriteLine("Press a key to terminate.");
            Console.ReadKey();

            FileStream fs2 = File.OpenRead(filePath);
            int length = (int)fs2.Length;
            byte[] buffer = new byte[length];
            fs2.Read(buffer, 0, length);
            content = Encoding.UTF8.GetString(buffer);
            fs2.Close();
            Console.WriteLine($"\nfile content: \n{content}\n End of file content.");
        }
    }
}
