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
       
        public static void Scalex4xMeth(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Frame dst1;
            Scalex2xMeth(src, out dst1, eqDiff, scale);

            Frame dst2;
            Scalex2xMeth(dst1, out dst2, eqDiff, scale);

            dst = dst2;
        }

        public static void Scalex4xMeth(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Bitmap dst1;
            Scalex2xMeth(src, out dst1, eqDiff, scale);

            Bitmap dst2;
            Scalex2xMeth(dst1, out dst2, eqDiff, scale);

            dst = dst2;
        }

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Scalex4xMeth(src, out dst, eqDiff, scale);
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Scalex4xMeth(src, out dst, eqDiff, scale);
        }
    }
}
