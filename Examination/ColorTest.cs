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
        /// Pixel color difference (Lab Method)
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <returns>difference value</returns>
        public static double PixelDeviation(uint argb1, uint argb2)
        {
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2);
        }

        /// <summary>
        /// Pixel color difference (Lab Method) less equal than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelRGBEqual(uint argb1, uint argb2, double eqDifference)
        {
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) <= eqDifference;
        }

        /// <summary>
        /// Pixel color difference (Lab Method) greater than allowed
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <param name="eqDifference"></param>
        /// <returns></returns>
        public static bool PixelRGBNotEqual(uint argb1, uint argb2, double eqDifference)
        {
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) > eqDifference;
        }

        /// <summary>
        /// Pixel color difference (Lab Method)
        /// </summary>
        /// <param name="argb1">argb color 1</param>
        /// <param name="argb2">argb color 2</param>
        /// <returns>difference value</returns>
        public static double PixelDeviation(Color argb1, Color argb2)
        {
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2);
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
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) <= eqDifference;
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
            LabColor lab1 = new LabColor();
            lab1.ConvertRGBToLab(argb1);
            LabColor lab2 = new LabColor();
            lab2.ConvertRGBToLab(argb2);

            return LabColor.CalculateDeltaE(lab1, lab2) > eqDifference;
        }
    }
}
