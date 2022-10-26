using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using NetworkService.AdditionalElements;
using NetworkService.Model;
//using System.Windows.Media;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Messaging;

namespace NetworkService.ViewModel
{
    public class MeasurmentGraphViewModel : BindableBase
    {


        public static Graph ElementHeights { get; set; } = new Graph();


        public static double CalculateElementHeight(double value)
        {
            double temp;
            if (value > 0 && value <= 5)
                temp = value * 40;
            else if (value <= 0)
                temp = 0;
            else
                temp = 5*40;
           return temp;
        }

    }
}
