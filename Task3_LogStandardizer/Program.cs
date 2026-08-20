using System;
using System.Threading.Tasks;
using Task3_LogStandardizer.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Task3_LogStandardizer
{/// <summary>
 /// Entry point for the Log Standardizer console application.
 /// </summary>
 /// <remarks>
 /// The application processes log files by parsing entries from two different formats
 /// and writing them to a unified tab-separated format.
 /// </remarks>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Log Standardizer ===\n");

            string inputFile = @"C:\\Users\\katia\\source\\repos\\Cleverence_Test_Strizhova\\CleverenceTest\\Task3_LogStandardizer\\input.txt";
            string outputFile = @"C:\\Users\\katia\\source\\repos\\Cleverence_Test_Strizhova\\CleverenceTest\\Task3_LogStandardizer\\output.txt";
            string problems = @"C:\\Users\\katia\\source\\repos\\Cleverence_Test_Strizhova\\CleverenceTest\\Task3_LogStandardizer\\problems1.txt";


            var processor = new LogProcessor();
            await processor.ProcessAsync(inputFile, outputFile, problems);

            Console.WriteLine($"✅ Обработка завершена!");
            Console.WriteLine($"📄 Входной файл: {inputFile}");
            var lines = File.ReadAllLines(inputFile);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
                Console.WriteLine($"📄 Выходной файл: {outputFile}");
            lines = File.ReadAllLines(outputFile);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine($"📄 Проблемные строки: {problems}");
            lines = File.ReadAllLines(problems);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

    }
}