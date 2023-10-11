using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Examination
{
    public class LabColor : IEquatable<LabColor?>
    {
        public double L { get; set; }
        public double a { get; set; }
        public double b { get; set; }



        public LabColor()
        {

        }

        public LabColor(double L, double a, double b)
        {
            this.L = L;
            this.a = a;
            this.b = b;
        }

        public static LabColor ConvertARGBToLab(Color col)
        {
            // Convert RGB to XYZ
            double a = col.A / 255.0; // premultiply with alpha
            double r = a * col.R / 255.0;
            double g = a * col.G / 255.0;
            double b = a * col.B / 255.0;

            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            r *= 100.0;
            g *= 100.0;
            b *= 100.0;

            double X = r * 0.4124564 + g * 0.3575761 + b * 0.1804375;
            double Y = r * 0.2126729 + g * 0.7151522 + b * 0.0721750;
            double Z = r * 0.0193339 + g * 0.1191920 + b * 0.9503041;

            // Convert XYZ to Lab
            X /= 95.047;
            Y /= 100.000;
            Z /= 108.883;

            X = (X > 0.008856) ? Math.Pow(X, 1.0 / 3.0) : (903.3 * X + 16.0) / 116.0;
            Y = (Y > 0.008856) ? Math.Pow(Y, 1.0 / 3.0) : (903.3 * Y + 16.0) / 116.0;
            Z = (Z > 0.008856) ? Math.Pow(Z, 1.0 / 3.0) : (903.3 * Z + 16.0) / 116.0;

            return new LabColor
            {
                L = Math.Max(0.0, Math.Min(100.0, 116.0 * Y - 16.0)),
                a = Math.Max(-128.0, Math.Min(127.0, (X - Y) * 500.0)),
                b = Math.Max(-128.0, Math.Min(127.0, (Y - Z) * 200.0))
            };
        }

        public static LabColor ConvertARGBToLab(uint col)
        {
            // Convert RGB to XYZ
            double a = (col & 0xFF000000) / 255.0; // premultiply with alpha
            double r = a * (col & 0x00FF0000) / 255.0;
            double g = a * (col & 0x0000FF00) / 255.0;
            double b = a * (col & 0x000000FF) / 255.0;

            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            r *= 100.0;
            g *= 100.0;
            b *= 100.0;

            double X = r * 0.4124564 + g * 0.3575761 + b * 0.1804375;
            double Y = r * 0.2126729 + g * 0.7151522 + b * 0.0721750;
            double Z = r * 0.0193339 + g * 0.1191920 + b * 0.9503041;

            // Convert XYZ to Lab
            X /= 95.047;
            Y /= 100.000;
            Z /= 108.883;

            X = (X > 0.008856) ? Math.Pow(X, 1.0 / 3.0) : (903.3 * X + 16.0) / 116.0;
            Y = (Y > 0.008856) ? Math.Pow(Y, 1.0 / 3.0) : (903.3 * Y + 16.0) / 116.0;
            Z = (Z > 0.008856) ? Math.Pow(Z, 1.0 / 3.0) : (903.3 * Z + 16.0) / 116.0;

            return new LabColor
            {
                L = Math.Max(0.0, Math.Min(100.0, 116.0 * Y - 16.0)),
                a = Math.Max(-128.0, Math.Min(127.0, (X - Y) * 500.0)),
                b = Math.Max(-128.0, Math.Min(127.0, (Y - Z) * 200.0))
            };
        }

        public double CalculateDeltaE(LabColor other)
        {
            double deltaL = other.L - this.L;
            double deltaA = other.a - this.a;
            double deltaB = other.b - this.b;

            return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
        }

        public static double CalculateDeltaE(LabColor first, LabColor second)
        {
            double deltaL = first.L - second.L;
            double deltaA = first.a - second.a;
            double deltaB = first.b - second.b;

            return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as LabColor);
        }

        public bool Equals(LabColor? other)
        {
            return other is not null &&
                   L == other.L &&
                   a == other.a &&
                   b == other.b;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(L, a, b);
        }

        public static bool operator ==(LabColor? left, LabColor? right)
        {
            return EqualityComparer<LabColor>.Default.Equals(left, right);
        }

        public static bool operator !=(LabColor? left, LabColor? right)
        {
            return !(left == right);
        }
    }
}
