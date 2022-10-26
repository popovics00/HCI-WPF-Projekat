using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NetworkService.AdditionalElements;
using NetworkService.Model;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public int brojac = 0;
        public static ObservableCollection<Generatori> GeneratoriLista { get; private set; } = new ObservableCollection<Generatori>();
        public static ObservableCollection<Generatori> GeneratoriSearch { get; private set; } = new ObservableCollection<Generatori>();
       
        public static List<string> listaCuvanja= new List<string>();
        public static ObservableCollection<string> SavedSearch { get;  set; }

        public static ObservableCollection<Model.Type> Tipovi { get; set; }
        Model.Type tipSolarni = new Model.Type { Name = "Solarni", Img_src = AppDomain.CurrentDomain.BaseDirectory + "Images\\solarni.png" };
        Model.Type tipVetrogeneratori = new Model.Type { Name = "Vetrogenerator", Img_src = AppDomain.CurrentDomain.BaseDirectory + "Images\\vetrogenerator.png" };

        public static List<string> TipoviComboBox { get; set; } = new List<string> { "Solarni", "Vetrogenerator"};

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand SearchCommand { get; set; }
        public MyICommand ResetCommand { get; set; }
        public MyICommand PreviewMouseDownCommand { get; set; }
        public MyICommand SaveCurrentSearchCommand { get; set; }
        public MyICommand StartSimulatorCommand { get; set; }
        public MyICommand ShowHelpCommand { get; set; }

        private Generatori selectedGenerator;
        private string idText;
        private string nazivText;
        private string imagePath = "";
        private string pathBeforeImage = "pack://application:,,,/Images/";
        public int brojacRandom = 0;
        //simulator
        public static bool pokrenuto = false;

        //za Search
        private static bool nameChecked = true;
        private static bool typeChecked = false;
        private string searchText;
        private string searchTextError;
        private string currentSearchText;

        private string selectedTip = string.Empty;

        public NetworkEntitiesViewModel()
        {
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd);
            SearchCommand = new MyICommand(Search);
            ResetCommand = new MyICommand(ResetSearch);
            StartSimulatorCommand = new MyICommand(StartSimulator);
            SaveCurrentSearchCommand = new MyICommand(SaveSearch);
            ShowHelpCommand = new MyICommand(ShowCommand);
            if (GeneratoriSearch == null)
                GeneratoriSearch = new ObservableCollection<Generatori>();

            if (GeneratoriLista == null)
                GeneratoriLista = new ObservableCollection<Generatori>();
            if (SavedSearch == null)
            {
                SavedSearch = new ObservableCollection<string>();
                //  SavedSearch.Add("Sve");
            }
        }

        private void SaveSearch()
        {
            bool postoji = false;
            
            if (string.IsNullOrEmpty(SearchText))
            {
                SearchTextError = "Nemozemo sacuvati prazno polje!";
                return;
            }
            else
            {
                SearchTextError = "";

                try
                {
                    foreach (string s in SavedSearch)
                    {
                        if (s == SearchText)
                        {
                            postoji = true;
                            break;
                        }
                    }
                }
                catch
                {

                }
            }
            if(postoji)
            {
                SearchTextError = "Ta pretraga vec postoji!";
                return;
            }
            else
            {
                SearchTextError = "";
                if (NameChecked)
                {
                    SavedSearch.Add("Naziv:" + SearchText);
                    CurrentSearchText = "Naziv:" + SearchText;
                }
                else if(TypeChecked)
                {
                    SavedSearch.Add("Tip:" + SearchText);
                    CurrentSearchText = "Tip:" + SearchText;
                }
                OnPropertyChanged("SavedSearch");               
            }      
        }

        
        public string CurrentSearchText
        {
            get {
                if (SavedSearch.Count >= 1)
                {
                    if (currentSearchText.Substring(0, 3) == "Tip")
                    {
                        //SearchText = SavedSearch[0];
                        int tempLen = currentSearchText.Length;
                        string temp = currentSearchText.Substring(tempLen - (tempLen - 4));
                        SearchText = temp;
                        TypeChecked = true;
                    }
                    else if (currentSearchText.Substring(0, 5) == "Naziv")
                    {
                        //SearchText = SavedSearch[0];
                        int tempLen = currentSearchText.Length;
                        string temp = currentSearchText.Substring(tempLen - (tempLen - 6));
                        SearchText = temp;
                        NameChecked = true;
                    }
                }
                return currentSearchText;

            }
            set
            {
                if (currentSearchText != value)
                {
                    currentSearchText = value;
                    OnPropertyChanged("CurrentSearchText");
                    OnPropertyChanged("SearchText");
                }

            }
        }
        public string SearchTextError
        {
            get { return searchTextError; }
            set
            {
                if (searchTextError != value)
                {
                    searchTextError = value;
                    OnPropertyChanged("SearchTextError");
                }
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }

        

        public string ImagePath 
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        public bool NameChecked
        {
            get { return nameChecked; }
            set
            {
                if (nameChecked != value)
                {
                    nameChecked = value;
                    OnPropertyChanged("NameChecked");
                }
            }
        }
        public bool TypeChecked
        {
            get { return typeChecked; }
            set
            {
                if (typeChecked != value)
                {
                    typeChecked = value;
                    OnPropertyChanged("TypeChecked");
                }
            }
        }

        

        public Generatori SelectedGenerator
        {
            get { return selectedGenerator; }
            set
            {
                selectedGenerator = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedTip 
        {
            get { return selectedTip; }
            set
            {
                if(selectedTip != value)
                {
                    selectedTip = value;
                    ImagePath = pathBeforeImage + value.ToString() + ".png";
                    OnPropertyChanged("ImagePath");
                    OnPropertyChanged("SelectedTip");
                }
            }
        }

        private bool CanDelete()
        {
            return selectedGenerator != null;
        }

        private void OnDelete()
        {
            GeneratoriLista.Remove(selectedGenerator);
            ResetSearch();
        }

        private void OnAdd()
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(idText);
                //Console.WriteLine(id) ;
            }
            catch
            {
               
                System.Windows.MessageBox.Show("ID mora biti broj.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                
            }

            bool vecPostoji = false;
   
            foreach (Generatori pz in GeneratoriLista)
            {
                 if (pz.Id == id)
                 {
                   vecPostoji = true;
                 }
            }

            if (vecPostoji)
            {
                System.Windows.MessageBox.Show("ID mora biti jedinstven.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            /*string naziv = NazivText;
            if (string.IsNullOrWhiteSpace(naziv))
            {
                System.Windows.MessageBox.Show("Naziv mora biti unet.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                //return;
            }
            */
            Model.Type tip;
            if(selectedTip == tipSolarni.Name)
            {
                tip = tipSolarni;
            }
            else if(selectedTip == tipVetrogeneratori.Name)
            {
                tip = tipVetrogeneratori;
            }
            else
            {
                System.Windows.MessageBox.Show("Tip mora biti izabran.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Random rnd = new Random();
            //int maksimalno = 5;
            //double vrednost = Math.Round(rnd.NextDouble() * maksimalno);
            double vrednost = 0;
            brojacRandom++;
            id= brojacRandom;
            string naziv = "Generator - " + id ; 
            GeneratoriLista.Add(new Generatori { Id = id, Naziv = naziv, Tip = tip, Value = vrednost });
            ResetSearch();
            
            
        }

        private void Search()
        {

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                System.Windows.MessageBox.Show("Unos za pretragu ne sme biti prazan.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SearchText != "")
            {

                if (NameChecked)
                {
                    GeneratoriSearch.Clear();

                        for (int i = 0; i < GeneratoriLista.Count; i++)
                        {
                            if (GeneratoriLista[i].Naziv.ToLower().Contains(SearchText.ToLower()))
                            {
                                GeneratoriSearch.Add(GeneratoriLista[i]);
                            }

                        }
                }
                else
                { 
                    GeneratoriSearch.Clear();
                    
                        for (int i = 0; i < GeneratoriLista.Count; i++)
                        {
                            if (GeneratoriLista[i].Tip.Name.ToLower().Contains(SearchText.ToLower()))
                            {
                                GeneratoriSearch.Add(GeneratoriLista[i]);
                            }

                        }
                }
            }
        }

        private void ResetSearch()
        {
            GeneratoriSearch.Clear();
            for(int i = 0; i < GeneratoriLista.Count; i++)
            {
                GeneratoriSearch.Add(GeneratoriLista[i]);
            }
            SearchText = "";
            NameChecked = true;
            TypeChecked = false;
        }

        private void StartSimulator()
        {
                Process.Start("MeteringSimulator.exe");
        }

        private void ShowCommand()
        {
            System.Windows.MessageBox.Show("Unos -> Popunjavanjem polja [ID,Naziv,Type] i pritiskom na dugme 'Add'\nBrisanje -> Selektovanjem reda iz tabele i pritiskom na dugme 'Delete'", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
