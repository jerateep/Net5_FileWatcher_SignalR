using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitorFile.Models;

namespace MonitorFile.Repository
{
    public interface IFileWatcherRepositiory
    {
        List<FileDetails> CheckFile();
    }
}
