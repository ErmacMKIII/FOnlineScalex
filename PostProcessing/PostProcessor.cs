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

namespace FOnlineScalex.PostProcessing
{
    public struct AlphaRange
    {
        public int DropThreshold { get; set; }
        public int MultiplyThreshold {  get; set; } 
    }

    public static class PostProcessor
    {
        // --------------------------------
        // Algorithm provided from:
        // https://www.scale2x.it/algorithm
        // --------------------------------

        /// <summary>
        /// Converts sRGB color component to linear RGB.
        /// Applies inverse gamma correction (gamma expansion).
        /// </summary>
        /// <param name="value">sRGB component value in [0, 1] range.</param>
        /// <returns>Linear RGB value in [0, 1] range.</returns>
        private static double SrgbToLinear(double value)
        {
            return (value > 0.04045) ? Math.Pow((value + 0.055) / 1.055, 2.4) : value / 12.92;
        }

        /// <summary>
        /// Converts linear RGB color component to sRGB.
        /// Applies gamma correction (gamma compression).
        /// </summary>
        /// <param name="value">Linear RGB component value in [0, 1] range.</param>
        /// <returns>sRGB value in [0, 1] range.</returns>
        private static double LinearToSrgb(double value)
        {
            return (value > 0.0031308) ? 1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055 : 12.92 * value;
        }

        /// <summary>
        /// Applies bidirectional brightness adjustment to a color.
        /// Uses a perceptual curve to brighten or darken while preserving color ratios.
        /// </summary>
        /// <param name="col">The input color to be adjusted.</param>
        /// <param name="adjustment">
        /// Brightness adjustment value in range [-1.0 to +1.0]:
        /// <list type="bullet">
        /// <item><description>-1.0 = Maximum darkening (black)</description></item>
        /// <item><description>-0.5 = Moderate darkening (50% darker)</description></item>
        /// <item><description> 0.0 = No change (original color)</description></item>
        /// <item><description>+0.5 = Moderate brightening (50% brighter)</description></item>
        /// <item><description>+1.0 = Maximum brightening (white point preserved)</description></item>
        /// </list>
        /// Values outside [-1, 1] are clamped to this range.
        /// </param>
        /// <returns>
        /// An adjusted Color with modified brightness.
        /// Alpha channel is preserved unchanged.
        /// </returns>
        /// <remarks>
        /// <para><b>Algorithm Explanation:</b></para>
        /// <para>
        /// This method uses a perceptually uniform brightness adjustment that works differently
        /// for brightening (positive values) and darkening (negative values):
        /// </para>
        /// <para><b>For Brightening (0 to +1):</b></para>
        /// <list type="number">
        /// <item>Converts sRGB to linear RGB (removes gamma encoding)</item>
        /// <item>Applies power curve: value^(1 - adjustment)</item>
        /// <item>When adjustment = 0: value^1 = value (no change)</item>
        /// <item>When adjustment = 0.5: value^0.5 = √value (moderate brightening)</item>
        /// <item>When adjustment = 1: value^0 = 1 (maximum brightening, but see step 5)</item>
        /// <item>Converts back to sRGB (adds gamma encoding)</item>
        /// <item>Blends with original based on luminance to preserve white point</item>
        /// </list>
        /// <para><b>For Darkening (-1 to 0):</b></para>
        /// <list type="number">
        /// <item>Uses linear interpolation toward black</item>
        /// <item>Formula: value × (1 + adjustment)</item>
        /// <item>When adjustment = 0: value × 1 = value (no change)</item>
        /// <item>When adjustment = -0.5: value × 0.5 (50% darker)</item>
        /// <item>When adjustment = -1: value × 0 = 0 (black)</item>
        /// </list>
        /// <para><b>White Point Preservation:</b></para>
        /// <para>
        /// For brightening, the algorithm preserves near-white colors by blending based on luminance:
        /// <list type="bullet">
        /// <item>Dark colors: Full brightening effect applied</item>
        /// <item>Mid-tone colors: Partial brightening (smooth transition)</item>
        /// <item>Bright colors: Minimal brightening to prevent over-saturation</item>
        /// <item>Pure white (255,255,255): Remains unchanged</item>
        /// </list>
        /// This prevents the "washed out" look that occurs with simple linear brightening.
        /// </para>
        /// <para><b>Gamma-Correct Processing:</b></para>
        /// <para>
        /// The brightening operation works in linear RGB space for physically accurate results.
        /// This ensures that brightness adjustments match real-world perception and don't
        /// introduce color shifts or gamma artifacts.
        /// </para>
        /// <para><b>Use Cases:</b></para>
        /// <list type="bullet">
        /// <item>Correcting under/over-exposed game sprites</item>
        /// <item>Adjusting brightness while preserving color relationships</item>
        /// <item>Creating darker/lighter variants of images</item>
        /// <item>Real-time brightness adjustment in image processing pipelines</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Brighten image (50% brighter)
        /// Color brightened = FixColor(originalColor, 0.5);
        /// 
        /// // Darken image (50% darker)
        /// Color darkened = FixColor(originalColor, -0.5);
        /// 
        /// // Maximum brightening
        /// Color maxBright = FixColor(originalColor, 1.0);
        /// 
        /// // Maximum darkening (toward black)
        /// Color maxDark = FixColor(originalColor, -1.0);
        /// 
        /// // No change (bypass)
        /// Color unchanged = FixColor(originalColor, 0.0);
        /// </code>
        /// </example>
        public static Color FixColor(Color col, double adjustment = 0.0)
        {
            // Clamp adjustment to valid range [-1.0, +1.0]
            adjustment = Math.Clamp(adjustment, -1.0, 1.0);

            // Early exit: if adjustment is 0, return original color unchanged
            if (Math.Abs(adjustment) < 0.001)
                return col;

            double r, g, b;

            if (adjustment > 0)
            {
                // BRIGHTENING: Use gamma-correct power curve in linear space
                // This provides perceptually uniform brightening while preserving white point

                // Step 1: Convert sRGB to linear RGB (remove gamma ~2.2)
                // sRGB uses piecewise function: linear below 0.04045, power curve above
                double rLinear = SrgbToLinear(col.R / 255.0);
                double gLinear = SrgbToLinear(col.G / 255.0);
                double bLinear = SrgbToLinear(col.B / 255.0);

                // Step 2: Calculate luminance for white point preservation
                // Uses Rec. 709 coefficients weighted for human perception
                double luma = 0.2126 * rLinear + 0.7152 * gLinear + 0.0722 * bLinear;

                // Step 3: Apply power curve: value^(1 - adjustment)
                // adjustment = 0.0 → exponent = 1.0 → no change
                // adjustment = 0.5 → exponent = 0.5 → square root (moderate brightening)
                // adjustment = 1.0 → exponent = 0.0 → maximum brightening
                double exponent = 1.0 - adjustment;
                double rBright = Math.Pow(rLinear, exponent);
                double gBright = Math.Pow(gLinear, exponent);
                double bBright = Math.Pow(bLinear, exponent);

                // Step 4: Calculate blend factor based on luminance
                // Darker colors (luma near 0) → blend near 0 → more brightening
                // Brighter colors (luma near 1) → blend near 1 → less brightening
                // This preserves white point and prevents over-saturation
                double blend = Math.Sqrt(luma); // Square root for smooth transition

                // Step 5: Blend original and brightened values
                // Bright areas get less correction, dark areas get more
                rLinear = rLinear * blend + rBright * (1.0 - blend);
                gLinear = gLinear * blend + gBright * (1.0 - blend);
                bLinear = bLinear * blend + bBright * (1.0 - blend);

                // Step 6: Clamp to valid linear RGB range [0, 1]
                rLinear = Math.Clamp(rLinear, 0.0, 1.0);
                gLinear = Math.Clamp(gLinear, 0.0, 1.0);
                bLinear = Math.Clamp(bLinear, 0.0, 1.0);

                // Step 7: Convert back to sRGB (add gamma encoding ~2.2)
                r = LinearToSrgb(rLinear) * 255.0;
                g = LinearToSrgb(gLinear) * 255.0;
                b = LinearToSrgb(bLinear) * 255.0;
            }
            else
            {
                // DARKENING: Use simple linear scaling toward black
                // This is perceptually correct for darkening and computationally efficient

                // Step 1: Calculate scale factor
                // adjustment = -0.0 → scale = 1.0 → no change
                // adjustment = -0.5 → scale = 0.5 → 50% darker
                // adjustment = -1.0 → scale = 0.0 → black
                double scale = 1.0 + adjustment; // adjustment is negative, so this reduces value

                // Step 2: Apply linear scaling to each channel
                // Simple multiplication preserves color ratios perfectly
                r = col.R * scale;
                g = col.G * scale;
                b = col.B * scale;
            }

            // Final step: Clamp to valid byte range [0, 255] and construct color
            // Alpha channel is preserved unchanged
            return Color.FromArgb(
                col.A,
                (int)Math.Clamp(r, 0, 255),
                (int)Math.Clamp(g, 0, 255),
                (int)Math.Clamp(b, 0, 255)
            );
        }

        /// <summary>
        /// Post process image to mitigate algorithm
        /// </summary>
        /// <param name="src">original source image</param>
        /// <param name="dst">result bitmap by post-processing</param>
        /// <param name="correctionValue">brightness correction value (-1.0 to +1.0)</param>
        public static void Process(Bitmap src, out Bitmap dst, AlphaRange alphaRange, double correctionValue)
        {
            int w = src.Width;
            int h = src.Height;

            dst = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int px, py;

            // remove artifacts
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Color srcPixel = src.GetPixel(px, py);
                    dst.SetPixel(px, py, srcPixel);

                    Color srcGauss = ColorSampler.GaussianBlurSample(src, px, py);

                    if (srcPixel.A != 0 && srcGauss.A < alphaRange.DropThreshold) // artifact detected
                    {
                        dst.SetPixel(px, py, Color.Transparent);
                    }

                    if (srcPixel.A == 0 && srcGauss.A != 0 && srcGauss.A >= alphaRange.DropThreshold) // edge detected
                    {
                        float alphaf = srcGauss.A / 255.0f;
                        int red = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.R), 255);
                        int green = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.G), 255);
                        int blue = Math.Min((int)Math.Round(alphaf * 2.0 * srcGauss.B), 255);
                        dst.SetPixel(px, py, Color.FromArgb(alphaRange.DropThreshold, red, green, blue));
                    }

                    if (srcPixel.A != 0 && srcGauss.A != 0 && srcGauss.A <= alphaRange.MultiplyThreshold) // opaque pixel but adjacent weak alpha
                    {
                        float alphaf = srcPixel.A / 255.0f;
                        int red = Math.Min((int)Math.Round(alphaf * (srcGauss.R + srcPixel.R)), 255);
                        int green = Math.Min((int)Math.Round(alphaf * (srcGauss.G + srcPixel.G)), 255);
                        int blue = Math.Min((int)Math.Round(alphaf * (srcGauss.B + srcPixel.B)), 255);
                        dst.SetPixel(px, py, Color.FromArgb(255, red, green, blue));
                    }
                }
            }

            // Fix colors
            // Remove blue color (always)
            for (px = 0; px < w; px++)
            {
                for (py = 0; py < h; py++)
                {
                    Color srcPixel = src.GetPixel(px, py);
                    if (srcPixel.Equals(Color.Blue))
                    {
                        dst.SetPixel(px, py, Color.Transparent);
                    } 
                    else
                    {
                        // Apply brightness/contrast correction
                        dst.SetPixel(px, py, FixColor(srcPixel, correctionValue));
                    }
                }
            }
        }
    }
}
