using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB_3
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Example1();

            Dictionary<string, string> inputDictionary = new Dictionary<string, string>();
            Dictionary<string, string> outputDictionary = new Dictionary<string, string>();
            inputDictionary.Add("Антон", " Щ у р2 ");
            inputDictionary.Add("А", "Щу        р");
            ICollection<string> keys = inputDictionary.Keys;
            List<char> charList = new List<char>();
            List<string> stringList = new List<string>();
            string s = "";
            foreach (string key in keys)
            {  
                foreach(char element in inputDictionary[key])
                {
                    if(element != ' ')
                    {
                        charList.Add(element);
                    }
                    s = new string(charList.ToArray());
                }
                outputDictionary.Add(key, s);
                charList.Clear();
            }
            Console.WriteLine(outputDictionary["А"]);
            
            
        }
        static void Example1()
        {
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
           // Console.WriteLine("відсортований список");
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

            string DictionaryString(Dictionary<string, string> dictionary)
            {
                var entries = dictionary.Select(d =>
                string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
                return "{" + string.Join(",", entries) + "}";
            }
        }
       


    }
}
