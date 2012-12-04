using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubDownloader
{
    class VideoFile
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public FileInfo File { get; private set; }
        public string Hash { get; private set; }
        public string Extension { get; private set; }
        public bool HasSubtitle { get; private set; }

        public VideoFile(string filePath)
        {
            File = new FileInfo(filePath);
            Name = File.Name;
            Path = File.DirectoryName;
            Extension = File.Extension;
            Hash = VideoFileHash.Compute(filePath).ToHexadecimal();
            HasSubtitle = CheckIfSubtitlePresent();
        }

        private string[] GetValidSubtitleExtensions()
        {
            return new String[] { "*.srt", "*.sub", "*.smi", "*.txt", "*.ssa", "*.ass", "*.mpl" };
        }

        private bool CheckIfSubtitlePresent()
        {
            var subtitleFiles = GetValidSubtitleExtensions().AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(Path, searchPattern, SearchOption.TopDirectoryOnly));
            foreach (var item in subtitleFiles)
            {
                if(String.Compare(System.IO.Path.GetFileNameWithoutExtension(Name),  System.IO.Path.GetFileNameWithoutExtension(item), true) == 0) return true;
            }
            return false;
        }
    }
}
