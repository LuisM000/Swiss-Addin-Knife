using System;
using System.Drawing;

namespace SwissAddinKnife.Extensions
{
    public static class SizeExtensions
    {
        public static Size Multiply(this Size size, double factor)
        {
            return new Size((int)(size.Width * factor), (int)(size.Height * factor));
        }

        public static bool SizeEquals(this Size size, Size other, int epsilon)
        {
            return Math.Abs(size.Width - other.Width) <= epsilon &&
                Math.Abs(size.Height - other.Height) <= epsilon;
        }
    }
}
