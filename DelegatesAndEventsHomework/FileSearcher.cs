namespace DelegatesAndEventsHomework
{
    public class FileSearcher
    {
        public event EventHandler<FileArgs>? FileFound;

        public void Search(string directory)
        {
            var files = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var args = new FileArgs(file);
                OnFileFound(args);
                if (args.Cancel)
                {
                    //Console.WriteLine($"Поиск отменён на файле: {file}");
                    break;
                }

            }
        }

        protected virtual void OnFileFound(FileArgs e)
        {
            FileFound?.Invoke(this, e);
        }
    }
}
