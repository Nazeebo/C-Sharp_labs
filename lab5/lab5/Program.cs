using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5 {
    class Program {
        static void Main(string[] args) {
            MatrixFilter filter = new MatrixFilter(1);
            //filter.writeKernel();
            
            Bitmap bitmapImage = new Bitmap("image.jpg");
            Bitmap unsafeImage = new Bitmap(bitmapImage);

            DateTime start = DateTime.Now;
            filter.applyFilterToBitmap(ref bitmapImage);
            TimeSpan elapsed = DateTime.Now - start;

            Console.WriteLine("Время работы через getPixel/setPixel = {0}", elapsed);

            start = DateTime.Now;
            filter.applyFilterToBitmapUnsafe(ref unsafeImage);
            elapsed = DateTime.Now - start;

            Console.WriteLine("Время работы через unsafe = {0}", elapsed);

            bitmapImage.Save("Bitmap.jpeg", ImageFormat.Jpeg);
            unsafeImage.Save("Unsafe.jpeg", ImageFormat.Jpeg);
        }
    }
}
