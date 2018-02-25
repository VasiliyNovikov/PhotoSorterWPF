using PhotoSorterWPF.Commands;

namespace PhotoSorterWPF.Model
{
    public class ImageViewModel : ObservableObject
    {
        public ImageInfo Image { get; }

        public ImageViewModel(ImageInfo image)
        {
            Image = image;
            OnMouseOver = new DelegateCommand<bool>(DoOnMouseOver);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                ReportPropertyChanged();
            }
        }

        private bool _isSelectorVisible;
        public bool IsSelectorVisible
        {
            get => _isSelectorVisible;
            private set
            {
                if (_isSelectorVisible == value)
                    return;

                _isSelectorVisible = value;
                ReportPropertyChanged();
            }
        }

        public ICommand<bool> OnMouseOver { get; }

        private void DoOnMouseOver(bool isMouseOver) => IsSelectorVisible = isMouseOver || IsSelected;
    }
}


