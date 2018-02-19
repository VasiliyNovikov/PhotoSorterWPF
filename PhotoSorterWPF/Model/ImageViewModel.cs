namespace PhotoSorterWPF.Model
{
    public class ImageViewModel
    {
        public ImageInfo Image { get; }

        public ImageViewModel(ImageInfo image) => Image = image;
    }
}


