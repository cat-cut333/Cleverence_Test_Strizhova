using System;
using System.Threading.Tasks;
using Task3_LogStandardizer.Services;

namespace Task3_LogStandardizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Log Standardizer ===\n");

            string inputFile = @"C:\\Users\\katia\\source\\repos\\Cleverence_Test_Strizhova\\CleverenceTest\\Task3_LogStandardizer\\input.txt";
            string outputFile = @"C:\\Users\\katia\\source\\repos\\Cleverence_Test_Strizhova\\CleverenceTest\\Task3_LogStandardizer\\output.txt";

            string[] inputLines = new[]
        {
            "10.03.2025 15:14:49.523 INFORMATION Валидная строка",
            "Невалидная строка",
            "10.03.2025 15:14:49.523 ERROR Ещё одна валидная",
            "Невалидная строка22",
        };
            File.WriteAllLines(inputFile, inputLines);

            var processor = new LogProcessor();
            await processor.ProcessAsync(inputFile, outputFile);

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
            Console.WriteLine($"📄 Проблемные строки: problems.txt");
            lines = File.ReadAllLines("problems.txt");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

    }
}