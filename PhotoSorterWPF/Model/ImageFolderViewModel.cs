using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

namespace PhotoSorterWPF.Model
{
    public class ImageFolderViewModel
    {
        public string Path { get; }
        public string OldName { get; }
        public string NewName { get; }
        public List<ImageInfo> Images { get; }

        public ImageFolderViewModel (string folder)
        {
            Images = new List<ImageInfo>();
            DateTime? minDate = null;
            DateTime? maxDate = null;
            Path = folder;
            OldName = System.IO.Path.GetFileName(folder);

            Images = Directory.GetFiles(folder, "*.jp*", SearchOption.AllDirectories)
                              .AsParallel()
                              .Select(f => GetImageInfo(f))
                              .Where(i => i != null)
                              .ToList();

            if (Images.Count == 0)
                return;

            minDate = Images.Min(i => i.Date);
            maxDate = Images.Max(i => i.Date);
            if (maxDate != minDate)
                NewName = $"{minDate:yyyy-MM-dd} - {maxDate:yyyy-MM-dd} ({OldName})";
            else
                NewName = $"{minDate:yyyy-MM-dd} - {OldName}";
        }

        private static ImageInfo GetImageInfo(string path)
        {
            try
            {
                using (var f = File.OpenRead(path))
                {
                    var decoder = BitmapDecoder.Create(f, BitmapCreateOptions.None, BitmapCacheOption.Default);
                    var metadata = (BitmapMetadata)decoder.Frames[0].Metadata;
                    var date = metadata.GetDate();
                    var rotation = metadata.GetRotation();
                    return new ImageInfo(path, date, rotation);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
