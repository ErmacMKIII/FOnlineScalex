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
using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.PostProcessing
{
    public struct AlphaRange
    {
        public int DropThreshold { get; set; }
        public int MultiplyThreshold {  get; set; } 
    }

    public static class PostProcessor
    {
        /// <summary>
        /// Post process image to mitigate algorithm
        /// </summary>
        /// <param name="src">original source image</param>
        /// <param name="dst">result bitmap by post-processing</param>
        public static void Process(Bitmap src, out Bitmap dst, AlphaRange alphaRange)
        {
            int w = src.Width;
            int h = src.Height;

            dst = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py;

            // remove artifacts
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Color srcPixel = src.GetPixel(px, py);
                    dst.SetPixel(px, py, srcPixel);

                    Color srcGauss = ColorSampler.GaussianBlurSample(src, px, py);

                    if (srcPixel.A != 0 && srcGauss.A < alphaRange.DropThreshold) // artifact detected
                    {
                        dst.SetPixel(px, py, Color.Transparent);
                    }

                    if (srcPixel.A == 0 && srcGauss.A != 0 && srcGauss.A >= alphaRange.DropThreshold) // edge detected
                    {
                        float alphaf = srcGauss.A / 255.0f;
                        int red = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.R), 255);
                        int green = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.G), 255);
                        int blue = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.B), 255);
                        dst.SetPixel(px, py, Color.FromArgb(alphaRange.DropThreshold, red, green, blue));
                    }

                    if (srcPixel.A != 0 && srcGauss.A != 0 && srcGauss.A <= alphaRange.MultiplyThreshold) // opaque pixel but adjacent weak alpha
                    {
                        float alphaf = srcPixel.A / 255.0f;
                        int red = Math.Min((int)Math.Round(alphaf * (srcGauss.R + srcPixel.R)), 255);
                        int green = Math.Min((int)Math.Round(alphaf * (srcGauss.G + srcPixel.G)), 255);
                        int blue = Math.Min((int)Math.Round(alphaf * (srcGauss.B + srcPixel.B)), 255);
                        dst.SetPixel(px, py, Color.FromArgb(255, red, green, blue));
                    }
                }
            }

            // Remove blue color (always)
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Color srcPixel = src.GetPixel(px, py);
                    if (srcPixel.Equals(Color.Blue))
                    {
                        dst.SetPixel(px, py, Color.Transparent);
                    }
                }
            }
        }
    }
}
