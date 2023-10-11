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
using FOnlineScalex.Examination;
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
        /// Compares two ARGB colors according, are they equal against corresponding difference.
        /// </summary>
        /// <param name="c1">ARGB Color 1</param>
        /// <param name="c2">ARGB Color 2</param>        
        /// <returns>equal test</returns>
        public static bool PixelEqual(uint c1, uint c2, double eqDiff)
        {           
            return ColorTest.PixelARGBEqual(c1, c2, eqDiff);
        }

        /// <summary>
        /// Compares two ARGB colors according, are they equal against corresponding difference.
        /// </summary>
        /// <param name="c1">ARGB Color 1</param>
        /// <param name="c2">ARGB Color 2</param> 
        /// <returns>nequal test</returns>
        public static bool PixelNotEqual(uint c1, uint c2, double eqDiff)
        {
            return ColorTest.PixelARGBNotEqual(c1, c2, eqDiff);
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