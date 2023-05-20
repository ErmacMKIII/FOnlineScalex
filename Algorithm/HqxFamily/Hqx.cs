using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm.HqxFamily
{
    /*
     * Copyright © 2003 Maxim Stepin (maxst@hiend3d.com)
     *
     * Copyright © 2010 Cameron Zemek (grom@zeminvaders.net)
     *
     * Copyright © 2011 Tamme Schichler (tamme.schichler@googlemail.com)
     *
     * Copyright © 2012 A. Eduardo García (arcnorj@gmail.com)
     */

    // --------------------------------
    // Algorithm provided from:
    // https://github.com/VincenzoLaSpesa/hqxcli-java
    // --------------------------------

    /// <summary>
    /// Maxim Stepin Hqx base class
    /// </summary>
    public abstract class Hqx : IAlgorithm
    {
        public const uint Ymask = 0x00FF0000;
        public const uint Umask = 0x0000FF00;
        public const uint Vmask = 0x000000FF;

        /// <summary>
        /// Compares two ARGB colors according to the provided Y, U, V and A thresholds.
        /// </summary>
        /// <param name="c1">ARGB Color</param>
        /// <param name="c2">ARGB Color</param>
        /// <param name="trY">U (chrominance) threshold</param>
        /// <param name="trU"></param>
        /// <param name="trV">V (chrominance) threshold</param>
        /// <param name="trA">A (transparency) threshold</param>
        /// <returns></returns>
        public static bool Diff(uint c1, uint c2, uint trY, uint trU, uint trV, uint trA)
        {
            uint YUV1 = RgbYuv.RGBToYuv(c1);
            uint YUV2 = RgbYuv.RGBToYuv(c2);

            return (
                (Math.Abs((YUV1 & Ymask) - (YUV2 & Ymask)) > trY) ||
                (Math.Abs((YUV1 & Umask) - (YUV2 & Umask)) > trU) ||
                (Math.Abs((YUV1 & Vmask) - (YUV2 & Vmask)) > trV) ||
                (Math.Abs(((c1 >> 24) - (c2 >> 24))) > trA)
            );
        }

        /// <summary>
        /// Compares two ARGB colors according to the provided Y, U, V and A thresholds.
        /// </summary>
        /// <param name="c1">ARGB Color</param>
        /// <param name="c2">ARGB Color</param>        
        /// <returns>nequal test</returns>
        public static bool PixelRGBNotEqual(uint c1, uint c2, double eqDiff)
        {
            return !PixelRGBEqual(c1, c2, eqDiff);
        }

        /// <summary>
        /// Compares two ARGB colors according to the provided Y, U, V and A thresholds.
        /// </summary>
        /// <param name="c1">ARGB Color</param>
        /// <param name="c2">ARGB Color</param>        
        /// <returns>nequal test</returns>
        public static bool PixelRGBANotEqual(uint c1, uint c2, double eqDiff)
        {
            return !PixelRGBAEqual(c1, c2, eqDiff);
        }

        /// <summary>
        /// Comparing if two pixel colors satisfy RGB color equality against eqDiff.
        /// </summary>
        /// <param name="c1">color</param>
        /// <param name="c2">other color</param>
        /// <param name="eqDiff">equal difference</param>
        /// <returns>equal test</returns>
        public static bool PixelRGBEqual(uint c1, uint c2, double eqDiff)
        {
            return Palette.RGBDeviation(Color.FromArgb((int)c1), Color.FromArgb((int)c2)) <= eqDiff;
        }

        /// <summary>
        /// Comparing if two pixel colors satisfy RGBA color equality against eqDiff.
        /// </summary>
        /// <param name="c1">color</param>
        /// <param name="c2">other color</param>
        /// <param name="eqDiff">equal difference</param>
        /// <returns>equal test</returns>
        public static bool PixelRGBAEqual(uint c1, uint c2, double eqDiff)
        {
            return Palette.RGBADeviation(Color.FromArgb((int)c1), Color.FromArgb((int)c2)) <= eqDiff;
        }

        /// <summary>
        /// Get Pixel Color (ARGB) from the image from color (ARGB) array at index pos.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static uint GetPixel(Bitmap img, uint pos)
        {
            uint px = (uint)(pos % img.Width);
            uint py = (uint)(pos / img.Width);

            return (uint)img.GetPixel((int)px, (int)py).ToArgb();
        }

        /// <summary>
        /// Set Pixel Color (ARGB) from the image from color (ARGB) array at index pos.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="pos"></param>
        /// <param name="col"></param>
        public static void SetPixel(Bitmap img, uint pos, uint col)
        {
            uint px = (uint)(pos % img.Width);
            uint py = (uint)(pos / img.Width);

            img.SetPixel((int)px, (int)py, Color.FromArgb((int)col));
        }

        public abstract void Process(
                Frame src, out Frame dst, double eqDiff, bool scale = true
        );

        public abstract void Process(
                Bitmap src, out Bitmap dst, double eqDiff, bool scale = true
        );
    }

}