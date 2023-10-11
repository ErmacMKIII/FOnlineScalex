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
using FOnlineScalex.Algorithm;
using FOnlineScalex.Examination;
using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.ScalexFamily
{
    public abstract class Scalex : IAlgorithm
    {
        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------

        /// <summary>
        /// Copy a pixel from src to dst
        /// </summary>
        /// <param name="dst">Destination Frame</param>
        /// <param name="dx">Destination Pixel px</param>
        /// <param name="dy">Destination Pixel py</param>
        /// <param name="src">Source Frame</param>
        /// <param name="sx">Source Pixel px</param>
        /// <param name="sy">Source Pixel py</param>
        protected static void PixelCopy(ref Frame dst, uint dx, uint dy, Frame src, uint sx, uint sy)
        {            
            dst.SetPixel(dx, dy, src.GetPixel(sx, sy));
        }

        /// <summary>
        /// Copy a pixel from src to dst
        /// </summary>
        /// <param name="dst">Destination Frame</param>
        /// <param name="dx">Destination Pixel px</param>
        /// <param name="dy">Destination Pixel py</param>
        /// <param name="src">Source Frame</param>
        /// <param name="sx">Source Pixel px</param>
        /// <param name="sy">Source Pixel py</param>
        protected static void PixelCopy(ref Bitmap dst, int dx, int dy, Bitmap src, int sx, int sy)
        {
            dst.SetPixel((int)dx, (int)dy, src.GetPixel((int)sx, (int)sy));
        }

        /// <summary>
        /// Copy a pixel from src to dst (safely)
        /// </summary>
        /// <param name="dst">Destination Frame</param>
        /// <param name="dx">Destination Pixel px</param>
        /// <param name="dy">Destination Pixel py</param>
        /// <param name="src">Source Frame</param>
        /// <param name="sx">Source Pixel px</param>
        /// <param name="sy">Source Pixel py</param>
        protected static void PixelCopySafe(ref Frame dst, uint dx, uint dy, Frame src, uint sx, uint sy)
        {
            dst.SetPixelSafe(dx, dy, src.GetPixelSafe(sx, sy));
        }

        /// <summary>
        /// Set pixel Safe from to img
        /// </summary>
        /// <param name="src">Source Image</param>
        /// <param name="px">Pixel px</param>
        /// <param name="py">Pixel py</param>
        /// <param name="col">Pixel Color</param>
        protected static void SetPixelSafe(Bitmap src, int px, int py, Color col)
        {
            if (px >= 0 && px < src.Width && py >= 0 && py < src.Height)
            {
                src.SetPixel(px, py, col);
            }
        }

        /// <summary>
        /// Copy a pixel from src to dst (safely)
        /// </summary>
        /// <param name="dst">Destination Frame</param>
        /// <param name="dx">Destination Pixel px</param>
        /// <param name="dy">Destination Pixel py</param>
        /// <param name="src">Source Frame</param>
        /// <param name="sx">Source Pixel px</param>
        /// <param name="sy">Source Pixel py</param>
        protected static void PixelCopySafe(ref Bitmap dst, int dx, int dy, Bitmap src, int sx, int sy)
        {
            if ((dx >= 0 && dx < dst.Width && dy >= 0 && dy < dst.Height)
                && (sx >= 0 && sx < src.Width && sy >= 0 && sy < src.Height))
            {
                dst.SetPixel(dx, dy, src.GetPixel(sx, sy));
            }
        }

        /// <summary>
        /// Check if two pixels are equal with some eqDiff [0..1]
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">eqDiff tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        protected static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
        {
            return (px == tx && py == ty) || ColorTest.PixelARGBEqual(src.GetPixel(px, py), src.GetPixel(tx, ty), deviation);            
        }

        /// <summary>
        /// Check if two pixels are equal with some eqDiff [0..1]
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">eqDiff tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        protected static bool PixelNotEqual(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
        {
            return ColorTest.PixelARGBNotEqual(src.GetPixel(px, py), src.GetPixel(tx, ty), deviation);            
        }

        protected static bool PixelEqual(Bitmap src, int px, int py, int tx, int ty, double deviation)
        {
            return (px == tx && py == ty) || ColorTest.PixelRGBEqual(src.GetPixel(px, py), src.GetPixel(tx, ty), deviation);            
        }

        protected static bool PixelNotEqual(Bitmap src, int px, int py, int tx, int ty, double deviation)
        {
            return ColorTest.PixelRGBNotEqual(src.GetPixel(px, py), src.GetPixel(tx, ty), deviation);            
        }

        public abstract void Process(
                Frame src, out Frame dst, double eqDiff, bool scale = true
        );

        public abstract void Process(
                Bitmap src, out Bitmap dst, double eqDiff, bool scale = true
        );
    }
}
