using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;


namespace LAB_3
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Example1();

            Example2();

            Example3();

        }
        static void Example1()
        {
            Console.WriteLine("Завдання 1");
            Random rand = new Random();
            int countOfPairs = 0;
            List<int> listOfInt = new List<int>();
            Console.WriteLine("Не відсортований список");
            for (int i = 0; i < 20; i++)
            {
                listOfInt.Add(rand.Next(0, 20));
                Console.Write(listOfInt[i] + "; ");
            }
            listOfInt.Sort();

            Console.WriteLine("\nВідсортований список");
            for (int i = 0; i < 20; i++)
            {
                Console.Write(listOfInt[i]+"; ");
            }
            for (int i = 0; i < listOfInt.Count - 1;)
            {
                if (listOfInt[i] == listOfInt[i + 1])
                {
                    countOfPairs++;
                    i += 2;
                }
                else { i++; }
            }
            Console.WriteLine("К-сть знайдених пар: " + countOfPairs);  
        }

        static void Example2()
        {
            Console.WriteLine("Завдання 2");
            Dictionary<string, string> inputDictionary = new Dictionary<string, string>();
            Dictionary<string, string> outputDictionary = new Dictionary<string, string>();
            inputDictionary.Add("Антон", " Щ у р ");
            inputDictionary.Add("А", "Щу        р");
            ICollection<string> keys = inputDictionary.Keys;
            List<char> charList = new List<char>();
            List<string> stringList = new List<string>();
            string s = "";
            foreach (string key in keys)
            {
                foreach (char element in inputDictionary[key])
                {
                    if (element != ' ')
                    {
                        charList.Add(element);
                    }
                    s = new string(charList.ToArray());
                }
                outputDictionary.Add(key, s);
                charList.Clear();
            }
            Console.WriteLine(outputDictionary["А"]);
            string json;
            json = JsonConvert.SerializeObject(outputDictionary);
            
            using (StreamWriter streamWriter = new StreamWriter("E:\\Git\\KPI_LABS\\ProgramLABS\\LAB-3\\bin\\Debug\\netcoreapp3.1\\json.json", false))
            {
                streamWriter.Write(json);
            }
        }

        static void Example3()
        {
            Console.WriteLine("Linq");
            int[] inputIntegerNumbers = { 1, -5, -10, 22, 45, 88, 122, 7, 34, 100, 90, 56, 45 };
            IEnumerable<int> integers = from integer in inputIntegerNumbers where integer > 10 && integer < 100 orderby integer ascending select integer;
            foreach(int i in integers)
            {
                Console.Write(i + "; ");
            }
                                                    
        }

    }
}
