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
    public class Scalex4x : Scalex2x
    {
        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------
       
        public static void Scalex4xMeth(Frame src, out Frame dst, double eqDiff)
        {
            Frame dst1;
            Scalex2xMeth(src, out dst1, eqDiff);

            Frame dst2;
            Scalex2xMeth(dst1, out dst2, eqDiff);

            dst = dst2;
        }

        public static void Scalex4xMeth(Bitmap src, out Bitmap dst, double eqDiff)
        {
            Bitmap dst1;
            Scalex2xMeth(src, out dst1, eqDiff);

            Bitmap dst2;
            Scalex2xMeth(dst1, out dst2, eqDiff);

            dst = dst2;
        }

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex4xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Frame(new Bitmap(dst.ToBitmap(), (int)src.Width, (int)src.Height), src.OffsetX, src.OffsetY);
            }
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex4xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Bitmap(dst, (int)src.Width, (int)src.Height);
            }
        }
    }
}
