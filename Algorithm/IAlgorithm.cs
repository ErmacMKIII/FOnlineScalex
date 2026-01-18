/* Copyright (C) 2026 Aleksandar Stojanovic <coas91@rocketmail.com>

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm
{
    public interface IAlgorithm
    {
        public enum AlgorithmId { Scalex2x, Scalex3x, Scalex4x, Hqx2x, Hqx3x, Hqx4x }

        public void Process(
                Frame src, out Frame dst, bool scale = true
        );

        public void Process(
                Bitmap src, out Bitmap dst, bool scale = true
        );

        /// <summary>
        /// Apply some fixes to color to improve contrast/brightness and white point
        /// based on luma value
        /// </summary>
        protected static Color FixColor(Color col)
        {
            double luma = 0.299 * col.R + 0.587 * col.G + 0.114 * col.B;
            double blend = Math.Clamp(luma / 255.0, 0.0, 1.0);
            double r = col.R * (1 - blend) + Math.Sqrt(col.R * 255.0) * blend;
            double g = col.G * (1 - blend) + Math.Sqrt(col.G * 255.0) * blend;
            double b = col.B * (1 - blend) + Math.Sqrt(col.B * 255.0) * blend;

            return Color.FromArgb(
                col.A,
                (int)Math.Clamp(r, 0, 255),
                (int)Math.Clamp(g, 0, 255),
                (int)Math.Clamp(b, 0, 255)
            );
        }
    }
}
