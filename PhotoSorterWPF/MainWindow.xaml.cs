using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Threading;

namespace PhotoSorterWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            allRules = int_allRules;
            FolderList.ItemsSource = allRules;
            allImg = new ObservableCollection<imgClass>();

            EventManager.RegisterClassHandler(typeof(ListBoxItem),
                ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(this.OnMouseLeftButtonDown));
            PhotoList.ItemsSource = allImg;
        }

        private ObservableCollection<WorkRules> int_allRules = new ObservableCollection<WorkRules>();
        public ObservableCollection<WorkRules> allRules { get; set; }

        public ObservableCollection<imgClass> allImg { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new Thread(() =>
                   {
                        Thread.CurrentThread.IsBackground = true;
                        FolderParser(dialog.SelectedPath);
                    }).Start();
                }
                else
                {

                }
            }
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var lsI = sender as ListBoxItem;
            if (lsI.DataContext.GetType() == typeof(WorkRules))
            {
                var obj = (WorkRules) lsI.DataContext;
                allImg.Clear();
                foreach (var ph_path in obj.allPhotoNames)
                {
                    imgClass _img = new imgClass();
                    _img.ImagePath = ph_path;
                    allImg.Add(_img);
                }
            }

        }

        void FolderParser(string path)
        {
            WorkRules newWR = new WorkRules(path);
            ObservableCollection<WorkRules> _allRules = new ObservableCollection<WorkRules>();
            _allRules.Add(newWR);
            foreach (var f in Directory.GetDirectories(path))
            {
                WorkRules _newWR = new WorkRules(f);
                _allRules.Add(_newWR);
            }

            App.Current.Dispatcher.Invoke((Action) delegate
            {
                int_allRules.Clear();
                foreach (var wr in _allRules)
                {
                    int_allRules.Add(wr);
                }
            });

        }
    }
}
