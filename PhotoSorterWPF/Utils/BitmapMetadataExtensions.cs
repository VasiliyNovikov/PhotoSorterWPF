using System;
using System.Windows.Media.Imaging;

namespace PhotoSorterWPF
{
    public static class BitmapMetadataExtensions
    {
        private const string RotationQuery = "/app1/ifd/{ushort=274}";

        public static Rotation GetRotation(this BitmapMetadata metadata)
        {
            if (metadata.ContainsQuery(RotationQuery) &&
                metadata.GetQuery(RotationQuery) is ushort rotationMetadata)
            {
                switch (rotationMetadata)
                {
                    case 3:
                        return Rotation.Rotate180;
                    case 6:
                        return Rotation.Rotate90;
                    case 8:
                        return Rotation.Rotate270;
                }
            }

            return Rotation.Rotate0;
        }

        public static DateTime GetDate(this BitmapMetadata metadata) =>  DateTime.Parse(metadata.DateTaken);
    }
}
