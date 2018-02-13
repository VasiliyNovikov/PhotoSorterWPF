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
        public DateTime minDate { get; }
        public DateTime maxDate { get; }
        public List<string> allPhotoNames { get; }
        private List<string> _allPhotoNames = new List<string>();

        List<DateTime> allPhotoDates_in_f = new List<DateTime>();

        public WorkRules (string folder)
        {
            f_path = folder;
            f_name_old = new System.IO.DirectoryInfo(folder).Name;

            var f1 = Directory.GetFiles(folder, "*.jp*", SearchOption.TopDirectoryOnly);
            if (f1.Length != 0)
            {
                f_photo_counter = f1.Length;
                foreach (var j_file in f1)
                {
                    try
                    {
                        FileStream f = File.Open(j_file, FileMode.Open);
                        BitmapDecoder decoder = JpegBitmapDecoder.Create(f, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                        BitmapMetadata metadata = (BitmapMetadata)decoder.Frames[0].Metadata;
                        var dt = DateTime.Parse(metadata.DateTaken);
                        var date = dt.Date;
                        if (!allPhotoDates_in_f.Contains(date))
                            allPhotoDates_in_f.Add(date);
                        f.Close();
                        _allPhotoNames.Add(j_file);
                    }
                    catch { }
                }

                allPhotoNames = _allPhotoNames;
            }
            if (allPhotoDates_in_f.Count != 0)
            {
                minDate = allPhotoDates_in_f.Min();
                maxDate = allPhotoDates_in_f.Max();
                if (maxDate != minDate)
                    f_name_new = (minDate.ToString("yyyy-MM-dd") + " - " + maxDate.ToString("yyyy-MM-dd") + "(" + f_name_old + ")");
                else
                    f_name_new = (minDate.ToString("yyyy-MM-dd") + " - " + f_name_old );
            }

        }
        public void ReportPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, args);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
