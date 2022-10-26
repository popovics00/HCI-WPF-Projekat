using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Generatori : INotifyPropertyChanged
    {

        private int id;
        private string naziv;
        private Type tip;
        private double value;
        public Generatori() { }
        public Generatori(int id, string naziv, Type tip)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Tip = tip;

            
        }

        public Generatori(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    RaisePropertyChanged("Naziv");
                }
            }
        }
        public double Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    RaisePropertyChanged("Value");
                    ViewModel.MeasurmentGraphViewModel.ElementHeights.FirstBindingPoint = ViewModel.MeasurmentGraphViewModel.CalculateElementHeight(value);
                    ViewModel.MeasurmentGraphViewModel.ElementHeights.FirstValue = value;
                    

                }
            }
        }
        public Type Tip
        {
            get
            {
                return tip;
            }
            set
            {
                if (tip != value)
                {
                    tip = value;
                    RaisePropertyChanged("Tip");
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
