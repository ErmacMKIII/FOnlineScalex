using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Scalex
{
    public static class Scalex
    {
        //
        // scaler_scalex.c
        //

        // ========================
        //
        // ScaleNx pixel art scaler
        //
        // See: https://www.scale2x.it/
        // Also: https://web.archive.org/web/20140331104356/http://gimpscripts.com/2014/01/scale2x-plus/
        //
        // Adapted to C (was python) from : https://opengameart.org/forumtopic/pixelart-scaler-scalenx-and-eaglenx-for-gimp
        //
        // ========================

        public const uint BYTE_SIZE_RGBA_4BPP = 4; // RGBA 4BPP        

        /// <summary>
        /// Copy a pixel from src to dst
        /// </summary>
        /// <param name="dst">Destination Frame</param>
        /// <param name="dx">Destination Pixel px</param>
        /// <param name="dy">Destination Pixel py</param>
        /// <param name="src">Source Frame</param>
        /// <param name="sx">Source Pixel px</param>
        /// <param name="sy">Source Pixel py</param>
        private static void PixelCopy(ref Frame dst, uint dx, uint dy, Frame src, uint sx, uint sy)
        {
            dst.SetPixel(dx, dy, src.GetPixel(sx, sy));
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
        private static void PixelCopySafe(ref Frame dst, uint dx, uint dy, Frame src, uint sx, uint sy)
        {
            dst.SetPixelSafe(dx, dy, src.GetPixelSafe(sx, sy));
        }

        /// <summary>
        /// Check if two pixels are equal with some deviation [0..1]
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">deviation tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        private static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.Deviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
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
        private static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty)
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
        private static bool PixelEqualSafe(Frame src, uint px, uint py, uint tx, uint ty)
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
        private static bool PixelEqualSafe(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
        {
            if (px == tx && py == ty)
            {
                return true;
            }

            if (Palette.Deviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        private static void Scalex2xHelper(Frame src, ref Frame dst, uint px, uint py, double deviation)
        {
            uint xL, xR; // x-Left, x-Right
            uint yB, yT; // y-Bottom, y-Top            

            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yB = py - 1; } else { yB = 0; }
            if (py < h - 1) { yT = py + 1; } else { yT = h - 1; }

            // calculating near pixels => A, B, C, D
            uint Ax = px;
            uint Ay = yT;

            uint Bx = xR;
            uint By = py;

            uint Cx = xL;
            uint Cy = py;

            uint Dx = px;
            uint Dy = yT;

            /*
                1=P; 2=P; 3=P; 4=P;
                IF C==A AND C!=D AND A!=B => 1=A
                IF A==B AND A!=C AND B!=D => 2=B
                IF D==C AND D!=B AND C!=A => 3=C
                IF B==D AND B!=A AND D!=C => 4=D
            */
            PixelCopy(ref dst, xL, yT, src, px, py);
            PixelCopy(ref dst, xR, yT, src, px, py);
            PixelCopy(ref dst, xL, yB, src, px, py);
            PixelCopy(ref dst, xL, yB, src, px, py);

            if (PixelEqual(src, Cx, Cy, Ax, Ay, deviation) && !PixelEqual(src, Cx, Cy, Dx, Dy, deviation) && !PixelEqual(src, Ax, Ay, Bx, By, deviation))
            {
                PixelCopy(ref dst, xL, yT, src, Ax, Ay);
            }
            if (PixelEqual(src, Ax, Ay, Bx, By, deviation) && !PixelEqual(src, Ax, Ay, Cx, Cy, deviation) && !PixelEqual(src, Bx, By, Dx, Dy, deviation))
            {
                PixelCopy(ref dst, xR, yT, src, Bx, By);
            }
            if (PixelEqual(src, Dx, Dy, Cx, Cy, deviation) && !PixelEqual(src, Dx, Cy, Bx, By, deviation) && !PixelEqual(src, Cx, Cy, Ax, Ay, deviation))
            {
                PixelCopy(ref dst, xL, yB, src, Cx, Cy);
            }
            if (PixelEqual(src, Bx, By, Dx, Dy, deviation) && !PixelEqual(src, Bx, By, Ax, Ay, deviation) && !PixelEqual(src, Dx, Dy, Cx, Cy, deviation))
            {
                PixelCopy(ref dst, xL, yB, src, Dx, Dy);
            }

        }

        // ----------------------------------------------------------------------------------------
        // scaler_scalex_2x
        //
        // Scales image in *src up by 2x into *dst
        //
        // *src : pointer to source uint32 buffer of Xres * Yres, 4BPP RGBA
        // *dst : pointer to output uint32 buffer of 2 * Xres * 2 * Yres, 4BPP RGBA
        // Xres, Yres: resolution of source image
        // ----------------------------------------------------------------------------------------
        /// <summary>
        /// Scales image in *src up by 2x into *dst
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="deviation">deviation tolerance (pixel difference), in range [0,1]</param>
        public static void Scalex2x(Frame src, out Frame dst, double deviation)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;  

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (py = 0; py < h; py++)
            {
                for (px = 0; px < w; px++)
                {
                    Scalex2xHelper(src, ref dst, px, py, deviation);                    
                }
            }
        }
        
    }
}
