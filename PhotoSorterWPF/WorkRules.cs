using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PhotoSorterWPF
{
    public class WorkRules : INotifyPropertyChanged
    {
        /*private static readonly PropertyChangedEventArgs f_pathChangedArgs =
    new PropertyChangedEventArgs("f_path");

        private static readonly PropertyChangedEventArgs f_name_oldChangedArgs =
            new PropertyChangedEventArgs("f_name_old");

        private static readonly PropertyChangedEventArgs f_name_newChangedArgs =
            new PropertyChangedEventArgs("f_name_new");

        private static readonly PropertyChangedEventArgs f_photo_counterChangedArgs =
            new PropertyChangedEventArgs("f_photo_counter");*/

        public string f_path { get; set; }
        public string f_name_old { get; set; }
        public string f_name_new { get; set; }
        public int f_photo_counter { get; set; }
        public List<string> allPhotoNames { get; }

        public WorkRules (string folder)
        {
            allPhotoNames = new List<string>();
            DateTime? minDate = null;
            DateTime? maxDate = null;
            f_path = folder;
            f_name_old = Path.GetFileName(folder);

            var f1 = Directory.GetFiles(folder, "*.jp*", SearchOption.TopDirectoryOnly);
            if (f1.Length != 0)
            {
                f_photo_counter = f1.Length;
                foreach (var j_file in f1)
                {
                    try
                    {
                        using (var f = File.OpenRead(j_file))
                        {
                            var decoder = BitmapDecoder.Create(f, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                            var metadata = (BitmapMetadata)decoder.Frames[0].Metadata;
                            var dt = DateTime.Parse(metadata.DateTaken);
                            var date = dt.Date;
                            if (minDate == null || date < minDate)
                                minDate = date;
                            if (maxDate == null || date > maxDate)
                                maxDate = date;
                            allPhotoNames.Add(j_file);
                        }
                    }
                    catch { }
                }
            }

            if (allPhotoNames.Count != 0)
            {
                if (maxDate != minDate)
                    f_name_new = $"{minDate:yyyy-MM-dd} - {maxDate:yyyy-MM-dd} ({f_name_old})";
                else
                    f_name_new = $"{minDate:yyyy-MM-dd} - {f_name_old}";
            }
        }

        public void ReportPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
