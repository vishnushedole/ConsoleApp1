using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AllocateSeats
    {
        public static void PrintList(SortedList<char, char[]>list)
        {
            foreach (var c in list.Keys)
            {
                Console.Write(c + "      ");
                for (int i = 0; i < list[c].Length; i++)
                {
                    Console.Write(list[c][i] + " ");
                }
                Console.WriteLine();
            }
        }
        static bool CheckAvaiability1(SortedList<char, char[]> list,string choice)
        {
            int start = 0, end = 0;
            int row1 = choice[0], row2 = choice[5];

            for (int j = row1; j <= row2; j++)
            {
                end = (j == row2) ? (choice[6] - '0') * 10 + (choice[7] - '0') : list[(char)j].Length;
                start = (j == row1) ? (choice[1] - '0') * 10 + (choice[2] - '0') : 0;
                for (int i = start; i < end; i++)
                {
                    if (list[(char)j][i] == 'B')
                        return false;
                   
                }
            }
            return true;
        }
        static void UpdateSeats1(SortedList<char, char[]> list, string choice)
        {
            int start = 0, end = 0;
            int row1 = choice[0], row2 = choice[5];

            for (int j = row1; j <= row2; j++)
            {
                end = (j == row2) ? (choice[6] - '0') * 10 + (choice[7] - '0') : list[(char)j].Length;
                start = (j == row1) ? (choice[1] - '0') * 10 + (choice[2] - '0') : 0;
                for (int i = start; i < end; i++)
                {
                    list[(char)j][i] = 'B';
                }
            }
    
        }
        public static bool CheckAvailability2(SortedList<char, char[]>list,string choice)
        {
            char row = 'A';
            int num = 0;
            for (int i = 0; i < choice.Length; i++)
            {
                if (i % 4 == 0)
                {
                    row = choice[i];
                }
                else if (i % 4 == 1)
                {
                    num += (choice[i] - '0') * 10;
                }
                else if (i % 4 == 2)
                {
                    num += (choice[i] - '0');
                }
                else
                {

                    if (list[row][num] == 'B')
                        return false;

                    num = 0;
                }
            }
            if(list[row][num] == 'B')
                return false;

            return true;
        }
        public static void UpdateSeats2(SortedList<char, char[]>list,string choice)
        {
            char row = 'A';
            int num = 0;
            for (int i = 0; i < choice.Length; i++)
            {
                if (i % 4 == 0)
                {
                    row = choice[i];
                }
                else if (i % 4 == 1)
                {
                    num += (choice[i] - '0') * 10;
                }
                else if (i % 4 == 2)
                {
                    num += (choice[i] - '0');
                }
                else
                {
                    list[row][num] = 'B';
                    num = 0;
                }
            }
            list[row][num] = 'B';
        }
        public static void Test()
        {
        SortedList<char, char[]>list = new SortedList<char, char[]>();
        
       
            for (int i=0;i<3;i++)
            {
                char[] AC = { 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A' };
                list.Add((char)('A'+i), AC);
            }
            for(int i=3;i<10;i++)
            {
                char[] DJ = { 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A' };
                list.Add((char)('A' + i), DJ);
            }

            string choice = "";
            while(true)
            {
            Console.WriteLine("Enter the input:");
            choice = Console.ReadLine();

                if (choice == "Q")
                    break;

                if(choice.Length == 8)
                {
                    if (CheckAvaiability1(list, choice))
                    {
                        UpdateSeats1(list, choice);
                        PrintList(list);
                    }
                    else
                    {
                        Console.WriteLine("Seats not Available..");
                        PrintList(list);
                    }
                }
                if(choice.Length != 8 )
                {
                    if (CheckAvailability2(list, choice))
                    {
                        UpdateSeats2(list, choice);
                        PrintList(list);
                    }
                    else
                    {
                        Console.WriteLine("Seats not Available..");
                        PrintList(list);
                    }
                }
            }

        }
        
    }
}
