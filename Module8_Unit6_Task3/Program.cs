using System;
using System.IO;

namespace Module8_Unit6_Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Ввели путь к папке
            string Path;
            double FirstSizePafh = 0;
            double PresentSizePafh = 0;
            double NumbDelFile = 0;
            long FirstNumFile = 0;
            long PresentNumFile = 0;
            

            Console.WriteLine("Введите путь к папке:");
            Path = Console.ReadLine();
            //----------------------------
            FirstSizePafh = SizeOfFolder(Path);
            FirstNumFile = NumFile(Path);
            Console.WriteLine($"Исходный размер папки:{FirstSizePafh}");
            ClearDirectory(Path);
            PresentSizePafh = SizeOfFolder(Path);
            PresentNumFile = NumFile(Path);
            double LastSizePafh =FirstSizePafh - PresentSizePafh;
            long LastNumFile = FirstNumFile - PresentNumFile;
            Console.WriteLine($"Освобождено: {LastSizePafh} байт, удалено: {LastNumFile} файлов");
            Console.WriteLine($"Текущий размер папки:{PresentSizePafh}");
        }

        static void ClearDirectory(string FolderName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@FolderName);
            DirectoryInfo[] diA = dirInfo.GetDirectories();
            FileInfo[] fi = dirInfo.GetFiles();
            try
            {
                if (dirInfo.Exists)
                {
                    
                    foreach (FileInfo f in dirInfo.GetFiles())
                    {
                        
                        if ((DateTime.Now - f.LastAccessTime) > TimeSpan.FromMinutes(1))
                        { f.Delete();  }
                    }
                    foreach (DirectoryInfo d in diA)
                    {
                       
                        ClearDirectory(d.FullName);
                        
                        if ((DateTime.Now - d.LastAccessTime) > TimeSpan.FromMinutes(1))
                        { d.Delete(); }
                    }

                }
              
                
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                
            }
        }
        static double SizeOfFolder(string folder)
        {
            try
            {
                double catalogSize = 0;
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {
                    //Записываем размер файла в байтах
                    catalogSize = catalogSize + f.Length;
                }
                foreach (DirectoryInfo df in diA)
                {
                    //рекурсивно вызываем наш метод
                    SizeOfFolder(df.FullName);
                }
                return catalogSize;
                
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                return 0;
            }
        }

        static long NumFile(string FolderName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@FolderName);
            FileInfo[] f = dirInfo.GetFiles();
            DirectoryInfo[] diA = dirInfo.GetDirectories();
            long num = 0;
            long num1 = 0;
            long sum = 0;
            try
            {
                if (dirInfo.Exists)
                {
                    num = f.Length;
                    foreach (DirectoryInfo d in diA)
                    {
                        FileInfo[] f1 = d.GetFiles();
                        num1  = f1.Length+ num1;
                    }
                    sum = num1 + num;
                }
                return sum;

            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                return 0;
            }
        }

    }
}
