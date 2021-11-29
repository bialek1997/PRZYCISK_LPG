using System;
using System.IO;

namespace Obiekt
{
    public class CAN
    {
        public static double DoObiektu()
        {
            try
            {
                return Double.Parse(File.ReadAllText("C:\\Users\\lidka\\source\\repos\\DoObiektu.txt"));
            }
            catch(IOException)
            {
                return -1;
            }
            
        }

        public static void DoSterowania(double value)
        {
            try
            {
                File.WriteAllText("C:\\Users\\lidka\\source\\repos\\DoSterowania.txt", value.ToString());
            }
            catch (IOException)
            {
                return;
            }

        }
    }
}
