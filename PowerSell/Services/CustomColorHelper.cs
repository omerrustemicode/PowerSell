using System;
using System.Windows.Media;

namespace PowerSell.Services
{
    public class CustomColorHelper
    {
        public static LinearGradientBrush CreateRedGreenGradientBrush()
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(0, 0);
            brush.EndPoint = new System.Windows.Point(1, 1);

            GradientStop redStop = new GradientStop(Colors.Red, 0.5);
            GradientStop greenStop = new GradientStop(Colors.DarkBlue, 0.5);

            brush.GradientStops.Add(redStop);
            brush.GradientStops.Add(greenStop);

            return brush;
        }
        public static LinearGradientBrush CreateYellowGreenGradientBrush()
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(0, 0);
            brush.EndPoint = new System.Windows.Point(1, 1);

            GradientStop redStop = new GradientStop(Colors.DarkBlue, 0.5);
            GradientStop greenStop = new GradientStop(Colors.Green, 0.5);

            brush.GradientStops.Add(redStop);
            brush.GradientStops.Add(greenStop);

            return brush;
        }
    }
}
