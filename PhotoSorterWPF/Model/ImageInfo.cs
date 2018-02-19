using System;
using System.Windows.Media.Imaging;

namespace PhotoSorterWPF.Model
{
    public class ImageInfo
    {
        public string Path { get; }
        public DateTime Date { get; }
        public Rotation Rotation { get; }

        public ImageInfo(string path, DateTime date, Rotation rotation)
        {
            Path = path;
            Date = date;
            Rotation = rotation;
        }
    }
}
