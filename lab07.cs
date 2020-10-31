using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace lab07_ver2
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"C:\Users\mrfra\Desktop\1\input.txt"); //mrfra - имя пользователя

            char[] MassiveUncord = new char[] { '-', '+', '/', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };

            for (int i = 0; i < text.Length; i++)
            {
                if (MassiveUncord.Contains(text[i]) == false)
                {
                    Console.WriteLine("Недопустимое значение!");
                    return;
                }       
            }

            List<string> Peremen = new List<string>();
            Razdelenie(text, ref Peremen);


            string rezult = "";

            if (Peremen[0] == "-")
            {
                Peremen.RemoveAt(0);
                Peremen[0] = Convert.ToString(Convert.ToInt32(Peremen[0]) * (-1));
            }

            Calculator(Peremen, ref rezult);
            File.WriteAllText(@"C:\Users\mrfra\Desktop\1\output.txt", rezult); //mrfra - имя пользователя
        }
        static void Razdelenie(string text, ref List<string> Peremen)
        {
            string[] MassiveOne = text.Split(new char[] { ' ' });
            
            for (var i = 0; i < MassiveOne.Length; i++)
            {
                if (MassiveOne[i] != "")
                    Peremen.Add(MassiveOne[i].Trim());
            }

        }
        static void Calculator(List <string> Peremen, ref string rezult)
        {
            
            int KolPeremen = 0;

            if (Peremen.Contains("*") == true || Peremen.Contains("/") == true)
            {
                int KolDiv = 0; int KolMul = 0; int index = 0; string zamena = "";

                foreach (string trash in Peremen)
                {
                    if (trash == "*")
                        KolMul++;
                    else if (trash == "/")
                        KolDiv++;
                }

                MulAndDiv(KolDiv, KolMul, index, zamena, ref Peremen);

                rezult = Peremen[0];

                foreach (var i in Peremen)
                    KolPeremen++;

                for (int i = 1; i < KolPeremen; i++)
                    Osnova(Peremen, i, ref rezult);
                
                Console.WriteLine(rezult);
                
            }
            else
            {
                foreach (var i in Peremen)
                    KolPeremen++;

                rezult = Peremen[0];

                for (int i = 1; i < KolPeremen; i++)
                    Osnova(Peremen, i, ref rezult);
                
                Console.WriteLine(rezult);
            }


        }
        static void Osnova(List<string> Peremen, int i, ref string rezult)
        {
            if (Peremen[i] == "-")
                Minus(ref rezult, Peremen, i);
            
            else if (Peremen[i] == "+")
                Plus(ref rezult, Peremen, i);
        }
        static void Minus(ref string rezult, List<string> Peremen, int i)
        {
            rezult = Convert.ToString(Convert.ToInt32(rezult) - Convert.ToInt32(Peremen[i + 1]));
        }
        static void Plus(ref string rezult, List<string> Peremen, int i)
        {
            rezult = Convert.ToString(Convert.ToInt32(rezult) + Convert.ToInt32(Peremen[i + 1]));
        }
        static void MulAndDiv(int KolDiv, int KolMul, int index, string zamena, ref List<string> Peremen) 
        {
            if (KolDiv > 0)
            {
                for (int i = 0; i < KolDiv; i++)
                {
                    index = Peremen.IndexOf("/");

                    zamena = Convert.ToString(Convert.ToInt32(Peremen[index - 1]) / Convert.ToInt32(Peremen[index + 1]));

                    Peremen.Insert(index, zamena);
                    Peremen.RemoveAt(index + 1);
                    Peremen.RemoveAt(index - 1);

                }
            }
            if (KolMul > 0)
            {
                for (int i = 0; i < KolMul; i++)
                {
                    index = Peremen.IndexOf("*");

                    zamena = Convert.ToString(Convert.ToInt32(Peremen[index - 1]) * Convert.ToInt32(Peremen[index + 1]));

                    Peremen.Insert(index, zamena);
                    Peremen.RemoveAt(index + 1);
                    Peremen.RemoveAt(index - 1);

                }
            }
        }
    }
}
