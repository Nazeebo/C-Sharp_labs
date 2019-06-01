using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace lab5 {

    class MatrixFilter {
        private int r;
        private double[,] kernel;

        public MatrixFilter(int radius) {
            this.r = radius;
            int K = 2 * r + 1;
            kernel = new double[K, K];
            gaussBlurCore(K);
        }
        private double gaussian (double x, double y, double sigma) {
            return Math.Exp(-(x*x + y*y) / 2*sigma*sigma) / (2 * Math.PI * Math.Pow(sigma, 2.0));
        }
        private void gaussBlurCore(int K) {
            double sigma = 1;
            double sum = 0.0; //for accumulating the kernel values
            for(int x = 0; x < K; ++x)
                for(int y = 0; y < K; ++y) {
                    //kernel[x,y] = Math.Exp(-0.5 * (Math.Pow((x - r) / sigma, 2.0) + Math.Pow((y - r) / sigma, 2.0))) / (2 * Math.PI * Math.Pow(sigma, 2.0));
                    kernel[x, y] = gaussian(x-r, y-r, sigma);
                    sum += kernel[x, y];
                }

            for (int x = 0; x < K; ++x)
                for (int y = 0; y < K; ++y)
                    kernel[x, y] /= sum;
        }


        public void writeKernel() {
            int K = 2 * r + 1;
            for(int i = 0; i < K; ++i) {
                for (int j = 0; j < K; ++j)
                    Console.Write("{0:F6} ", kernel[i, j]);
                Console.WriteLine();
            }
        }

        public void applyFilterToBitmap(ref Bitmap image) {
            for(int x = 0; x < image.Width; ++x)
                for(int y = 0; y < image.Height; ++y) {
                    Color newPixel = applyFilterToPixel(x, y, image);
                    image.SetPixel(x, y, newPixel);
                }
        }

        //x - столбец, у - строка
        private Color applyFilterToPixel(int x, int y, Bitmap image) {
            int N = image.Width, M = image.Height, n, m;
            byte R = 0, G = 0, B = 0;
            for(int i = y - r; i <= y + r; ++i) {
                if (i < 0)
                    m = 0;
                else if (i >= M)
                    m = M - 1;
                else m = i;
                for(int j = x - r; j <= x + r; ++j) {
                    if (j < 0)
                        n = 0;
                    else if (j >= N)
                        n = N - 1;
                    else n = j;

                    Color pixel = image.GetPixel(n, m);
                    R += (byte) (pixel.R * kernel[i - y + r, j - x + r]);
                    G += (byte) (pixel.G * kernel[i - y + r, j - x + r]);
                    B += (byte) (pixel.B * kernel[i - y + r, j - x + r]);
                }
            }

            return Color.FromArgb(R, G, B);
        }

        private unsafe void applyFilterUnsafeForPixel(int x, int y, int bytesPerPixel, int heightInPixels, int widthInBytes, byte* firstPxlAdr, byte* currLine, int stride) {
            int N = widthInBytes, M = heightInPixels, n, m;
            byte R = 0, G = 0, B = 0;
            for (int i = y - r; i <= y + r; ++i) {
                if (i < 0)
                    m = 0;
                else if (i >= M)
                    m = M - 1;
                else m = i;
                for (int j = x - r*bytesPerPixel; j <= x + r*bytesPerPixel; j+=bytesPerPixel) {
                    if (j < 0)
                        n = 0;
                    else if (j >= N)
                        n = N - bytesPerPixel;
                    else n = j;

                    Color pixel = Color.FromArgb(firstPxlAdr[n + m*stride + 2], firstPxlAdr[n + m * stride + 1], firstPxlAdr[n + m * stride]);
                    R += (byte)(pixel.R * kernel[i - y + r, (j - x) / bytesPerPixel + r]);
                    G += (byte)(pixel.G * kernel[i - y + r, (j - x) / bytesPerPixel + r]);
                    B += (byte)(pixel.B * kernel[i - y + r, (j - x) / bytesPerPixel + r]);
                }
            }
            currLine[x + 2] = R;
            currLine[x + 1] = G;
            currLine[x] = B;
        }

        public void applyFilterToBitmapUnsafe(ref Bitmap processedBitmap) {
            unsafe {
                BitmapData bitmapData = processedBitmap.LockBits(
                    new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height),
                    ImageLockMode.ReadWrite, processedBitmap.PixelFormat
                );
                int bytesPerPixel = Image.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0; //адрес данных первого пикселя

                for (int y = 0; y < heightInPixels; ++y) {
                    //stride -- это ширина шага, необходимого для перехода на новую строку изображения
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel) 
                        // calculate new pixel value
                        applyFilterUnsafeForPixel(x, y, bytesPerPixel, heightInPixels, widthInBytes, ptrFirstPixel,
                                                  currentLine, bitmapData.Stride);
                }
                processedBitmap.UnlockBits(bitmapData);
            }
        }
    }
}
