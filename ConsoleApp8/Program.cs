using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            RGBTRIPLE[][] image = new RGBTRIPLE[][] {new RGBTRIPLE[] {new RGBTRIPLE(0, 10, 25), new RGBTRIPLE(0, 10, 30), new RGBTRIPLE(40, 60, 80) },
                                                      new RGBTRIPLE[] {new RGBTRIPLE(20, 30, 90), new RGBTRIPLE(30, 40, 100), new RGBTRIPLE(80, 70, 90) },
                                                      new RGBTRIPLE[] {new RGBTRIPLE(20, 20, 40), new RGBTRIPLE(30, 10, 30), new RGBTRIPLE(50, 40, 10) } };

            RGBTRIPLE[][] imag = new RGBTRIPLE [3][];
            int height = 3, width = 3;
            RGBTRIPLE UgleImage = EdgeUngle(1, height, width, image);
            imag[0][0] = UgleImage; //блюрим угол левый верхний
            for (int w = 1; w < 3 - 1; w++) // первая строка пиксилей
            {
                RGBTRIPLE blImage = EdgeFirst(0, w, height, width, image);
                imag[0][w] = blImage;
            }
            UgleImage = EdgeUngle(2, height, width, image); // второй угол правый верхний
            imag[0][width - 1] = UgleImage;

            for (int h = 1; h < height - 1; h++) // основной масив блюра
            {
                RGBTRIPLE blOneImage = EdgeFirstOne(h, 0, height, width, image);
                imag[h][0] = blOneImage;
                for (int w = 1; w < width - 1; w++)
                {
                    RGBTRIPLE blImage = EdgeOne(h, w, height, width, image);
                    imag[h][w] = blImage;
                }
                blOneImage = EdgeLastOne(h, width - 1, height, width, image);
                imag[h][width - 1] = blOneImage;

            }
            UgleImage = EdgeUngle(3, height, width, image); // третий угол левый нижний
            imag[height - 1][0] = UgleImage;
            for (int w = 1; w < width - 1; w++) // последняя строка пикселей
            {
                RGBTRIPLE blImage = EdgeLast(height - 1, w, height, width, image);
                imag[height - 1][w] = blImage;
            }
            UgleImage = EdgeUngle(4, height, width, image); // правый нижний угол
            imag[height - 1][width - 1] = UgleImage;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    image[h][w] = imag[h][w];
                }
            }

        }

        static int [][] Gx = new int[][] { new int[] {-1, 0 , 1}, new int[] {-2, 0, 2}, new int[] {-1, 0, 1}};
        static int [][] Gy = new int[][] {new int[] {-1, -2 , -1}, new int[] {0, 0, 0}, new int[] {1, 2, 1}};

        public static RGBTRIPLE EdgeOne(int currentHeight, int currentWidth, int height, int width, RGBTRIPLE[][] image)
        {
            int h = currentHeight;
            int w = currentWidth;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    RGBTRIPLE asdImage = image[h + i][w + j];
                    bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                    by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                    gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                    ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                }
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }
        //случай первой строки
        public static RGBTRIPLE EdgeFirst(int currentHeight, int currentWidth, int height, int width, RGBTRIPLE [][] image)
        {
            int h = currentHeight;
            int w = currentWidth;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                    by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                    gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                    ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                }
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }
        // случай последней строка
        public static RGBTRIPLE EdgeLast(int currentHeight, int currentWidth, int height, int width, RGBTRIPLE[][] image)
        {
            int h = currentHeight;
            int w = currentWidth;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                    by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                    gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                    ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                }
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }
        // случай первого пикселя в строке
        public static RGBTRIPLE EdgeFirstOne(int currentHeight, int currentWidth, int height, int width, RGBTRIPLE[][] image)
        {
            int h = currentHeight;
            int w = currentWidth;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                    by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                    gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                    ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                }
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }
        // случай последнего пикселя в строке
        public static RGBTRIPLE EdgeLastOne(int currentHeight, int currentWidth, int height, int width, RGBTRIPLE [][] image)
        {
            int h = currentHeight;
            int w = currentWidth;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                    by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                    gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                    rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                    ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                }
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }
        //случай углов
        public static RGBTRIPLE EdgeUngle(int ungl, int height, int width, RGBTRIPLE [][] image)
        {
            int h = 0;
            int w = 0;
            int bx = 0, by = 0, gx = 0, gy = 0, rx = 0, ry = 0;
            switch (ungl)
            {
                case 1: //левый верхний уго
                    h = 0;
                    w = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                            by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                            gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                            ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                        }
                    }
                    break;
                case 2: // правый верхнийй угол
                    h = 0;
                    w = width - 2;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                            by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                            gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                            ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                        }
                    }
                    break;
                case 3: // левый нижний угол
                    h = height - 2;
                    w = 0;
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                            by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                            gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                            ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                        }
                    }
                    break;
                case 4: // правый нижний угол
                    h = height - 2;
                    w = width - 2;
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            bx += image[h + i][w + j].rgbtBlue * Gx[i + 1][j + 1];
                            by += image[h + i][w + j].rgbtBlue * Gy[i + 1][j + 1];
                            gx += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            gy += image[h + i][w + j].rgbtGreen * Gy[i + 1][j + 1];
                            rx += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                            ry += image[h + i][w + j].rgbtRed * Gy[i + 1][j + 1];
                        }
                    }
                    break;
            }
            RGBTRIPLE rImage = new RGBTRIPLE();
            double bb = Math.Sqrt(bx * bx + by * by);
            double gg = Math.Sqrt(gx * gx + gy * gy);
            double rr = Math.Sqrt(rx * rx + ry * ry);
            if (rr > 255)
            {
                rr = 255;
            }
            if (gg > 255)
            {
                gg = 255;
            }
            if (bb > 255)
            {
                bb = 255;
            }
            rImage.rgbtBlue = (byte)Math.Round(bb);
            rImage.rgbtGreen = (byte)Math.Round(gg);
            rImage.rgbtRed = (byte)Math.Round(rr);
            return rImage;
        }


    }
    public class RGBTRIPLE
    {
        public RGBTRIPLE(int blue = 0, int green = 0,int red = 0)
        {
            rgbtBlue = (byte)blue;
            rgbtGreen = (byte)green;
            rgbtRed = (byte)red;
        }
        public byte rgbtBlue;
        public byte rgbtGreen;
        public byte rgbtRed;
    }


}
