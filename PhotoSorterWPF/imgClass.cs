using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorterWPF
{
    public class imgClass : INotifyPropertyChanged
    {
        public string ImagePath { get; set; }

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


