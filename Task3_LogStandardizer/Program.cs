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
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">
        /// Command line arguments:
        /// <list type="bullet">
        /// <item><description>args[0] - Input file path (optional, defaults to "inputNew.txt" in the project directory)</description></item>
        /// <item><description>args[1] - Output file path (optional, defaults to "output.txt" in the project directory)</description></item>
        /// <item><description>args[2] - Problems file path (optional, defaults to "problems.txt" in the project directory)</description></item>
        /// </list>
        /// </param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// dotnet run -- input.txt output.txt problems.txt
        /// </code>
        /// </example>
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Log Standardizer ===\n");


            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string inputFile = args.Length > 0
                ? args[0]
                : Path.Combine(projectDirectory, "inputNew.txt");

       
            string outputFile = args.Length > 1
                ? args[1]
                : Path.Combine(projectDirectory, "output.txt");

            string problemFile = args.Length > 2
                ? args[2]
                : Path.Combine(projectDirectory, "problems.txt");


            var processor = new LogProcessor();


            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"⚠️ Входной файл '{inputFile}' не найден. Создаю пример...");
                CreateExampleInputFile(inputFile);
            }

            // Создаём пустые выходные файлы, если их нет
            EnsureFileExists(outputFile);
            EnsureFileExists(problemFile);

           

            try
            {
                await processor.ProcessAsync(inputFile, outputFile, problemFile);

                Console.WriteLine($"✅ Обработка завершена!");
                Console.WriteLine($"📄 Входной файл: {inputFile}");
                Console.WriteLine($"📄 Выходной файл: {outputFile}");
                Console.WriteLine($"📄 Проблемные строки: {problemFile}");

                // Выводим содержимое файлов
                DisplayFileContent(inputFile, "Входной файл");
                DisplayFileContent(outputFile, "Выходной файл");
                DisplayFileContent(problemFile, "Файл проблем");

                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при обработке: {ex.Message}");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Creates an example input file with sample log entries if it does not exist.
        /// </summary>
        /// <param name="filePath">The path where the example file should be created.</param>
        /// <remarks>
        /// The example file contains three lines:
        /// <list type="bullet">
        /// <item><description>A valid log entry in Format 1 (space-separated).</description></item>
        /// <item><description>A valid log entry in Format 2 (pipe-separated).</description></item>
        /// <item><description>An invalid line to demonstrate error handling.</description></item>
        /// </list>
        /// </remarks>
        private static void CreateExampleInputFile(string filePath)
        {
            var exampleLines = new[]
            {
                "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
                "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",
                "Невалидная строка для проверки обработки ошибок"
            };

            File.WriteAllLines(filePath, exampleLines);
            Console.WriteLine($"✅ Создан пример входного файла: {filePath}");
        }

        /// <summary>
        /// Ensures that a file exists at the specified path.
        /// If the file does not exist, creates an empty file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <remarks>
        /// This method is used for output files to ensure they are available
        /// even if no data is written to them.
        /// </remarks>
        private static void EnsureFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                Console.WriteLine($"📄 Создан пустой файл: {filePath}");
            }
        }

        /// <summary>
        /// Displays the content of a file to the console.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="description">A description to display before the file content.</param>
        /// <remarks>
        /// If the file is empty, displays "(файл пуст)".
        /// If the file does not exist, displays a message indicating it was not found.
        /// </remarks>
        private static void DisplayFileContent(string filePath, string description)
        {
            Console.WriteLine($"\n--- {description} ---");
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("(файл пуст)");
                }
                else
                {
                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Файл {filePath} не найден.");
            }
        }
    

}
}