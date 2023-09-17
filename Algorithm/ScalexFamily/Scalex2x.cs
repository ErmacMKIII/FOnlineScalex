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
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.ScalexFamily
{
    public class Scalex2x : Scalex
    {
        public struct ExpandorBitmap
        {
            public Color E0 { get; set; }
            public Color E1 { get; set; }
            public Color E2 { get; set; }
            public Color E3 { get; set; }
        }
        public struct ExpandorFrame
        {
            public byte E0 { get; set; }
            public byte E1 { get; set; }
            public byte E2 { get; set; }
            public byte E3 { get; set; }
        }

        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------

        protected static ExpandorFrame Scalex2xHelper(Frame src, uint px, uint py, double eqDiff)
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

            uint Bx = px;
            uint By = yT;

            uint Dx = xL;
            uint Dy = py;

            uint Fx = xR;
            uint Fy = py;

            uint Hx = px;
            uint Hy = yB;

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
                    result.E0 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E0 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    result.E1 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E1 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    result.E2 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E2 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff))
                {
                    result.E2 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E3 = src.GetPixel(Ex, Ey);
                }
            }
            else
            {
                result.E0 = src.GetPixel(Ex, Ey);
                result.E1 = src.GetPixel(Ex, Ey);
                result.E2 = src.GetPixel(Ex, Ey);
                result.E3 = src.GetPixel(Ex, Ey);
            }

            return result;
        }

        protected static ExpandorBitmap Scalex2xHelper(Bitmap src, int px, int py, double eqDiff)
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

            int Bx = px;
            int By = yT;

            int Dx = xL;
            int Dy = py;

            int Fx = xR;
            int Fy = py;

            int Hx = px;
            int Hy = yB;

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

            if (!PixelEqual(src, Bx, By, Hx, Hy, eqDiff) && !PixelEqual(src, Dx, Dy, Fx, Fy, eqDiff))
            {
                if (PixelEqual(src, Dx, Dy, Bx, By, eqDiff))
                {
                    result.E0 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E0 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Bx, By, Fx, Fy, eqDiff))
                {
                    result.E1 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E1 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Dx, Dy, Hx, Hy, eqDiff))
                {
                    result.E2 = src.GetPixel(Dx, Dy);
                }
                else
                {
                    result.E2 = src.GetPixel(Ex, Ey);
                }

                if (PixelEqual(src, Hx, Hy, Fx, Fy, eqDiff))
                {
                    result.E2 = src.GetPixel(Fx, Fy);
                }
                else
                {
                    result.E3 = src.GetPixel(Ex, Ey);
                }
            }
            else
            {
                result.E0 = src.GetPixel(Ex, Ey);
                result.E1 = src.GetPixel(Ex, Ey);
                result.E2 = src.GetPixel(Ex, Ey);
                result.E3 = src.GetPixel(Ex, Ey);
            }

            return result;
        }
        /// <summary>
        /// Scales image with Scale2x
        /// </summary>
        /// <param name="src">Source Bitmap</param>
        /// <param name="dst">Destination Bitmap</param>
        /// <param name="scale">Scale image</param>
        /// <param name="eqDiff">difference when equal [0..1]</param>
        /// <param name="scale">scale</param>
        public static void Scalex2xMeth(Bitmap src, out Bitmap dst, double eqDiff)
        {
            int w = (int)src.Width;
            int h = (int)src.Height;

            w *= 2;
            h *= 2;

            dst = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
   
            int px, py;

            for (px = 0; px < src.Width; px++)
            {
                for (py = 0; py < src.Height; py++)
                {
                    ExpandorBitmap e = Scalex2xHelper(src, px, py, eqDiff);                    

                    SetPixelSafe(dst, 2 * px, 2 * py, e.E0);
                    SetPixelSafe(dst, 2 * px + 1, 2 * py + 1, e.E1);
                    SetPixelSafe(dst, 2 * px + 2, 2 * py + 2, e.E2);
                    SetPixelSafe(dst, 2 * px + 3, 2 * py + 3, e.E3);
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
        /// <param name="scale">scale</param>
        public static void Scalex2xMeth(Frame src, out Frame dst, double eqDiff)
        {
            uint w = (uint)src.Width;
            uint h = (uint)src.Height;

            w *= 2;
            h *= 2;

            dst = new Frame(w, h, 2 * src.OffsetX, 2 * src.OffsetY);

            uint px, py;

            for (px = 0; px < src.Width; px++)
            {
                for (py = 0; py < src.Height; py++)
                {
                    ExpandorFrame e = Scalex2xHelper(src, px, py, eqDiff);

                    dst.SetPixelSafe(2 * px, 2 * py, e.E0);
                    dst.SetPixelSafe(2 * px + 1, 2 * py + 1, e.E1);
                    dst.SetPixelSafe(2 * px + 2, 2 * py + 2, e.E2);
                    dst.SetPixelSafe(2 * px + 3, 2 * py + 3, e.E3);
                }
            }
        }


        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex2xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Frame(new Bitmap(dst.ToBitmap(), (int)src.Width, (int)src.Height), src.OffsetX, src.OffsetY);
            }
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex2xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Bitmap(dst, (int)src.Width, (int)src.Height);
            }
        }

    }
}
