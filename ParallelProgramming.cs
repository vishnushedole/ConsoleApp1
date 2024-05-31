using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ParallelProgramming
    {
        static string ConvertBytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.AppendFormat("{0:X}", bytes[i]);
            }
            return sb.ToString();
        }
        internal static void Test()
        {
            Console.WriteLine("Before calling Sequential");
            SequentialLoop();
            Console.WriteLine("After calling Sequential");
        }
        static int MaxSize = 8_00_000;

        static void ParallelLoop()
        {
            Console.WriteLine("Parallel Loop begins execution");
            System.Diagnostics.Stopwatch watch = Stopwatch.StartNew();
            //for (int i = 0; i < MaxSize; i++)
            Parallel.For(1, MaxSize + 1, i =>
            {
                var aes = Aes.Create();
                aes.GenerateIV();
                aes.GenerateKey();
                var buffer = aes.Key;
                var str = ConvertBytesToHexString(buffer);
            });
            watch.Stop();
            Console.WriteLine($"{nameof(ParallelLoop)} has taken {watch.ElapsedMilliseconds} ms.");
        }
        static void SequentialLoop()
        {
            Console.WriteLine("Sequentail loop begins execution");
            System.Diagnostics.Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < MaxSize; i++)
            {
                var aes = Aes.Create();
                aes.GenerateIV();
                var buffer = aes.Key;
                var str = ConvertBytesToHexString(buffer);
            }
            watch.Stop();
            Console.WriteLine($"{nameof(SequentialLoop)} has taken {watch.ElapsedMilliseconds} seconds.");
        }
    }
}
