using System;
using System.IO;

namespace GraduateAssessment
{
    public class Logger
    {
        public static void Log(string text)
        {
            File.AppendAllText("logs.txt", text + Environment.NewLine);
        }
        public static void ClearTextFile()
        {
            File.WriteAllText("logs.txt", string.Empty);
        }
    }
}
