using System;
using System.IO;

namespace Zero_finder
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            bool start = true;
            int n = 0;
            int m = 0;
            int[,] input_array;
            Random rand = new Random();

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (start) {
                Console.WriteLine("Вас вітає програма по знаходженню нулів в двомірному масиві NxM елементів\n" +
                    "Виберіть варіант заповнення даних(1 - заповнення вручну, 2 - заповнення автоматично, 3 прочитати дані з файлу)");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            //Ручне заповнення
                            try
                            {
                                Console.WriteLine("Введіть n");
                                n = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Введіть m");
                                m = Convert.ToInt32(Console.ReadLine());
                                if (n >= 0 && m >= 0)
                                {
                                    input_array = new int[n, m];
                                    for (int i = 0; i < n; i++)
                                    {
                                        for (int j = 0; j < m;)
                                        {
                                            try
                                            {
                                                Console.WriteLine("Введіть значення");
                                                input_array[i, j] = Convert.ToInt32(Console.ReadLine());
                                                j++;
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Введіть коректні дані");
                                            }
                                        }
                                    }
                                    Console.WriteLine("К-сть нулів " + Zero_Counter(input_array, n, m));
                                    start = false;
                                }
                                else
                                {
                                    Console.WriteLine("Розмірність масиву не може бути від*ємною");
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Дані не коректні, спробуйте знову");
                            }
                            break;
                        }
                    case 2:
                        {
                            //Рандомне заповнення
                            n = rand.Next(0, 100);
                            m = rand.Next(0, 100);
                            input_array = new int[n, m];
                            string dir = Directory.GetCurrentDirectory();
                            using (StreamWriter sw = new StreamWriter(dir + "\\data.txt", false, System.Text.Encoding.Default))
                            {
                                sw.WriteLine("Розмірність:");
                                sw.WriteLine(n + "," + m);
                                sw.WriteLine("Дані:");
                            }

                            for (int i = 0; i < n; i++)
                            {
                                for(int j = 0; j < m; j++)
                                {
                                    input_array[i, j] = rand.Next(-1000, 1000);
                                    using (StreamWriter sw = new StreamWriter(dir + "\\data.txt", true, System.Text.Encoding.Default))
                                    {                           
                                        sw.WriteLine(input_array[i, j]);
                                    }
                                }
                            }
                            Console.WriteLine("К-сть нулів " + Zero_Counter(input_array, n, m));
                            start = false;
                            break;
                        }
                    case 3:
                        {
                            //Читання з файлу     

                            string dir = Directory.GetCurrentDirectory();
                            StreamReader sr = new StreamReader(dir + "\\data.txt", System.Text.Encoding.Default);
                            string size;
                            sr.ReadLine();
                            size = sr.ReadLine();
                            sr.ReadLine();
                            n = Convert.ToInt32(size.Split(",")[0]);
                            m = Convert.ToInt32(size.Split(",")[1]);
                            input_array = new int[n, m];
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < m; j++)
                                {
                                    input_array[i, j] = Convert.ToInt32(sr.ReadLine());
                                }
                            }
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < m; j++)
                                {
                                    Console.Write(input_array[i, j]+ " ");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("К-сть нулів " + Zero_Counter(input_array, n, m));
                            start = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Дані не коректні, спробуйте знову");
                            break;
                        }
                }

            }

            
        }

        public static int Zero_Counter(int[,] input_array, int n, int m)
        {
            int zero_counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (input_array[i, j] == 0) zero_counter++;
                }              
            }
            return zero_counter;
        }
    }
}
