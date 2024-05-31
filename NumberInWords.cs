using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class NumberInWords
    {
        public static string getDigits(string number,int index,int p)
        {
            string num = "";
            for(int i=index;i<=number.Length-p;i++)
                num += number[i];
            return num;
        }
        public static void PrintNum(string Digits,string tag,SortedList<string,string> list)
        {
            if (Digits.Length == 1)
            {
                Console.Write($"{list[Digits]} {tag} ");
            }
            else
            {
                if (Digits[0] == '0' && Digits[1] != '0')
                    Console.Write($"{list[Digits[1] + ""]} {tag} ");
                else if (Digits[0] == '1')
                    Console.Write($"{list[Digits]} {tag} ");
                else if (Digits[0] != '0')
                {
                    Console.Write($"{list[Digits[0] + "0"]}");
                    if (Digits[1] != '0')
                        Console.Write($"{list[Digits[1] + ""]} {tag} ");
                }
            }
        }
        public static void Test()
        {
            Console.WriteLine("Enter the number:");
            string num = Console.ReadLine();
            SortedList<string,string> list = new SortedList<string,string>();
            list.Add("1", "one");
            list.Add("2", "two");
            list.Add("3", "three");
            list.Add("4", "four");
            list.Add("5", "five");
            list.Add("6", "six");
            list.Add("7", "seven");
            list.Add("8", "eight");
            list.Add("9", "nine");
            list.Add("10", "ten");
            list.Add("11", "eleven");
            list.Add("12", "twelve");
            list.Add("13", "thirteen");
            list.Add("14", "fourteen");
            list.Add("15", "fifteen");
            list.Add("16", "sixteen");
            list.Add("17", "seventeen");
            list.Add("18", "eighteen");
            list.Add("19", "nineteen");
            list.Add("20", "twenty");
            list.Add("30", "thirty");
            list.Add("40", "fourty");
            list.Add("50", "fifty");
            list.Add("60", "sixty");
            list.Add("70", "seventy");
            list.Add("80", "eighty");
            list.Add("90", "ninenty");


            for (int i = 0; i < num.Length; i++)
            {
                int pos = num.Length - i;
                string Digits = "";
                if (pos == 8)
                {
                    Digits = getDigits(num, i, pos);
                    Console.Write($"{list[Digits]} crore ");
                }
                else if (pos == 7 || pos == 6)
                {
                    Digits = getDigits(num, i, 6);
                    PrintNum(Digits, "lakh",list);
                    if (pos == 7) i++;
                }
                else if (pos == 5 || pos == 4)
                {
                    Digits = getDigits(num, i, 4);
                    PrintNum(Digits, "thousand", list);
                    if (pos == 5) i++;
                }
                else if(pos == 3)
                {
                    Digits = getDigits(num, i, pos);

                    if (Digits[0] != '0')
                        Console.Write($"{list[Digits[0]+""]} hundred ");
                }
                else
                {
                    Digits = getDigits(num, i, 1);
                    PrintNum(Digits, "", list);
                    if (pos == 2) i++;
                }
            }
        }
    }
}
