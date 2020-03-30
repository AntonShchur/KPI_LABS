using System;
using System.Collections.Generic;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory directory1 = new Directory("Dir1",100);
            Console.WriteLine("Розмір директорії: "+directory1.Size);
            File zip_file = new File("Zip111", 10, ".zip");
            File mp3_file = new File("mp333", 50, ".mp3");
            File text_file = new File("text", 150, ".txt");
            directory1.AddFile(zip_file);
            directory1.AddFile(zip_file);//Сповіщення про наявність файлу
            directory1.AddFile(mp3_file);//Сповіщення про переповнення
            mp3_file.RunFile();
            Console.WriteLine("К-сть файлів в директорії: "+directory1.CountOfFiles()); 


        }
        abstract class Disk 
        {
            public string name { get; set; }
            private int size;
            public int Size
            {
                get
                {
                    return size;
                } 
                set 
                {
                    if(value < 0)
                    {
                        Console.WriteLine("Size < 0");
                        size = 0;
                    }
                    else size = value;
                    
                } 
            }
            public int fullness = 0;
        }
        class Directory : Disk
        {
            List<File> FileList = new List<File>();
            public Directory(string DirName = "", int DirSize=0)
            {
                name = DirName;
                Size = DirSize;
            } 
            public void AddFile(File file)
            {
                foreach(File f in FileList)
                {
                    if((f.name + f.typeOfFile) == file.name + file.typeOfFile)
                    {
                        Console.WriteLine("Даний файл існує");
                    }
                }
                if (fullness + file.Size >= Size)
                {
                    Console.WriteLine("Директорія переповнена, неможливо додати файл");
                }
                else
                {
                    FileList.Add(file);
                    fullness += file.Size;
                    Console.WriteLine("Файл додано");
                }    
            }
            public void RemoveFile(File file)
            {
                FileList.Remove(file);
            }
            public int CountOfFiles()
            {
                return FileList.Count;
            }
            public int CountOfSpecialTypeFile(string type)
            {
                int count = 0;
                foreach(File file in FileList)
                {
                    if (file.typeOfFile == type) count++;
                }
                return count;
            }
        }

        class File : Directory
        {
            
            public string typeOfFile { get; set; }
            public File(string FileName = "", int FileSize = 0, string FileType = "none")
            {
                name = FileName;
                Size = FileSize;
                typeOfFile = FileType;
            }
            public void RunFile()
            {
                Console.WriteLine("Файл запущено!!!");
            }
            
        }
        
    }
}
