using PhotoSorterWPF.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace PhotoSorterWPF.Model
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<ImageFolderViewModel> Folders { get; }

        private ImageFolderViewModel _selectedFolder;
        public ImageFolderViewModel SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                if (_selectedFolder == value)
                    return;

                _selectedFolder = value;
                ReportPropertyChanged();

                if (value == null)
                    return;

                Images.Clear();
                foreach (var imgInfo in value.Images)
                    Images.Add(new ImageViewModel(imgInfo));
            }
        }

        public ObservableCollection<ImageViewModel> Images { get; }

        public DelegateCommand OnChooseFolder { get; }

        public MainViewModel()
        {
            Folders = new ObservableCollection<ImageFolderViewModel>();
            Images = new ObservableCollection<ImageViewModel>();
            OnChooseFolder = new DelegateCommand(DoChooseFolder);
        }

        private async void DoChooseFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var path = dialog.SelectedPath;
                    var folders = await Task.Factory.StartNew(() => ProcessFolder(path), TaskCreationOptions.LongRunning);
                    Folders.Clear();
                    foreach (var rule in folders)
                        Folders.Add(rule);
                }
            }
        }

        private static List<ImageFolderViewModel> ProcessFolder(string path)
        {
            ImageFolderViewModel newWR = new ImageFolderViewModel(path);
            var allRules = new List<ImageFolderViewModel> { newWR };
            foreach (var f in Directory.GetDirectories(path))
                allRules.Add(new ImageFolderViewModel(f));

            return allRules;
        }
    }
}
