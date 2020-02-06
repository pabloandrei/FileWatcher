using System.IO;
using static System.Console;
using static System.ConsoleColor;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Testing..");

            Core.Run("/home/andrei/Workspace/FileWatcher/FileWatcher/bin/Debug/netcoreapp3.1/","Test.txt");
        }
    }

    public static class Core
    {
        public static void Run(string path, string fileName)
        {

            if (System.IO.File.Exists(path+fileName))
                WriteLine("File exist.");
            else
                WriteLine("File not exist.");

            var fileSystemWatcher = new FileSystemWatcher();
 
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
 
 
            fileSystemWatcher.Path = Path.GetDirectoryName(path);

            if (fileName!=string.Empty)
                fileSystemWatcher.Filter = Path.GetFileName(fileName);

            fileSystemWatcher.EnableRaisingEvents = true;
 
            WriteLine("Listening...");
            WriteLine("(Press any key to exit.)");
            
            ReadLine();
        }
 
        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            ForegroundColor = Yellow;
            WriteLine($"A new file has been renamed from {e.OldName} to {e.Name}");
        }
 
        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Red;
            WriteLine($"A new file has been deleted - {e.Name}");
        }
 
        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Green;
            WriteLine($"A new file has been changed - {e.Name}");
        }
 
        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Blue;
            WriteLine($"A new file has been created - {e.Name}");
        }
    }
}
