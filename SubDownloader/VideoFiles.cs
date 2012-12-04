using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubDownloader
{
    class VideoFiles
    {
        internal static List<VideoFile> GetFiles(string path)
        {
            var videoFileList = new List<VideoFile>();
            var searchPatterns = GetValidExtensions();
            var videoFiles = searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories));
            foreach (var videoFile in videoFiles)
            {
                videoFileList.Add(new VideoFile(videoFile));
            }
            return videoFileList;
        }

        internal static string[] GetValidExtensions()
        {
            return new String[] { "*.3g2", "*.3gp", "*.3gp2", "*.3gpp", "*.60d", "*.ajp", "*.asf", "*.asx", "*.avchd", "*.avi", "*.bik", "*.bix", "*.box", "*.cam", "*.dat", "*.divx", "*.dmf", "*.dv", "*.dvr-ms", "*.evo", "*.flc", "*.fli", "*.flic", "*.flv", "*.flx", "*.gvi", "*.gvp", "*.h264", "*.m1v", "*.m2p", "*.m2ts", "*.m2v", "*.m4e", "*.m4v", "*.mjp", "*.mjpeg", "*.mjpg", "*.mkv", "*.moov", "*.mov", "*.movhd", "*.movie", "*.movx", "*.mp4", "*.mpe", "*.mpeg", "*.mpg", "*.mpv", "*.mpv2", "*.mxf", "*.nsv", "*.nut", "*.ogg", "*.ogm", "*.omf", "*.ps", "*.qt", "*.ram", "*.rm", "*.rmvb", "*.swf", "*.ts", "*.vfw", "*.vid", "*.video", "*.viv", "*.vivo", "*.vob", "*.vro", "*.wm", "*.wmv", "*.wmx", "*.wrap", "*.wvx", "*.wx", "*.x264", "*.xvid" };
        }
    }
}
