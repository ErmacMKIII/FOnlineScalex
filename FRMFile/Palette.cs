// Copyright (C) 2021 Alexander Stojanovich
//
// This file is part of FOnlineDatRipper.
//
// FOnlineDatRipper is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//
// FOnlineDatRipper is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with FOnlineDatRipper. If not, see http://www.gnu.org/licenses/.

namespace FOnlineScalex.FRMFile
{
    using global::FOnlineScalex.Properties;
    using System;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Palette" />.
    /// </summary>
    public static class Palette
    {
        /// <summary>
        /// Defines the colors.
        /// </summary>
        private static readonly Color[] colors = new Color[256];

        /// <summary>
        /// Gets the Colors.
        /// </summary>
        public static Color[] Colors { get => colors; }

        /// <summary>
        /// Defines the buffer.
        /// </summary>
        private static byte[] buffer = new byte[768];

        /// <summary>
        /// Initializes the palette with Fallout colors from resource file.
        /// </summary>
        public static void Init()
        {
            buffer.Initialize();            
            using (var stream = new MemoryStream(Resources.Fallout_Palette))
            {
                using (var reader = new BinaryReader(stream))
                {
                    reader.Read(buffer, 0, 768);
                }
            }

            for (uint i = 0; i < buffer.Length / 3; i++)
            {
                colors[i] = Color.FromArgb(buffer[i * 3], buffer[i * 3 + 1], buffer[i * 3 + 2]);
            }
        }

        /// <summary>
        /// RGBDeviation (difference) between two values (colors)
        /// </summary>
        /// <param name="value">palette entry (indexed color)</param>
        /// <param name="otherValue">other pallete (indexed color) entry</param>
        /// <returns>colorDeviation (color difference)</returns>
        public static double RGBDeviation(byte value, byte otherValue)
        {
            Color col = Colors[value];
            Color otherCol = Colors[otherValue];
            double deviation = 0.299 * Math.Abs(otherCol.R - col.R) / 255.0
                        + 0.587 * Math.Abs(otherCol.G - col.G) / 255.0
                        + 0.114 * Math.Abs(otherCol.B - col.B) / 255.0;
            
            return deviation;
        }

        /// <summary>
        /// RGBDeviation (difference) between two values (colors)
        /// </summary>
        /// <param name="col">color</param>
        /// <param name="otherCol">other color</param>
        /// <returns>colorDeviation (color difference)</returns>
        public static double RGBDeviation(Color col, Color otherCol)
        {
            double colorDeviation = 0.299 * Math.Abs(otherCol.R - col.R) / 255.0
                        + 0.587 * Math.Abs(otherCol.G - col.G) / 255.0
                        + 0.114 * Math.Abs(otherCol.B - col.B) / 255.0;
            return colorDeviation;
        }

        /// <summary>
        /// RGBDeviation (difference) between two values (colors)
        /// </summary>
        /// <param name="col">color</param>
        /// <param name="otherCol">other color</param>
        /// <returns>colorDeviation (color difference)</returns>
        public static double RGBADeviation(Color col, Color otherCol)
        {            
            double colorDeviation = 0.299 * Math.Abs(otherCol.R - col.R) / 255.0
                        + 0.587 * Math.Abs(otherCol.G - col.G) / 255.0
                        + 0.114 * Math.Abs(otherCol.B - col.B) / 255.0;

            double alphaDeviation = Math.Abs(otherCol.A - col.A) / 255.0;

            return Math.Sqrt(colorDeviation * colorDeviation + alphaDeviation * alphaDeviation);
        }        

        public static Color ToPaletteColor(Color colTarget)
        {
            double minDeviation = 1.0;
            int minIndex = 0;

            for (int i = 0; i < 256; i++)
            {
                double deviation = RGBDeviation(Palette.Colors[i], colTarget);
                if (deviation < minDeviation)
                {
                    minDeviation = deviation;
                    minIndex = i;
                    if (deviation == 0.0)
                    {
                        break;
                    }
                }
            }

            return Colors[(byte)minIndex];
        }

        public static byte ToPaletteIndex(Color colTarget)
        {
            double minDeviation = 1.0;
            int minIndex = -1;

            for (int i = 0; i < 256; i++)
            {
                double deviation = RGBDeviation(Palette.Colors[i], colTarget);
                if (deviation < minDeviation)
                {
                    minDeviation = deviation;
                    minIndex = i;
                    if (deviation == 0.0)
                    {
                        break;
                    }
                }
            }

            return (byte)minIndex;
        }

        /// <summary>
        /// Add two colors into result color
        /// </summary>
        /// <param name="col"></param>
        /// <param name="otherCol"></param>
        /// <returns></returns>
        public static Color ColorAdditionColor(Color col, Color otherCol)
        {
            int red = col.R + otherCol.R;
            int green = col.G + otherCol.G;
            int blue = col.B + otherCol.B;

            Color colTarget = Color.FromArgb(Math.Clamp(red, 0, 255), Math.Clamp(green, 0, 255), Math.Clamp(blue, 0, 255));

            Color result = ToPaletteColor(colTarget);

            return result; 
        }

        /// <summary>
        /// Add two colors into result color
        /// </summary>
        /// <param name="col"></param>
        /// <param name="otherCol"></param>
        /// <returns></returns>
        public static byte ColorAdditionIndex(Color col, Color otherCol)
        {
            int red = col.R + otherCol.R;
            int green = col.G + otherCol.G;
            int blue = col.B + otherCol.B;

            Color colTarget = Color.FromArgb(Math.Clamp(red, 0, 255), Math.Clamp(green, 0, 255), Math.Clamp(blue, 0, 255));

            byte index = ToPaletteIndex(colTarget);

            return index;
        }

        /// <summary>
        /// Multiply two colors into result color
        /// </summary>
        /// <param name="col"></param>
        /// <param name="otherCol"></param>
        /// <returns></returns>
        public static byte ColorMultiplicationIndex(Color col, Color otherCol)
        {
            int red = (int)Math.Round(col.R * otherCol.R / 255.0);
            int green = (int)Math.Round(col.G * otherCol.G / 255.0);
            int blue = (int)Math.Round(col.B * otherCol.B / 255.0);

            Color colTarget = Color.FromArgb(Math.Clamp(red, 0, 255), Math.Clamp(green, 0, 255), Math.Clamp(blue, 0, 255));

            byte index = ToPaletteIndex(colTarget);

            return index;
        }

        /// <summary>
        /// Multiply two colors into result color
        /// </summary>
        /// <param name="col"></param>
        /// <param name="otherCol"></param>
        /// <returns></returns>
        public static Color ColorMultiplicationColor(Color col, Color otherCol)
        {
            int red = (int)Math.Round(col.R * otherCol.R / 255.0);
            int green = (int)Math.Round(col.G * otherCol.G / 255.0);
            int blue = (int)Math.Round(col.B * otherCol.B / 255.0);

            Color colTarget = Color.FromArgb(Math.Clamp(red, 0, 255), Math.Clamp(green, 0, 255), Math.Clamp(blue, 0, 255));

            Color result = ToPaletteColor(colTarget);

            return result;
        }

        /// <summary>
        /// Cast color to gray according to palette index
        /// </summary>
        /// <param name="value">palette index</param>
        /// <returns>best matching gray</returns>
        public static Color ColorToGrayColor(Color col)
        {            
            double gray = 0.299 * col.R / 255.0
                        + 0.587 * col.G / 255.0
                        + 0.114 * col.B / 255.0;
            Color grayCol = Color.FromArgb((int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray));

            Color result = ToPaletteColor(grayCol);

            return result;
        }

        /// <summary>
        /// Cast color to gray according to palette index
        /// </summary>
        /// <param name="value">palette index</param>
        /// <returns>best matching gray</returns>
        public static Color ColorToGrayColor(byte value)
        {
            Color col = Colors[value];
            double gray = 0.299 * col.R / 255.0
                        + 0.587 * col.G / 255.0
                        + 0.114 * col.B / 255.0;
            Color grayCol = Color.FromArgb((int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray));

            Color result = ToPaletteColor(grayCol);

            return result;
        }

        /// <summary>
        /// Cast color to gray according to palette index
        /// </summary>
        /// <param name="value">palette index</param>
        /// <returns>best matching gray</returns>
        public static byte ColorToGrayIndex(byte value)
        {
            Color col = Colors[value];
            double gray = 0.299 * col.R / 255.0
                        + 0.587 * col.G / 255.0
                        + 0.114 * col.B / 255.0;
            Color grayCol = Color.FromArgb((int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray));

            int index = ToPaletteIndex(grayCol);

            return (byte)index;
        }

        /// <summary>
        /// Cast color to gray according to palette index
        /// </summary>
        /// <param name="col">color</param>
        /// <returns>best matching gray</returns>
        public static byte ColorToGrayIndex(Color col)
        {
            double gray = 0.299 * col.R / 255.0
                        + 0.587 * col.G / 255.0
                        + 0.114 * col.B / 255.0;
            Color grayCol = Color.FromArgb((int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray), (int)Math.Round(255.0 * gray));

            double minDeviation = 1.0;
            int minIndex = -1;

            for (int i = 0; i < 256; i++)
            {
                double deviation = RGBDeviation(Palette.Colors[i], grayCol);
                if (deviation < minDeviation)
                {
                    minDeviation = deviation;
                    minIndex = i;
                    if (deviation == 0.0)
                    {
                        break;
                    }
                }
            }

            return (byte)minIndex;
        }

    }
}
