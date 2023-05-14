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
                1  2
                3  4

                1=P; 2=P; 3=P; 4=P;
                IF C==A AND C!=D AND A!=B => 1=A
                IF A==B AND A!=C AND B!=D => 2=B
                IF D==C AND D!=B AND C!=A => 3=C
                IF B==D AND B!=A AND D!=C => 4=D
            */
            PixelCopy(ref dst, xL, yT, src, px, py);
            PixelCopy(ref dst, xR, yT, src, px, py);
            PixelCopy(ref dst, xL, yB, src, px, py);
            PixelCopy(ref dst, xR, yB, src, px, py);

            if (PixelEqual(src, Cx, Cy, Ax, Ay, deviation) && !PixelEqual(src, Cx, Cy, Dx, Dy) && !PixelEqual(src, Ax, Ay, Bx, By))
            {
                PixelCopy(ref dst, xL, yT, src, Ax, Ay);
            }
            if (PixelEqual(src, Ax, Ay, Bx, By, deviation) && !PixelEqual(src, Ax, Ay, Cx, Cy) && !PixelEqual(src, Bx, By, Dx, Dy))
            {
                PixelCopy(ref dst, xR, yT, src, Bx, By);
            }
            if (PixelEqual(src, Dx, Dy, Cx, Cy, deviation) && !PixelEqual(src, Dx, Cy, Bx, By) && !PixelEqual(src, Cx, Cy, Ax, Ay))
            {
                PixelCopy(ref dst, xL, yB, src, Cx, Cy);
            }
            if (PixelEqual(src, Bx, By, Dx, Dy, deviation) && !PixelEqual(src, Bx, By, Ax, Ay) && !PixelEqual(src, Dx, Dy, Cx, Cy))
            {
                PixelCopy(ref dst, xL, yB, src, Dx, Dy);
            }

        }

        /// <summary>
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        private static void Scalex3xHelper(Frame src, ref Frame dst, uint px, uint py, double deviation)
        {
            uint xL, xR; // x-Left, x-Right
            uint yB, yT; // y-Bottom, y-Top            

            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yB = py - 1; } else { yB = 0; }
            if (py < h - 1) { yT = py + 1; } else { yT = h - 1; }

            // calculating near pixels => A, B, C, D, E, F, G, H, I
            uint Ax = xL;
            uint Ay = yT;

            uint Bx = px;
            uint By = yT;

            uint Cx = xR;
            uint Cy = yT;            

            uint Dx = xL;
            uint Dy = py;

            uint Ex = px;
            uint Ey = py;

            uint Fx = xR;
            uint Fy = py;

            uint Gx = xL;
            uint Gy = yB;

            uint Hx = px;
            uint Hy = yB;

            uint Ix = xR;
            uint Iy = yB;
            /*
                 1 2 3
                 4 5 6
                 7 8 9
             
                 1=E; 2=E; 3=E; 4=E; 5=E; 6=E; 7=E; 8=E; 9=E;
                 IF D==B AND D!=H AND B!=F => 1=D
                 IF (D==B AND D!=H AND B!=F AND E!=C) OR (B==F AND B!=D AND F!=H AND E!=A) => 2=B
                 IF B==F AND B!=D AND F!=H => 3=F
                 IF (H==D AND H!=F AND D!=B AND E!=A) OR (D==B AND D!=H AND B!=F AND E!=G) => 4=D
                 5=E
                 IF (B==F AND B!=D AND F!=H AND E!=I) OR (F==H AND F!=B AND H!=D AND E!=C) => 6=F
                 IF H==D AND H!=F AND D!=B => 7=D
                 IF (F==H AND F!=B AND H!=D AND E!=G) OR (H==D AND H!=F AND D!=B AND E!=I) => 8=H
                 IF F==H AND F!=B AND H!=D => 9=F
            */
            PixelCopy(ref dst, xL, yT, src, px, py);
            PixelCopy(ref dst, xR, yT, src, px, py);
            PixelCopy(ref dst, xL, yB, src, px, py);
            PixelCopy(ref dst, xR, yB, src, px, py);

            PixelCopy(ref dst, px, py, src, px, py);

            PixelCopy(ref dst, px, yB, src, px, py);
            PixelCopy(ref dst, px, yT, src, px, py);
            PixelCopy(ref dst, xL, py, src, px, py);
            PixelCopy(ref dst, xR, py, src, px, py);
            //      1 2 3
            //      4 5 6
            //      7 8 9

            //      1[xL,yT] 2[x,yT] 3[xR, yT]
            //      4[xL,y ] 5[x,y ] 6[xR, y ]
            //      7[xL,yB] 8[x,yB] 9[xR, yB]
            if (PixelEqual(src, Dx, Dy, Bx, By, deviation) && !PixelEqual(src, Dx, Dy, Hx, Hy) && !PixelEqual(src, Bx, By, Fx, Fy))
            {
                PixelCopy(ref dst, xL, yT, src, Dx, Dy);
            }

            if ((PixelEqual(src, Dx, Dy, Bx, By, deviation) && !PixelEqual(src, Dx, Dy, Hx, Hy) && !PixelEqual(src, Bx, By, Fx, Fy) && !PixelEqual(src, Ex, Ey, Cx, Cy))
                ||
                (PixelEqual(src, Bx, By, Fx, Fy, deviation) && !PixelEqual(src, Bx, By, Dx, Dy) && !PixelEqual(src, Fx, Fy, Hx, Hy) && !PixelEqual(src, Ex, Ey, Ax, Ay)))
            {
                PixelCopy(ref dst, px, yT, src, Bx, By);
            }

            if (PixelEqual(src, Bx, By, Fx, Fy, deviation) && !PixelEqual(src, Bx, By, Dx, Dy) && !PixelEqual(src, Fx, Fy, Hx, Hy))
            {
                PixelCopy(ref dst, xR, yT, src, Fx, Fy);
            }

            if ((PixelEqual(src, Hx, Hy, Dx, Dy, deviation) && !PixelEqual(src, Hx, Hy, Fx, Fy) && !PixelEqual(src, Dx, Dy, Bx, By) && !PixelEqual(src, Ex, Ey, Ax, Ay))
                ||
                (PixelEqual(src, Dx, Dy, Bx, By, deviation) && !PixelEqual(src, Dx, Dy, Hx, Hy) && !PixelEqual(src, Bx, By, Fx, Fy) && !PixelEqual(src, Ex, Ey, Gx, Gy)))
            {
                PixelCopy(ref dst, px, yT, src, Bx, By);
            }

            PixelCopy(ref dst, px, py, src, Ex, Ey);

            if ((PixelEqual(src, Bx, By, Fx, Fy, deviation) && !PixelEqual(src, Bx, By, Dx, Dy) && !PixelEqual(src, Fx, Fy, Hx, Hy) && !PixelEqual(src, Ex, Ey, Ix, Iy))
                ||
                (PixelEqual(src, Fx, Fy, Hx, Hy, deviation) && !PixelEqual(src, Fx, Fy, Bx, By) && !PixelEqual(src, Hx, Hy, Dx, Dy) && !PixelEqual(src, Ex, Ey, Cx, Cy)))
            {
                PixelCopy(ref dst, xR, py, src, Fx, Fy);
            }

            if (PixelEqual(src, Hx, Hy, Dx, Dy, deviation) && !PixelEqual(src, Hx, Hy, Fx, Fy) && !PixelEqual(src, Dx, Dy, Bx, By))
            {
                PixelCopy(ref dst, xL, yB, src, Dx, Dy);
            }

            if ((PixelEqual(src, Fx, Fy, Hx, Hy, deviation) && !PixelEqual(src, Fx, Fy, Bx, By) && !PixelEqual(src, Hx, Hy, Dx, Dy) && !PixelEqual(src, Ex, Ey, Gx, Gy))
                ||
                (PixelEqual(src, Hx, Hy, Dx, Dy, deviation) && !PixelEqual(src, Hx, Hy, Fx, Fy) && !PixelEqual(src, Dx, Dy, Bx, By) && !PixelEqual(src, Ex, Ey, Ix, Iy)))
            {
                PixelCopy(ref dst, px, yT, src, Bx, By);
            }

            if (PixelEqual(src, Fx, Fy, Hx, Hy, deviation) && !PixelEqual(src, Fx, Fy, Bx, By) && !PixelEqual(src, Hx, Hy, Dx, Dy))
            {
                PixelCopy(ref dst, xR, yB, src, Fx, Fy);
            }
        }
        
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

            for (px = 0; px < w; px++)               
            {
                for (py = 0; py < h; py++) 
                { 
                    Scalex2xHelper(src, ref dst, px, py, deviation);                    
                }
            }
        }

        /// <summary>
        /// Scales image in *src up by 2x into *dst
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="deviation">deviation tolerance (pixel difference), in range [0,1]</param>
        public static void Scalex3x(Frame src, out Frame dst, double deviation)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;
            
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex3xHelper(src, ref dst, px, py, deviation);
                }
            }
        }
    }
}
