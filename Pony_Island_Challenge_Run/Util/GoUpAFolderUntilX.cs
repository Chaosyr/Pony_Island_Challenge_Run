using System;
using System.IO;

namespace PonyIslandChallengeRuns.Util
{
    public class GoUpAFolderUntilX
    {
        /// <summary>
        /// Gets the path backward from the inputted path to the inputted X value
        /// </summary>
        /// <param name="X">What directory to look for.</param>
        /// <param name="path">The path you want to trace from.</param>
        /// <returns>The final path after getting back to where the current directory is X.</returns>
        /// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
        public static string Approach(string X, string path)
        {
            try
            {
                while (Path.GetFileName(Path.GetFullPath(path)) != X)
                {
                    Console.WriteLine($"(Approach-Info): Currently attempting path; {Path.GetFullPath(path)}");
                    path += "\\..";
                }
                Console.WriteLine($"(Approach-Info): Found path where end equals {X}; {Path.GetFullPath(path)}");
                return path;
            }
            catch
            {
                Console.WriteLine($"(Approach-Error): Could not find path where end equals {X} within; {Path.GetFullPath(path)}");
                return path;
            }
        }
    }
}