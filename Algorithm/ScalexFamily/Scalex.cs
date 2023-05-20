using FOnlineScalex.Algorithm;
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
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.RGBDeviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if two pixels are equal
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>
        /// <returns></returns>
        protected static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (src.GetPixel(px, py) != src.GetPixel(tx, ty))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Check if two pixels are equal (safely)
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>
        /// <returns></returns>
        protected static bool PixelEqualSafe(Frame src, uint px, uint py, uint tx, uint ty)
        {
            if (px == tx && py == ty)
            {
                return true;
            }
            
            if (src.GetPixelSafe(px, py) != src.GetPixelSafe(tx, ty))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if two pixels are equal (safely)
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>
        /// <returns></returns>
        protected static bool PixelEqualSafe(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.RGBDeviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Check if two pixels are equal (RGBA) with some eqDiff [0..1]
        /// </summary>
        /// <param name="src">Source Bitmap (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">eqDiff tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        protected static bool PixelRGBAEqual(Bitmap src, int px, int py, int tx, int ty, double deviation)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.RGBADeviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if two pixels are equal (RGB) with some eqDiff [0..1]
        /// </summary>
        /// <param name="src">Source Bitmap (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">eqDiff tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        protected static bool PixelRGBEqual(Bitmap src, int px, int py, int tx, int ty, double deviation)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.RGBDeviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }

        public abstract void Process(
                Frame src, out Frame dst, double eqDiff, bool scale = true
        );

        public abstract void Process(
                Bitmap src, out Bitmap dst, double eqDiff, bool scale = true
        );
    }
}
