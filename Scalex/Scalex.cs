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
        /// Check if two pixels are equal with some eqDiff [0..1]
        /// </summary>
        /// <param name="src">Source Frame (where comparison is being performed)</param>
        /// <param name="px">first pixel px</param>
        /// <param name="py">first pixel py</param>
        /// <param name="tx">other (target) pixel px</param>
        /// <param name="ty">other (target) pixel py</param>        
        /// <param name="deviation">eqDiff tolerance (pixel difference), in range [0,1]</param>
        /// <returns></returns>
        private static bool PixelEqual(Frame src, uint px, uint py, uint tx, uint ty, double deviation)
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

            if (Palette.RGBDeviation(src.GetPixel(px, py), src.GetPixel(tx, ty)) > deviation)
            {
                return false;
            }

            return true;
        }

        /*
            Starting from this pattern :
            A	B	C
            D	E	F
            G	H	I
            The central pixel E is expanded in 4 new pixels:

            E0	E1
            E2	E3
            with these rules (in C language) :

            E0 = D == B && B != F && D != H ? D : E;
            E1 = B == F && B != D && F != H ? F : E;
            E2 = D == H && D != B && H != F ? D : E;
            E3 = H == F && D != H && B != F ? F : E;
            which can be rewritten as :

            E0 = D == B && B != H && D != F ? D : E;
            E1 = B == F && B != H && D != F ? F : E;
            E2 = D == H && B != H && D != F ? D : E;
            E3 = H == F && B != H && D != F ? F : E;
            and optimized as:

            if (B != H && D != F) {
	            E0 = D == B ? D : E;
	            E1 = B == F ? F : E;
	            E2 = D == H ? D : E;
	            E3 = H == F ? F : E;
            } else {
	            E0 = E;
	            E1 = E;
	            E2 = E;
	            E3 = E;
            }
        */
        /// <summary>
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        /// <param name="eqDiff">difference when equal</param>
        /// <param name="eqDiff">difference when not equal</param>
        private static void Scalex2xHelper(Frame src, ref Frame dst, uint px, uint py, double eqDiff)
        {
            uint xL, xR; // x-Left, x-Right
            uint yB, yT; // y-Bottom, y-Top            

            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yB = py - 1; } else { yB = 0; }
            if (py < h - 1) { yT = py + 1; } else { yT = h - 1; }

            uint Bx = px;
            uint By = yT;

            uint Dx = xR;
            uint Dy = py;

            uint Fx = xL;
            uint Fy = py;

            uint Hx = px;
            uint Hy = yT;

            uint Ex = px;
            uint Ey = py;

            /*
                  if (B != H && D != F) {
	                E0 = D == B ? D : E;
	                E1 = B == F ? F : E;
	                E2 = D == H ? D : E;
	                E3 = H == F ? F : E;
                } else {
	                E0 = E;
	                E1 = E;
	                E2 = E;
	                E3 = E;
                }
            */

            /*
               E0[xL,yT]  E1[xR,yT]
               E2[xL,yB]  E3[xR,yB]
            */

            if (!PixelEqual(src, Bx, By, Hx, Hy, eqDiff) && !PixelEqual(src, Dx, Dy, Fx, Fy, eqDiff))
            {
               if (PixelEqual(src, Dx, Dy, Bx, By, eqDiff))
               {
                   PixelCopy(ref dst, xL, yT, src, Dx, Dy);
               }
               else
               {
                   PixelCopy(ref dst, xL, yT, src, Ex, Ey);
               }

               if (PixelEqual(src, Bx, By, Fx, Fy, eqDiff))
               {
                   PixelCopy(ref dst, xR, yT, src, Fx, Fy);
               }
               else
               {
                   PixelCopy(ref dst, xR, yT, src, Ex, Ey);
               }

               if (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff))
               {
                   PixelCopy(ref dst, xL, yB, src, Dx, Dy);
               }
               else
               {
                   PixelCopy(ref dst, xL, yB, src, Ex, Ey);
               }

               if (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff))
               {
                   PixelCopy(ref dst, xR, yB, src, Fx, Fy);
               }
               else
               {
                   PixelCopy(ref dst, xR, yB, src, Ex, Ey);
               }
           }
           else
           {
               PixelCopy(ref dst, xL, yT, src, Ex, Ey);
               PixelCopy(ref dst, xR, yT, src, Ex, Ey);
               PixelCopy(ref dst, xL, yB, src, Ex, Ey);
               PixelCopy(ref dst, xR, yB, src, Ex, Ey);
           }

       }

        /*
            The central pixel E is expanded in 9 new pixels:

            E0	E1	E2
            E3	E4	E5
            E6	E7	E8
            with these rules (in C language) :

            E0 = D == B && B != F && D != H ? D : E;
            E1 = (D == B && B != F && D != H && E != C) || (B == F && B != D && F != H && E != A) ? B : E;
            E2 = B == F && B != D && F != H ? F : E;
            E3 = (D == B && B != F && D != H && E != G) || (D == H && D != B && H != F && E != A) ? D : E;
            E4 = E
            E5 = (B == F && B != D && F != H && E != I) || (H == F && D != H && B != F && E != C) ? F : E;
            E6 = D == H && D != B && H != F ? D : E;
            E7 = (D == H && D != B && H != F && E != I) || (H == F && D != H && B != F && E != G) ? H : E;
            E8 = H == F && D != H && B != F ? F : E;
            and optimized as:

            if (B != H && D != F) {
                E0 = D == B ? D : E;
                E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                E2 = B == F ? F : E;
                E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                E4 = E;
                E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
                E6 = D == H ? D : E;
                E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                E8 = H == F ? F : E;
            } else {
                E0 = E;
                E1 = E;
                E2 = E;
                E3 = E;
                E4 = E;
                E5 = E;
                E6 = E;
                E7 = E;
                E8 = E;
            }
         */

        /// <summary>
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        /// <param name="eqDiff">difference when equal</param>
        /// <param name="eqDiff">difference when not equal</param>
        private static void Scalex3xHelper(Frame src, ref Frame dst, uint px, uint py, double eqDiff)
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
                if (B != H && D != F) {
	                E0 = D == B ? D : E;
	                E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
	                E2 = B == F ? F : E;
	                E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
	                E4 = E;
	                E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
	                E6 = D == H ? D : E;
	                E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
	                E8 = H == F ? F : E;
                } else {
	                E0 = E;
	                E1 = E;
	                E2 = E;
	                E3 = E;
	                E4 = E;
	                E5 = E;
	                E6 = E;
	                E7 = E;
	                E8 = E;
                }
            */
            //      E0[xL,yT] E1[x,yT] E2[xR, yT]
            //      E3[xL,y ] E4[x,y ] E5[xR, y ]
            //      E6[xL,yB] E7[x,yB] E8[xR, yB]
            if (!PixelEqual(src, Bx, By, Hx, Hy, eqDiff) && !PixelEqual(src, Dx, Dy, Fx, Fy, eqDiff))
            {
                // E0 = D == B ? D : E;
                if (PixelEqual(src, Dx, Dy, Bx, By, eqDiff))
                {
                    PixelCopy(ref dst, xL, yT, src, Dx, Dy);
                } 
                else
                {
                    PixelCopy(ref dst, xL, yT, src, Ex, Ey);
                }

                // E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                if ((PixelEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelEqual(src, Ex, Ey, Cx, Cy, eqDiff))
                    ||
                    (PixelEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    PixelCopy(ref dst, px, yT, src, Bx, By);
                }
                else
                {
                    PixelCopy(ref dst, px, yT, src, Ex, Ey);
                }

                // E2 = B == F ? F : E;
                if (PixelEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    PixelCopy(ref dst, xR, yT, src, Fx, Fy);
                }
                else
                {
                    PixelCopy(ref dst, xR, yT, src, Ex, Ey);
                }

                // E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                if ((PixelEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelEqual(src, Ex, Ey, Gx, Gy, eqDiff))
                    ||
                    (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    PixelCopy(ref dst, xL, py, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, py, src, Ex, Ey);
                }

                // E4 = E;
                PixelCopy(ref dst, px, py, src, Ex, Ey);

                // E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
                if ((PixelEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Cx, Cy, eqDiff)))
                {
                    PixelCopy(ref dst, xR, py, src, Fx, Fy);
                }
                else
                {
                    PixelCopy(ref dst, xR, py, src, Ex, Ey);
                }

                // E6 = D == H ? D : E;
                if (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    PixelCopy(ref dst, xL, yB, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, yB, src, Ex, Ey);
                }

                // E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                if ((PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Gx, Gy, eqDiff)))
                {
                    PixelCopy(ref dst, px, yB, src, Hx, Hy);
                }
                else
                {
                    PixelCopy(ref dst, px, yB, src, Ex, Ey);
                }

                // E8 = H == F ? F : E;
                if (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff))
                {
                    PixelCopy(ref dst, xR, yB, src, Fx, Fy);
                } 
                else
                {
                    PixelCopy(ref dst, xR, yB, src, Ex, Ey);
                }

            } 
            else
            {
                PixelCopy(ref dst, xL, yT, src, Ex, Ey);
                PixelCopy(ref dst, px, yT, src, Ex, Ey);
                PixelCopy(ref dst, px, yT, src, Ex, Ey);
                PixelCopy(ref dst, xR, yT, src, Ex, Ey);

                PixelCopy(ref dst, px, py, src, Ex, Ey);

                PixelCopy(ref dst, xL, yB, src, Ex, Ey);
                PixelCopy(ref dst, px, yB, src, Ex, Ey);
                PixelCopy(ref dst, px, yB, src, Ex, Ey);
                PixelCopy(ref dst, xR, yB, src, Ex, Ey);
            }
        }

        /// <summary>
        /// Scales image with Scale2x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1</param>
        public static void Scalex2x(Frame src, out Frame dst, double eqDiff)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;  

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (px = 0; px < w; px++)               
            {
                for (py = 0; py < h; py++) 
                { 
                    Scalex2xHelper(src, ref dst, px, py, eqDiff);                    
                }
            }
        }

        /// <summary>
        /// Scales image with Scale3x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1</param>
        public static void Scalex3x(Frame src, out Frame dst, double eqDiff)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;
            
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex3xHelper(src, ref dst, px, py, eqDiff);
                }
            }
        }

        /// <summary>
        /// Scales image with Scale4x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1</param>
        public static void Scalex4x(Frame src, out Frame dst, double eqDiff)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex2xHelper(src, ref dst, px, py, eqDiff);
                }
            }

            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex2xHelper(src, ref dst, px, py, eqDiff);
                }
            }
        }
    }
}
