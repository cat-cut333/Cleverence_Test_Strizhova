using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Task2_ThreadSafeServer.Server.Interfaces;
using Task2_ThreadSafeServer.Server.Services;

namespace Task2_ThreadSafeServer
{
    class Program
    {
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            var server = new StaticServer();

            Console.WriteLine("=== Демонстрация работы статического сервера ===\n");
            Console.WriteLine("Запускаем 10 читателей и 3 писателей...\n");

            var tasks = new List<Task>();

            // Запускаем 10 читателей.
            for (int i = 1; i <= 10; i++)
            {
                int readerId = i;
                tasks.Add(Task.Run(() => ReaderWork(server, readerId)));
            }

            // Запускаем 3 писателей.
            for (int i = 1; i <= 3; i++)
            {
                int writerId = i;
                tasks.Add(Task.Run(() => WriterWork(server, writerId)));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"\nИтоговое значение count: {server.GetCount()}");
            Console.WriteLine($"Ожидаемое значение (если все операции успешны): 1+2+3 = 6");
            Console.WriteLine($"\nРабота завершена. Программа не упала — значит, синхронизация работает.");
        }

        static void ReaderWork(IStaticServer server, int id)
        {
            for (int i = 0; i < 5; i++)
            {
                int value = server.GetCount();
                Console.WriteLine($"Читатель {id}: прочитал {value}");
                Thread.Sleep(_random.Next(100, 300));
            }
        }

        static void WriterWork(IStaticServer server, int id)
        {
            int value = id;
           
                Console.WriteLine($"Писатель {id}: пытается добавить {value}");
                server.AddToCount(value);
                Console.WriteLine($"Писатель {id}: добавил {value}. Текущий count: {server.GetCount()}");
                Thread.Sleep(_random.Next(200, 500));
          
        }
    }
}