using System;
using System.IO;

namespace Sterowanie
{
    public class CAN
    {
        public static void DoObiektu(ushort text)
        {
            try
            {
                File.WriteAllText("C:\\Users\\lidka\\source\\repos\\DoObiektu.txt", text.ToString());
            }
            catch (IOException)
            {
                return;
            }
        }
        public static string DoSterowania()
        {
            try
            {
                return File.ReadAllText("C:\\Users\\lidka\\source\\repos\\DoSterowania.txt");
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }
    }
}
