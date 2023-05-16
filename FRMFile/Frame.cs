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
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Defines the <see cref="Frame" />.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Defines the width.
        /// </summary>
        private readonly uint width;

        /// <summary>
        /// Defines the height.
        /// </summary>
        private readonly uint height;

        /// <summary>
        /// Defines the offsetX.
        /// </summary>
        private readonly int offsetX;

        /// <summary>
        /// Defines the offsetY.
        /// </summary>
        private readonly int offsetY;

        /// <summary>
        /// Defines the data.
        /// </summary>
        private readonly byte[] data;

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public uint Width => width;

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public uint Height => height;

        /// <summary>
        /// Gets the OffsetX.
        /// </summary>
        public int OffsetX => offsetX;

        /// <summary>
        /// Gets the OffsetY.
        /// </summary>
        public int OffsetY => offsetY;

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public byte[] Data => data;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="width">The width<see cref="uint"/>.</param>
        /// <param name="height">The height<see cref="uint"/>.</param>
        /// <param name="offsetX">The offsetX<see cref="int"/>.</param>
        /// <param name="offsetY">The offsetY<see cref="int"/>.</param>
        public Frame(uint width, uint height, int offsetX, int offsetY)
        {
            this.width = width;
            this.height = height;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.data = new byte[width * height];
        }

        /// <summary>
        /// Creates new image data from image (converts image to indexed then writes to the data)
        /// </summary>
        /// <param name="image">original image to get the data from</param>
        /// <param name="offsetX">offsetX frame offset in X direction</param>
        /// <param name="offsetY">offsetY frame offset in Y direction</param>
        public Frame(Bitmap image, int offsetX, int offsetY)
        {
            this.width = (uint)image.Width;
            this.height = (uint)image.Height;
            this.data = new byte[width * height];
            for (int px = 0; px < width; px++)
            {
                for (int py = 0; py < height; py++)
                {
                    int e = (int)(width * py + px);
                    byte index = 0;
                    Color col = image.GetPixel(px, py);
                    if (col.A != 0)
                    {
                        double minDeviation = 1.0;
                        int minIndex = -1;
                        foreach (Color coli in Palette.Colors)
                        {
                            double deviation = Palette.RGBDeviation(col, coli);
                            if (deviation < minDeviation)
                            {
                                minDeviation = deviation;
                                minIndex = index;
                                if (deviation == 0.0)
                                {
                                    break;
                                }
                            }
                            index++;
                        }

                        data[e] = (byte)minIndex;
                    }
                }
            }
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        /// <summary>
        /// Gets pixel color (palette entry).
        /// </summary>
        /// <param name="px">x coord.</param>
        /// <param name="py">y coord.</param>
        /// <returns>.</returns>
        public byte GetPixel(uint px, uint py)
        {
            uint e = width * py + px;
            return data[e];
        }

        /// <summary>
        /// Gets pixel color (palette entry).
        /// Safety to not get outside the bounds.
        /// When outside the bound transparent color (blue) is returned
        /// </summary>
        /// <param name="px">x coord.</param>
        /// <param name="py">y coord.</param>
        /// <returns>Palette entry</returns>
        public byte GetPixelSafe(uint px, uint py)
        {
            if (px >= 0 && px < width && py >= 0 && py < height)
            {
                uint e = width * py + px;
                return data[e];
            }
            return 0;
        }

        /// <summary>
        /// Sets pixel color to one from the palette (to the entry).
        /// </summary>
        /// <param name="px">x coord.</param>
        /// <param name="py">y coord.</param>
        /// <param name="val">palette index to set.</param>
        public void SetPixel(uint px, uint py, byte val)
        {
            uint e = width * py + px;
            data[e] = val;
        }

        /// <summary>
        /// Sets pixel color to one from the palette (to the entry).
        /// When outside the bound do nothing.
        /// </summary>
        /// <param name="px">x coord.</param>
        /// <param name="py">y coord.</param>
        /// <param name="val">palette index to set.</param>
        public void SetPixelSafe(uint px, uint py, byte val)
        {
            if (px >= 0 && px < width && py >= 0 && py < height && val >= 0 && val <= 255)
            {
                uint e = width * py + px;
                data[e] = val;
            }
        }

        /// <summary>
        /// Converts this frame to bitmap, used later to drawing on controls.
        /// </summary>
        /// <returns>.</returns>
        public Bitmap ToBitmap()
        {
            Bitmap result = new Bitmap((int)width, (int)height, PixelFormat.Format32bppArgb);
            for (uint px = 0; px < width; px++)
            {
                for (uint py = 0; py < height; py++)
                {
                    uint e = width * py + px;
                    result.SetPixel((int)px, (int)py, Palette.Colors[data[e]]);
                }
            }
            // make the blue transparent
            result.MakeTransparent(Palette.Colors[0]);
            return result;
        }
    }
}
