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
    public class Scalex2x : Scalex
    {
        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------
        
        protected static void Scalex2xHelper(Frame src, ref Frame dst, uint px, uint py, double eqDiff)
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
       
        protected static void Scalex2xHelper(Bitmap src, ref Bitmap dst, int px, int py, double eqDiff)
        {
            int xL, xR; // x-Left, x-Right
            int yB, yT; // y-Bottom, y-Top            

            int w = (int)src.Width;
            int h = (int)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yB = py - 1; } else { yB = 0; }
            if (py < h - 1) { yT = py + 1; } else { yT = h - 1; }

            int Bx = px;
            int By = yT;

            int Dx = xR;
            int Dy = py;

            int Fx = xL;
            int Fy = py;

            int Hx = px;
            int Hy = yT;

            int Ex = px;
            int Ey = py;

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

            if (!PixelRGBAEqual(src, Bx, By, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Dx, Dy, Fx, Fy, eqDiff))
            {
                if (PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff))
                {
                    PixelCopy(ref dst, xL, yT, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, yT, src, Ex, Ey);
                }

                if (PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    PixelCopy(ref dst, xR, yT, src, Fx, Fy);
                }
                else
                {
                    PixelCopy(ref dst, xR, yT, src, Ex, Ey);
                }

                if (PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    PixelCopy(ref dst, xL, yB, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, yB, src, Ex, Ey);
                }

                if (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff))
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
        /// <summary>
        /// Scales image with Scale2x
        /// </summary>
        /// <param name="src">Source Bitmap</param>
        /// <param name="dst">Destination Bitmap</param>
        /// <param name="scale">Scale image</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1</param>
        /// <param name="scale">scale</param>
        public static void Scalex2xMeth(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            int w = (int)src.Width;
            int h = (int)src.Height;

            dst = new Bitmap(scale ? 2 * w : w, scale ? 2 * h : h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py;

            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex2xHelper(src, ref dst, px, py, eqDiff);
                }
            }
        }
        /// <summary>
        /// Scales image with Scale2x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="scale">Scale image</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1]</param>
        /// <param name="scale">scale</param>
        public static void Scalex2xMeth(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;
            
            dst = new Frame(scale ? 2 * w : w, scale ? 2 * h : h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (px = 0; px < w; px++)               
            {
                for (py = 0; py < h; py++) 
                { 
                    Scalex2xHelper(src, ref dst, px, py, eqDiff);                    
                }
            }
        }
        

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex2xMeth(src, out dst, eqDiff, scale);
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex2xMeth(src, out dst, eqDiff, scale);
        }

    }
}
