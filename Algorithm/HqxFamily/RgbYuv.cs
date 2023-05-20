using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm.HqxFamily
{
    public class RgbYuv
    {
        public const uint Ymask = 0x00FF0000;
        public const uint Umask = 0x0000FF00;
        public const uint Vmask = 0x000000FF;

        public const uint RgbMask = 0x00FFFFFF;
        private static uint[] RGBtoYUV = new uint[0x1000000];

        /// <summary>
        /// Convert RGB To Yuv.
        /// Returns the 24bit YUV equivalent of the provided 24bit RGB color.
        /// alpha component is dropped
        /// </summary>
        /// <param name="rgb">pixel color (RGB)</param>
        /// <returns>Yuv format</returns>
        public static uint RGBToYuv(uint rgb)
        {
            return RGBtoYUV[rgb & RgbMask];
        }

        /// <summary>
        /// Convert RGB To Yuv.
        /// Returns the 24bit YUV equivalent of the provided 24bit RGB color.
        /// alpha component is dropped
        /// </summary>
        /// <param name="color">pixel color (RGB)</param>
        /// <returns>Yuv format</returns>
        public static uint RGBToYuv(Color color)
        {
            return RGBtoYUV[color.ToArgb() & RgbMask];
        }        

        /// <summary>
        /// Convert YUV to RGB. Alpha is opaque.
        /// </summary>
        /// <param name="yuv">pixel in YUV format</param>
        /// <returns>pixel color (RGB) format</returns>
        public static uint YuvToRGB(uint yuv)
        {
            uint y = yuv & Ymask;
            uint u = yuv & Umask;
            uint v = yuv & Vmask;

            uint r = (uint)(y + 1.14 * v);
            uint g = (uint)(y - 0.395 * u - 0.581 * v);
            uint b = (uint)(y + 2.033 * v);

            return (uint)Color.FromArgb((int)r, (int)g, (int)b).ToArgb();
        }

        /// <summary>
        /// Calculates the lookup table. MUST be called (only once) before
        /// doing anything else
        /// </summary>        
        public static void RgbYuvInit()
        {
            /* Initalize RGB to YUV lookup table */
            if (RGBtoYUV == null)
            {
                RGBtoYUV = new uint[0x1000000];
            }
            uint r, g, b, y, u, v;
            for (uint c = 0x1000000 - 1; c >= 0; c--)
            {
                r = (c & 0xFF0000) >> 16;
                g = (c & 0x00FF00) >> 8;
                b = c & 0x0000FF;
                y = (uint)(+0.299d * r + 0.587d * g + 0.114d * b); // luma coefficients
                u = (uint)(-0.169d * r - 0.331d * g + 0.500d * b) + 128;
                v = (uint)(+0.500d * r - 0.419d * g - 0.081d * b) + 128;
                RGBtoYUV[c] = (y << 16) | (u << 8) | v;
            }
        }
    }
}
