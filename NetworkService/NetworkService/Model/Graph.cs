using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NetworkService.AdditionalElements;

namespace NetworkService.Model
{
    public class Graph : BindableBase
    {
        private double firstBindingPoint;
        public double FirstBindingPoint
        {
            get { return firstBindingPoint; }
            set
            {
                SecondBindingPoint = firstBindingPoint;
                firstBindingPoint = value;
                OnPropertyChanged("FirstBindingPoint");
            }
        }

        private double secondBindingPoint;
        public double SecondBindingPoint
        {
            get { return secondBindingPoint; }
            set
            {
                ThirdBindingPoint = secondBindingPoint;
                secondBindingPoint = value;

                OnPropertyChanged("SecondBindingPoint");
            }
        }

        private double thirdBindingPoint;
        public double ThirdBindingPoint
        {
            get { return thirdBindingPoint; }
            set
            {
                FourthBindingPoint = thirdBindingPoint;
                thirdBindingPoint = value;
                OnPropertyChanged("ThirdBindingPoint");
            }
        }

        private double fourthBindingPoint;
        public double FourthBindingPoint
        {
            get { return fourthBindingPoint; }
            set
            {
                FifthBindingPoint = fourthBindingPoint;
                fourthBindingPoint = value;
                OnPropertyChanged("FourthBindingPoint");
            }
        }

        private double fifthBindingPoint;
        public double FifthBindingPoint
        {
            get { return fifthBindingPoint; }
            set
            {
                fifthBindingPoint = value;

                OnPropertyChanged("FifthBindingPoint");
            }
        }

        private double firstValue;
        public double FirstValue
        {
            get { return firstValue; }
            set
            {
                SecondValue = firstValue;
                firstValue = value;

                OnPropertyChanged("FirstValue");
            }
        }

        private double secondValue;
        public double SecondValue
        {
            get { return secondValue; }
            set
            {
                ThirdValue = secondValue;
                secondValue = value;
                OnPropertyChanged("SecondValue");
            }
        }

        private double thirdValue;
        public double ThirdValue
        {
            get { return thirdValue; }
            set
            {
                FourthValue = thirdValue;
                thirdValue = value;
                OnPropertyChanged("ThirdValue");
            }
        }

        private double fourthValue;
        public double FourthValue
        {
            get { return fourthValue; }
            set
            {
                FifthValue = fourthValue;
                fourthValue = value;
                OnPropertyChanged("FourthValue");
            }
        }

        private double fifthValue;
        public double FifthValue
        {
            get { return fifthValue; }
            set

            {
                fifthValue = value;
                OnPropertyChanged("FifthValue");
            }
        }
       
    }

}
