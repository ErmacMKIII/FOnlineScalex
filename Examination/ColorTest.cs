using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Examination
{
    /// <summary>
    /// Provides color comparison methods using perceptually uniform CIELAB color space.
    /// </summary>
    public static class ColorTest
    {
        /// <summary>
        /// Pixel color difference (Lab Method) less or equal than allowed.
        /// Uses CIELAB color space for perceptually accurate color comparison.
        /// </summary>
        /// <param name="argb1">ARGB color 1 (System.Drawing.Color format as int)</param>
        /// <param name="argb2">ARGB color 2 (System.Drawing.Color format as int)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0], scaled to Delta E [0-100]</param>
        /// <returns>True if colors are perceptually equal within tolerance</returns>
        public static bool PixelARGBEqual(uint argb1, uint argb2)
        {
            // Convert signed int to unsigned int for LabColor conversion
            LabColor lab1 = LabColor.ConvertARGBToLab(unchecked(argb1));
            LabColor lab2 = LabColor.ConvertARGBToLab(unchecked(argb2));

            return LabColor.CalculateDeltaE(lab1, lab2) == 100.0;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) greater than allowed.
        /// Uses CIELAB color space for perceptually accurate color comparison.
        /// </summary>
        /// <param name="argb1">ARGB color 1 (System.Drawing.Color format as int)</param>
        /// <param name="argb2">ARGB color 2 (System.Drawing.Color format as int)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0], scaled to Delta E [0-100]</param>
        /// <returns>True if colors are perceptually different beyond tolerance</returns>
        public static bool PixelARGBNotEqual(uint argb1, uint argb2)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(unchecked(argb1));
            LabColor lab2 = LabColor.ConvertARGBToLab(unchecked(argb2));

            return LabColor.CalculateDeltaE(lab1, lab2) != 100.0;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) less or equal than allowed.
        /// Uses CIELAB color space for perceptually accurate color comparison.
        /// </summary>
        /// <param name="color1">Color 1 (System.Drawing.Color)</param>
        /// <param name="color2">Color 2 (System.Drawing.Color)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0], scaled to Delta E [0-100]</param>
        /// <returns>True if colors are perceptually equal within tolerance</returns>
        public static bool PixelRGBEqual(Color color1, Color color2)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(color1);
            LabColor lab2 = LabColor.ConvertARGBToLab(color2);

            return LabColor.CalculateDeltaE(lab1, lab2) == 100.0;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) greater than allowed.
        /// Uses CIELAB color space for perceptually accurate color comparison.
        /// </summary>
        /// <param name="color1">Color 1 (System.Drawing.Color)</param>
        /// <param name="color2">Color 2 (System.Drawing.Color)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0], scaled to Delta E [0-100]</param>
        /// <returns>True if colors are perceptually different beyond tolerance</returns>
        public static bool PixelRGBNotEqual(Color color1, Color color2)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(color1);
            LabColor lab2 = LabColor.ConvertARGBToLab(color2);

            return LabColor.CalculateDeltaE(lab1, lab2) != 100.0;
        }

        /// <summary>
        /// Compares two palette indices for color equality using CIELAB color space.
        /// </summary>
        /// <param name="paletteIndex1">First palette index (0-255)</param>
        /// <param name="paletteIndex2">Second palette index (0-255)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0]</param>
        /// <returns>True if palette colors are perceptually equal within tolerance</returns>
        public static bool PixelPaletteEqual(byte paletteIndex1, byte paletteIndex2)
        {
            Color color1 = FRMFile.Palette.Colors[paletteIndex1];
            Color color2 = FRMFile.Palette.Colors[paletteIndex2];

            return PixelRGBEqual(color1, color2);
        }

        /// <summary>
        /// Compares two palette indices for color inequality using CIELAB color space.
        /// </summary>
        /// <param name="paletteIndex1">First palette index (0-255)</param>
        /// <param name="paletteIndex2">Second palette index (0-255)</param>
        /// <param name="eqDifference">Equality difference threshold [0.0-1.0]</param>
        /// <returns>True if palette colors are perceptually different beyond tolerance</returns>
        public static bool PixelPaletteNotEqual(byte paletteIndex1, byte paletteIndex2)
        {
            Color color1 = FRMFile.Palette.Colors[paletteIndex1];
            Color color2 = FRMFile.Palette.Colors[paletteIndex2];

            return PixelRGBNotEqual(color1, color2);
        }
    }
}