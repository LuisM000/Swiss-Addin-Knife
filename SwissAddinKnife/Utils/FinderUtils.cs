using System;
using System.Diagnostics;

namespace SwissAddinKnife.Utils
{
    public static class FinderUtils
    {
        public static bool Reveal(string path)
        {
            try
            {
                var process = Process.Start(path);
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}