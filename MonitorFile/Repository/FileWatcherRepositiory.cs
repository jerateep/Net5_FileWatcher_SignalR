using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using MonitorFile.Hubs;
using MonitorFile.Models;

namespace MonitorFile.Repository
{
    public class FileWatcherRepositiory : IFileWatcherRepositiory
    {
        private readonly IHubContext<FileWatcherHub> _hubContext;

        public FileWatcherRepositiory(IHubContext<FileWatcherHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public List<FileDetails> CheckFile()
        {
            List<FileDetails> LstFile = new List<FileDetails>();
            string FolderPath = @"D:\AV\";
            var watcher = new FileSystemWatcher();
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnChanged);
            //watcher.Filter = "*.xlsx";
            watcher.Path = FolderPath;
            watcher.EnableRaisingEvents = true;
            DirectoryInfo di = new DirectoryInfo(FolderPath);
            LstFile = di.GetFiles("*.*")
                                .OrderByDescending(file => file.LastWriteTime)
                                .Select(file => new FileDetails
                                {
                                    Name = file.Name,
                                    FileType = file.Extension
                                }).ToList();
            return LstFile;

        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _hubContext.Clients.All.SendAsync("Refreshfilewatcher");
        }
    }
}
