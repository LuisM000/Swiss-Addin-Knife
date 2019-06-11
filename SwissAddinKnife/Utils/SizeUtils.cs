using System;
using System.Drawing;
using SwissAddinKnife.Utils;

namespace SwissAddinKnife.Utils
{
    public static class SizeUtils
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
