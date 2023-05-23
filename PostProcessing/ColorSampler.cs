/* Copyright (C) 2023 Aleksandar Stojanovic <coas91@rocketmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/> */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.PostProcessing
{
    public static class ColorSampler
    {
        /// <summary>
        /// up-left-right-down factor
        /// </summary>
        public const double A = 0.123317; // up-left-right-down
        /// <summary>
        /// diagonal factor
        /// </summary>
        public const double B = 0.077847; // diagonal
        /// <summary>
        /// center factor
        /// </summary>
        public const double C = 0.195346; // center  

        /// <summary>
        /// Get Sample from all of adjacent pixels on given the offset
        /// </summary>
        /// <param name="src">Source Image</param>
        /// <param name="px">Pixel x coord</param>
        /// <param name="py">Pixel y coord</param>
        /// <param name="offset">corresponds to outline width</param>
        /// <returns>sampel color (RGB)</returns>
        public static Color AverageBlurSample(Bitmap src, int px, int py, int offset)
        {
            // INPUT LOGIC          
            int len = 2 * offset + 1;
            int[] offX = new int[len];
            int[] offY = new int[len];
            int e = 0; // used for indexing
            for (int i = -offset; i <= offset; i++)
            {
                if (i < 0)
                {
                    offX[e] = Math.Max(px + i, 0);
                    offY[e] = Math.Max(py + i, 0);
                }
                else
                {
                    offX[e] = Math.Min(px + i, src.Width - 1);
                    offY[e] = Math.Min(py + i, src.Height - 1);
                }
                e++;
            }
            //----------------------------------------------------------------------
            // RED, GREEN, BLUE AND ALPHA SAMPLE        
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;
            int sumA = 0;
            for (int i = 0; i < offX.Length; i++)
            {
                for (int j = 0; j < offY.Length; j++)
                {
                    Color col = src.GetPixel(offX[i], offY[j]);
                    sumR += col.R;
                    sumG += col.G;
                    sumB += col.B;
                    sumA += col.A;
                }
            }
            int avgR = sumR / (len * len);
            int avgG = sumG / (len * len);
            int avgB = sumB / (len * len);
            int avgA = sumA / (len * len);
            //----------------------------------------------------------------------        
            // OUTPUT LOGIC
            return Color.FromArgb(avgA, avgR, avgG, avgB);
        }

        /// <summary>
        /// Get Sample from all of adjacent pixels with Gauss kernel coefficients for
        /// single pass
        /// </summary>
        /// <param name="src">Source Image</param>
        /// <param name="px">Pixel x coord</param>
        /// <param name="py">Pixel y coord</param>
        /// <returns>sample color (RGBA)</returns>
        public static Color GaussianBlurSample(Bitmap src, int px, int py)
        {
            // INPUT LOGIC
            int[] offX = { Math.Max(px - 1, 0), px, Math.Min(px + 1, src.Width - 1) };
            int[] offY = { Math.Max(py - 1, 0), py, Math.Min(py + 1, src.Height - 1) };
            double red = 0;
            double green = 0;
            double blue = 0;
            double alpha = 0;
            // [0] - RED
            red += B * (src.GetPixel(offX[0], offY[0]).R
                    + src.GetPixel(offX[2], offY[0]).R
                    + src.GetPixel(offX[0], offY[2]).R
                    + src.GetPixel(offX[2], offY[2]).R);

            red += C * src.GetPixel(offX[1], offY[1]).R;

            red += A * (src.GetPixel(offX[1], offY[0]).R + src.GetPixel(offX[0], offY[1]).R
                    + src.GetPixel(offX[1], offY[2]).R + src.GetPixel(offX[2], offY[1]).R);
            // [1] - GREEN
            green += B * (src.GetPixel(offX[0], offY[0]).G
                    + src.GetPixel(offX[2], offY[0]).G
                    + src.GetPixel(offX[0], offY[2]).G
                    + src.GetPixel(offX[2], offY[2]).G);

            green += C * src.GetPixel(offX[1], offY[1]).G;

            green += A * (src.GetPixel(offX[1], offY[0]).G + src.GetPixel(offX[0], offY[1]).G
                    + src.GetPixel(offX[1], offY[2]).G + src.GetPixel(offX[2], offY[1]).G);
            // [2] - BLUE
            blue += B * (src.GetPixel(offX[0], offY[0]).B
                    + src.GetPixel(offX[2], offY[0]).B
                    + src.GetPixel(offX[0], offY[2]).B
                    + src.GetPixel(offX[2], offY[2]).B);

            blue += C * src.GetPixel(offX[1], offY[1]).B;

            blue += A * (src.GetPixel(offX[1], offY[0]).B + src.GetPixel(offX[0], offY[1]).B
                    + src.GetPixel(offX[1], offY[2]).B + src.GetPixel(offX[2], offY[1]).B);
            // [3] - ALPHA
            alpha += B * (src.GetPixel(offX[0], offY[0]).A
                    + src.GetPixel(offX[2], offY[0]).A
                    + src.GetPixel(offX[0], offY[2]).A
                    + src.GetPixel(offX[2], offY[2]).A);

            alpha += C * src.GetPixel(offX[1], offY[1]).A;

            alpha += A * (src.GetPixel(offX[1], offY[0]).A + src.GetPixel(offX[0], offY[1]).A
                    + src.GetPixel(offX[1], offY[2]).A + src.GetPixel(offX[2], offY[1]).A);
            // FINALLY, OUTPUT LOGIC
            return Color.FromArgb((int)alpha, (int)red, (int)green, (int)blue);
        }

    }
}
