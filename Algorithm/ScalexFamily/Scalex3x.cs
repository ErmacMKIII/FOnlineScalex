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
        public struct ExpandorBitmap
        {
            public Color E0 { get; set; }
            public Color E1 { get; set; }
            public Color E2 { get; set; }
            public Color E3 { get; set; }
            public Color E4 { get; set; }
            public Color E5 { get; set; }
            public Color E6 { get; set; }
            public Color E7 { get; set; }
            public Color E8 { get; set; }
        }
        public struct ExpandorFrame
        {
            public byte E0 { get; set; }
            public byte E1 { get; set; }
            public byte E2 { get; set; }
            public byte E3 { get; set; }
            public byte E4 { get; set; }
            public byte E5 { get; set; }
            public byte E6 { get; set; }
            public byte E7 { get; set; }
            public byte E8 { get; set; }
        }

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
        /// <param name="scale">scale image</param>
        private static ExpandorFrame Scalex3xHelper(Frame src, uint px, uint py, double eqDiff)
        {
            ExpandorFrame result = new ExpandorFrame();

            uint xL, xR; // x-Left, x-Right
            uint yB, yT; // y-Bottom, y-Top            

            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yT = py - 1; } else { yT = 0; }
            if (py < h - 1) { yB = py + 1; } else { yB = h - 1; }

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
                    result.E0 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E0 = src.GetPixel(Ex, Ey);
                }

                // E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                if ((PixelEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelEqual(src, Ex, Ey, Cx, Cy, eqDiff))
                    ||
                    (PixelEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    result.E1 = src.GetPixel(Bx, By);
                }
                else
                {
                    result.E1 = src.GetPixel(Ex, Ey);
                }

                // E2 = B == F ? F : E;
                if (PixelEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    result.E2 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E2 = src.GetPixel(Ex, Ey);
                }

                // E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                if ((PixelEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelEqual(src, Ex, Ey, Gx, Gy, eqDiff))
                    ||
                    (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    result.E3 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E3 = src.GetPixel(Ex, Ey);
                }

                // E4 = E;
                result.E4 = src.GetPixel(Ex, Ey);

                // E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
                if ((PixelEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Cx, Cy, eqDiff)))
                {
                    result.E5 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E5 = src.GetPixel(Ex, Ey);
                }

                // E6 = D == H ? D : E;
                if (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    result.E6 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E6 = src.GetPixel(Ex, Ey);
                }

                // E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                if ((PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelEqual(src, Ex, Ey, Gx, Gy, eqDiff)))
                {
                    result.E7 = src.GetPixel(Hx, Hy);
                }
                else
                {
                    result.E7 = src.GetPixel(Ex, Ey);
                }

                // E8 = H == F ? F : E;
                if (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff))
                {
                    result.E8 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E8 = src.GetPixel(Fx, Fy);
                }

            }
            else
            {
                result.E0 = src.GetPixel(Ex, Ey);
                result.E1 = src.GetPixel(Ex, Ey);
                result.E2 = src.GetPixel(Ex, Ey);
                result.E3 = src.GetPixel(Ex, Ey);
                result.E4 = src.GetPixel(Ex, Ey);
                result.E5 = src.GetPixel(Ex, Ey);
                result.E6 = src.GetPixel(Ex, Ey);
                result.E7 = src.GetPixel(Ex, Ey);
                result.E8 = src.GetPixel(Ex, Ey);
            }

            return result;
        }


        /// <summary>
        /// Return adjacent pixel values for given pixel
        /// </summary>
        /// <param name="src">Source Bitmap</param>
        /// <param name="px">Pixel x</param>
        /// <param name="py">Pixel y</param>
        /// <param name="eqDiff">difference when equal</param>
        /// <returns>expandor (9 pixels)</returns>
        private static ExpandorBitmap Scalex3xHelper(Bitmap src, int px, int py, double eqDiff)
        {
            ExpandorBitmap result = new ExpandorBitmap();

            int xL, xR; // x-Left, x-Right
            int yB, yT; // y-Bottom, y-Top            

            int w = (int)src.Width;
            int h = (int)src.Height;

            if (px > 0) { xL = px - 1; } else { xL = 0; }
            if (px < w - 1) { xR = px + 1; } else { xR = w - 1; }
            if (py > 0) { yT = py - 1; } else { yT = 0; }
            if (py < h - 1) { yB = py + 1; } else { yB = h - 1; }

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
                    result.E0 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E0 = src.GetPixel(Ex, Ey);
                }

                // E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Cx, Cy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    result.E1 = src.GetPixel(Bx, By);
                }
                else
                {
                    result.E1 = src.GetPixel(Ex, Ey);
                }

                // E2 = B == F ? F : E;
                if (PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    result.E2 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E2 = src.GetPixel(Ex, Ey);
                }

                // E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Bx, By, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Gx, Gy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ax, Ay, eqDiff)))
                {
                    result.E3 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E3 = src.GetPixel(Ex, Ey);
                }

                // E4 = E;
                result.E4 = src.GetPixel(Ex, Ey);

                // E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
                if ((PixelRGBAEqual(src, Bx, By, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Cx, Cy, eqDiff)))
                {
                    result.E5 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E5 = src.GetPixel(Ex, Ey);
                }

                // E6 = D == H ? D : E;
                if (PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    result.E6 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E6 = src.GetPixel(Ex, Ey);
                }

                // E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                if ((PixelRGBAEqual(src, Dx, Dy, Hx, Hy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Ix, Iy, eqDiff))
                    ||
                    (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff) && !PixelRGBAEqual(src, Ex, Ey, Gx, Gy, eqDiff)))
                {
                    result.E7 = src.GetPixel(Hx, Hy);
                }
                else
                {
                    result.E7 = src.GetPixel(Ex, Ey);
                }

                // E8 = H == F ? F : E;
                if (PixelRGBAEqual(src, Hx, Hy, Fx, Fy, eqDiff))
                {
                    result.E8 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E8 = src.GetPixel(Fx, Fy);
                }

            }
            else
            {
                result.E0 = src.GetPixel(Ex, Ey);
                result.E1 = src.GetPixel(Ex, Ey);
                result.E2 = src.GetPixel(Ex, Ey);
                result.E3 = src.GetPixel(Ex, Ey);
                result.E4 = src.GetPixel(Ex, Ey);
                result.E5 = src.GetPixel(Ex, Ey);
                result.E6 = src.GetPixel(Ex, Ey);
                result.E7 = src.GetPixel(Ex, Ey);
                result.E8 = src.GetPixel(Ex, Ey);
            }

            return result;
        }

        /// <summary>
        /// Scales image with Scale3x
        /// </summary>
        /// <param name="src">Source Frame</param>
        /// <param name="dst">Destination Frame</param>
        /// <param name="scale">scale image</param>
        /// <param name="eqDiff">difference when not equal [0..1]</param>
        public static void Scalex3xMeth(Frame src, out Frame dst, double eqDiff)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            w *= 3;
            h *= 3;

            dst = new Frame(w, h, src.OffsetX, src.OffsetY);

            uint px, py;

            for (px = 0; px < src.Width; px++)
            {
                for (py = 0; py < src.Height; py++)
                {
                    ExpandorFrame e = Scalex3xHelper(src, px, py, eqDiff);

                    dst.SetPixelSafe(3 * px, 3 * py, e.E0);
                    dst.SetPixelSafe(3 * px + 1, 3 * py + 1, e.E1);
                    dst.SetPixelSafe(3 * px + 2, 3 * py + 2, e.E2);
                    dst.SetPixelSafe(3 * px + 3, 3 * py + 3, e.E3);
                    dst.SetPixelSafe(3 * px + 4, 3 * py + 4, e.E4);
                    dst.SetPixelSafe(3 * px + 5, 3 * py + 5, e.E5);
                    dst.SetPixelSafe(3 * px + 6, 3 * py + 6, e.E6);
                    dst.SetPixelSafe(3 * px + 7, 3 * py + 7, e.E7);
                    dst.SetPixelSafe(3 * px + 8, 3 * py + 8, e.E8);

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
        public static void Scalex3xMeth(Bitmap src, out Bitmap dst, double eqDiff)
        {
            int w = (int)src.Width;
            int h = (int)src.Height;

            w *= 3;
            h *= 3;

            dst = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py; 

            for (px = 0; px < src.Width; px++)
            {
                for (py = 0; py < src.Height; py++)
                {
                    ExpandorBitmap e = Scalex3xHelper(src, px, py, eqDiff);

                    SetPixelSafe(dst, 3 * px, 3 * py, e.E0);
                    SetPixelSafe(dst, 3 * px + 1, 3 * py + 1, e.E1);
                    SetPixelSafe(dst, 3 * px + 2, 3 * py + 2, e.E2);
                    SetPixelSafe(dst, 3 * px + 3, 3 * py + 3, e.E3);
                    SetPixelSafe(dst, 3 * px + 4, 3 * py + 4, e.E4);
                    SetPixelSafe(dst, 3 * px + 5, 3 * py + 5, e.E5);
                    SetPixelSafe(dst, 3 * px + 6, 3 * py + 6, e.E6);
                    SetPixelSafe(dst, 3 * px + 7, 3 * py + 7, e.E7);
                    SetPixelSafe(dst, 3 * px + 8, 3 * py + 8, e.E8);
                }
            }
        }

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex3xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Frame(new Bitmap(src.ToBitmap(), (int)src.Width, (int)src.Height), src.OffsetX, src.OffsetY);
            }
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex3xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Bitmap(src, (int)src.Width, (int)src.Height);
            }
        }
    }
}
