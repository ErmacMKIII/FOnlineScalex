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


        // Check if two pixels are equal
        // TODO: RGBA Alpha handling, ignore Alpha byte?
        private static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty)
        {
            if (px == py && tx == ty)
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
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        private static void Scalex2xHelper(Frame src, ref Frame dst, uint px, uint py)
        {
            uint x0, x2;
            uint y0, y2;
            uint B, D, E, F, H;

            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            if (px > 0) { x0 = px - 1; } else { x0 = 0; }
            if (px < w - 1) { x2 = px + 1; } else { x2 = w - 1; }
            if (py > 0) { y0 = py - 1; } else { y0 = 0; }
            if (py < h - 1) { y2 = py + 1; } else { y2 = h - 1; }

            B = (uint)(px + y0);
            D = (uint)(x0 + py);
            E = (uint)(px + py);
            F = (uint)(x2 + py);
            H = (uint)(px + y2);

            if ((!PixelEqual(src, (uint)px, (uint)py, B, H)) && (!PixelEqual(src, (uint)px, (uint)py, D, F)))
            {

                if (PixelEqual(src, (uint)px, (uint)py, B, D)) { dst.SetPixel((uint)(px - 1), (uint)(py - 1), (byte)D); } else { dst.SetPixel((uint)(px - 1), (uint)(py - 1), (byte)E); }
                if (PixelEqual(src, (uint)px, (uint)py, B, F)) { dst.SetPixel((uint)(px + 1), (uint)(py - 1), (byte)F); } else { dst.SetPixel((uint)(px + 1), (uint)(py - 1), (byte)E); }
                if (PixelEqual(src, (uint)px, (uint)py, H, D)) { dst.SetPixel((uint)(px - 1), (uint)(py + 1), (byte)D); } else { dst.SetPixel((uint)(px - 1), (uint)(py + 1), (byte)E); }
                if (PixelEqual(src, (uint)px, (uint)py, H, F)) { dst.SetPixel((uint)(px + 1), (uint)(py + 1), (byte)F); } else { dst.SetPixel((uint)(px + 1), (uint)(py + 1), (byte)E); }
            }
            else
            {
                dst.SetPixel((uint)(px - 1), (uint)(py - 1), (byte)E);
                dst.SetPixel((uint)(px + 1), (uint)(py - 1), (byte)E);
                dst.SetPixel((uint)(px - 1), (uint)(py + 1), (byte)E);
                dst.SetPixel((uint)(px + 1), (uint)(py + 1), (byte)E);
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
        public static void Scalex2x(Frame src, out Frame dst)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;  

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (py = 0; py < h; py++)
            {
                for (px = 0; px < w; px++)
                {
                    Scalex2xHelper(src, ref dst, px, py);
                }
            }
        }
        
    }
}
