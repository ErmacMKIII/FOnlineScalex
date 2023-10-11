using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Examination
{
    public static class ColorTest
    {
        /// <summary>
        /// Pixel color difference (Lab Method) less equal than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelARGBEqual(uint argb1, uint argb2, double eqDifference)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(argb1);
            LabColor lab2 = LabColor.ConvertARGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) <= 100.0 * eqDifference;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) greater than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelARGBNotEqual(uint argb1, uint argb2, double eqDifference)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(argb1);
            LabColor lab2 = LabColor.ConvertARGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) > 100.0 * eqDifference;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) less equal than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelRGBEqual(Color argb1, Color argb2, double eqDifference)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(argb1);
            LabColor lab2 = LabColor.ConvertARGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) <= 100.0 * eqDifference;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) greater than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelRGBNotEqual(Color argb1, Color argb2, double eqDifference)
        {
            LabColor lab1 = LabColor.ConvertARGBToLab(argb1);
            LabColor lab2 = LabColor.ConvertARGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) > 100.0 * eqDifference;
        }
    }
}
