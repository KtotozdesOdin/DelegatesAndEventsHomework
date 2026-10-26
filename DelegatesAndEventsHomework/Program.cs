namespace DelegatesAndEventsHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --- Часть 1: GetMax на продуктах ---
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 85000, Weight = 2.1f },
                new Product { Name = "Monitor", Price = 35000, Weight = 5.5f },
                new Product { Name = "Keyboard", Price = 12000, Weight = 0.8f }
            };

            var mostExpensive = products.GetMax(p => p.Price);
            var heaviest = products.GetMax(p => p.Weight);

            Console.WriteLine($"Самый дорогой: {mostExpensive.Name}, цена {mostExpensive.Price}");
            Console.WriteLine($"Самый тяжёлый: {heaviest.Name}, вес {heaviest.Weight}");

            // --- Часть 2: поиск файлов ---
            var searcher = new FileSearcher();
            var foundFiles = new List<FileInfo>();

            searcher.FileFound += (sender, e) =>
            {
                foundFiles.Add(new FileInfo(e.FileName));
                Console.WriteLine($"Путь к файлу: {e.FileName}");
                if (Path.GetExtension(e.FileName) == ".exe")
                {
                    e.Cancel = true;
                }
            };

            searcher.FileFound += OnFileFoundPrint;          // второй подписчик

            Console.WriteLine("--- Поиск 1: два подписчика ---");
            searcher.Search(Environment.CurrentDirectory);
            PrintBiggest(foundFiles);

            searcher.FileFound -= OnFileFoundPrint;          // отписка
            foundFiles.Clear();                              // чтобы файлы не задвоились

            Console.WriteLine("--- Поиск 2: после отписки ---");
            searcher.Search(Environment.CurrentDirectory);
            PrintBiggest(foundFiles);
        }

        static void OnFileFoundPrint(object? sender, FileArgs e)
        {
            Console.WriteLine($"  [второй подписчик] {Path.GetFileName(e.FileName)}");
        }

        static void PrintBiggest(List<FileInfo> files)
        {
            if (files.Count == 0)
            {
                Console.WriteLine("Файлы не найдены.");
                return;
            }

            var biggest = files.GetMax(f => (float)f.Length);
            Console.WriteLine($"Самый большой файл: {biggest.Name} ({biggest.Length} байт)");
        }

    }
}
