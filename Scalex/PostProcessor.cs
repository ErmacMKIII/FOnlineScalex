using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Scalex
{
    public static class PostProcessor
    {
        /// <summary>
        /// Post process image to mitigate algorithm
        /// </summary>
        /// <param name="src">original source image</param>
        /// <param name="dst">result bitmap by post-processing</param>
        public static void Process(Bitmap src, out Bitmap dst)
        {
            int w = (int)src.Width;
            int h = (int)src.Height;

            dst = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py;

            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Color srcPixel = src.GetPixel(px, py);                    
                    Color srcGauss = ColorSampler.GaussianBlurSample(src, px, py);                                  
                    if (srcPixel.A == 0 && srcGauss.A >= ColorSampler.B * 255) // edge detected
                    {                        
                        dst.SetPixel(px, py, srcPixel);
                    } 
                    else if (srcPixel.A != 0 && srcGauss.A <= ColorSampler.A * 255) // artifact detected
                    {
                        dst.SetPixel(px, py, Color.Transparent);
                    } 
                    else if (srcPixel.A == 255 && srcGauss.A <= ColorSampler.C * 255) // opaque pixel but adjacent weak alpha
                    {
                        dst.SetPixel(px, py, Color.FromArgb(255, srcPixel.R, srcPixel.G, srcPixel.B));
                    }
                    else // in all other cases copy the pixel
                    {
                        dst.SetPixel(px, py, srcPixel);
                    }
                }
            }
        }
    }
}
