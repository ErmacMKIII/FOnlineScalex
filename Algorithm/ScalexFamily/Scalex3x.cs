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
    public class Scalex3x : Scalex
    {
        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------

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
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Bitmap</param>
        /// <param name="dst">Destination Bitmap</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        /// <param name="eqDiff">difference when equal</param>
        /// <param name="eqDiff">difference when not equal</param>
        private static void Scalex3xHelper(Bitmap src, ref Bitmap dst, int px, int py, double eqDiff)
        {
            int xL, xR; // x-Left, x-Right
            int yB, yT; // y-Bottom, y-Top            

            int w = (int)src.Width;
            int h = (int)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yB = py - 1; } else { yB = 0; }
            if (py < h - 1) { yT = py + 1; } else { yT = h - 1; }

            // calculating near pixels => A, B, C, D, E, F, G, H, I
            int Ax = xL;
            int Ay = yT;

            int Bx = px;
            int By = yT;

            int Cx = xR;
            int Cy = yT;

            int Dx = xL;
            int Dy = py;

            int Ex = px;
            int Ey = py;

            int Fx = xR;
            int Fy = py;

            int Gx = xL;
            int Gy = yB;

            int Hx = px;
            int Hy = yB;

            int Ix = xR;
            int Iy = yB;
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
            if (!PixelRGBAEqual(src, Bx, By, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Dx, Dy, Fx, Fy, eqDiff))
            {
                // E0 = D == B ? D : E;
                if (PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff))
                {
                    PixelCopy(ref dst, xL, yT, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, yT, src, Ex, Ey);
                }

                // E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Cx, Cy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    PixelCopy(ref dst, px, yT, src, Bx, By);
                }
                else
                {
                    PixelCopy(ref dst, px, yT, src, Ex, Ey);
                }

                // E2 = B == F ? F : E;
                if (PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    PixelCopy(ref dst, xR, yT, src, Fx, Fy);
                }
                else
                {
                    PixelCopy(ref dst, xR, yT, src, Ex, Ey);
                }

                // E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Gx, Gy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
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
                if ((PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Cx, Cy, eqDiff)))
                {
                    PixelCopy(ref dst, xR, py, src, Fx, Fy);
                }
                else
                {
                    PixelCopy(ref dst, xR, py, src, Ex, Ey);
                }

                // E6 = D == H ? D : E;
                if (PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    PixelCopy(ref dst, xL, yB, src, Dx, Dy);
                }
                else
                {
                    PixelCopy(ref dst, xL, yB, src, Ex, Ey);
                }

                // E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Gx, Gy, eqDiff)))
                {
                    PixelCopy(ref dst, px, yB, src, Hx, Hy);
                }
                else
                {
                    PixelCopy(ref dst, px, yB, src, Ex, Ey);
                }

                // E8 = H == F ? F : E;
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
        /// Scales image with Scale3x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="scale">scale image</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1]</param>
        public static void Scalex3xMeth(Frame src, out Frame dst, double eqDiff, bool scale)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            dst = new Frame(scale ? 3 * w : w, scale ? 3 * h : h, src.OffsetX, src.OffsetY);

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
        /// Scales image with Scale3x
        /// </summary>
        /// <param name="src">Source Image</param>
        /// <param name="dst">Destination Image</param>
        /// <param name="scale">Scale image</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="eqDiff">difference when not equal [0..1]</param>
        public static void Scalex3xMeth(Bitmap src, out Bitmap dst, double eqDiff, bool scale)
        {
            int w = (int)src.Width;
            int h = (int)src.Height;

            dst = new Bitmap(scale ? 3 * w : w, scale ? 3 * h : h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py;

            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Scalex3xHelper(src, ref dst, px, py, eqDiff);
                }
            }
        }

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex3xMeth(src, out dst, eqDiff, scale);
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex3xMeth(src, out dst, eqDiff, scale);
        }
    }
}
