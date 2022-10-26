using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NetworkService.AdditionalElements;
using NetworkService.Model;


namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        public static BindingList<Generatori> GeneratoriDisplay { get; set; } //lista objekata
        public List<Canvas> CanvasList { get; set; } = new List<Canvas>(); // nema
        //canvas objekti
        public static Dictionary<string, Generatori> GeneratoriCanvas { get; set; } = new Dictionary<string, Generatori>();


        private int selectedIndex = 0;
        public static Generatori draggedItem = null;
        private bool dragging = false;

        private static bool postoji = false;
        private ListView listViewItem;
        //private Canvas canvasViewItem;

        public MyICommand<Generatori> SelectionChangedCommand { get; set; }
        public MyICommand<ListView> MouseLeftButtonUpCommand { get; set; }
        public MyICommand<Canvas> DragOverCommand { get; set; }
        public MyICommand<Canvas> DragLeaveCommand { get; set; }
        public MyICommand<Canvas> DropCommand { get; set; }
        public MyICommand<Canvas> FreeCommand { get; set; }
        public MyICommand<Canvas> LoadCommand { get; set; }
        public MyICommand<ListView> LoadListViewCommand { get; set; }
        public MyICommand ShowHelpCommand { get; set; }
        public MyICommand RasporediCommand { get; set; }



        public MyICommand<Canvas> SelectionChangedCanvasCommand { get; set; }
        public MyICommand<Canvas> MouseLeftButtonUpCanvasCommand { get; set; }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");

            }
        }
         

        public NetworkDisplayViewModel()
        {
            GeneratoriDisplay = new BindingList<Generatori>();

            SelectionChangedCommand = new MyICommand<Generatori>(OnSelectionChanged);
            MouseLeftButtonUpCommand = new MyICommand<ListView>(OnMouseLeftButtonUp);
            DragOverCommand = new MyICommand<Canvas>(OnDragOver);
            DragLeaveCommand = new MyICommand<Canvas>(OnDragLeave);
            DropCommand = new MyICommand<Canvas>(OnDrop);
            FreeCommand = new MyICommand<Canvas>(OnFree);
            LoadCommand = new MyICommand<Canvas>(OnLoad);
            LoadListViewCommand = new MyICommand<ListView>(OnLoadListView);
            ShowHelpCommand = new MyICommand(ShowHelp);
            RasporediCommand = new MyICommand(Rasporedi);
            SelectionChangedCanvasCommand = new MyICommand<Canvas>(OnSelectionCanvasChanged);
            MouseLeftButtonUpCanvasCommand = new MyICommand<Canvas>(OnMouseLeftButtonUpCanvas);

            foreach (Generatori pz in NetworkEntitiesViewModel.GeneratoriLista)
            {
                postoji = false;
                foreach (Generatori pz2 in GeneratoriCanvas.Values)
                {
                    if (pz.Id == pz2.Id)
                    {
                        postoji = true;
                        break;
                    }

                }
                if (postoji == false)
                    GeneratoriDisplay.Add(new Generatori() { Id = pz.Id, Naziv = pz.Naziv, Tip = pz.Tip, Value = pz.Value });
            }
           
        }

        public void OnSelectionChanged(Generatori pz)
        {
            if (!dragging)                                                  // da li smo u fazi prevlacenja, ako nismo, vrsi se drag
            {
                dragging = true;
                draggedItem = pz;
                DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
            }
        }

        public void OnMouseLeftButtonUp(ListView lw)
        {                                                                   // kada se izvrsi klik, sve se postavlja na pocetne vrijednosti
            draggedItem = null;
            lw.SelectedItem = null;
            dragging = false;
        }

//############################################
        public void OnSelectionCanvasChanged(Canvas c)
        {
            if (!dragging)
            {
                if (c.Resources["taken"] != null)
                {
                    dragging = true;
                    draggedItem = GeneratoriCanvas[c.Name];

                    c.Background = Brushes.White;
                    c.Resources.Remove("taken");
                    GeneratoriCanvas.Remove(c.Name);
                    DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                }
            }
        }

        public void OnMouseLeftButtonUpCanvas(Canvas c)
        {                                                                   
            
        }
//#############################################



        public void OnDragOver(Canvas c)
        {                                                                   // kada predjemo preko zauzete povrsine
            if (c.Resources["taken"] != null)
            {
                ((TextBlock)(c).Children[1]).Text = "ZAUZETO";
                ((TextBlock)(c).Children[1]).FontSize = 22;
                ((TextBlock)(c).Children[1]).FontWeight = FontWeights.ExtraBold;
                ((TextBlock)(c).Children[1]).Foreground = Brushes.DarkRed;
                ((TextBlock)(c).Children[1]).Background = Brushes.Black;

            }
  
        }

        public void OnDragLeave(Canvas c)
        {                                                                   // kada sklonimo kursor sa zauzete povrsine
            if (((TextBlock)(c).Children[1]).Text == "ZAUZETO")
            {
                ((TextBlock)(c).Children[1]).Text = "";
                ((TextBlock)(c).Children[1]).Background = Brushes.Transparent;
            }
        }

        public void OnDrop(Canvas c)
        {   

            if (draggedItem != null)                                        // mozemo spustiti sliku
            {
                if (c.Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();

                    logo.BeginInit();
                    logo.UriSource = new Uri("pack://application:,,,/Images/" + draggedItem.Tip.Name.ToString() + ".png", UriKind.Absolute);
                    logo.EndInit();

                    c.Background = new ImageBrush(logo);
                    GeneratoriCanvas[c.Name] = draggedItem;
                    c.Resources.Add("taken", true);
                    GeneratoriDisplay.Remove(GeneratoriDisplay.Single(x => x.Id == draggedItem.Id));
                    SelectedIndex = 0;
                    CheckValue(c);
                }
                else
                {                                                           // ne mozemo spustiti sliku, vec je zauzeta
                    ((TextBlock)(c).Children[1]).Text = "";
                    ((TextBlock)(c).Children[1]).Background = Brushes.Transparent;
                }
                dragging = false;
            }
        }

        public void OnFree(Canvas c)
        {
            try
            {
                if (c.Resources["taken"] != null)
                {
                                                                            // uklanjamo sliku ako je ima
                    c.Background = Brushes.White;
                    foreach (Generatori pz in NetworkEntitiesViewModel.GeneratoriLista)
                    {
                        if (!GeneratoriDisplay.Contains(pz) && GeneratoriCanvas[c.Name].Id == pz.Id)
                            GeneratoriDisplay.Add(pz);
                    }
                    c.Resources.Remove("taken");
                    GeneratoriCanvas.Remove(c.Name);
                }
            }
            catch (Exception) { }

        }


        public void OnLoadListView(ListView listview)
        {
            //lv dobija vrednost ListView-a gde su obj
            listViewItem = listview;
        }

        public void OnLoad(Canvas c)
        {
            if (GeneratoriCanvas.ContainsKey(c.Name))
            {
                BitmapImage logo = new BitmapImage();

                logo.BeginInit();
                string temp = GeneratoriCanvas[c.Name].Tip.Name.ToString() + ".png";
                logo.UriSource = new Uri("pack://application:,,,/Images/" + temp, UriKind.Absolute);
                logo.EndInit();

                c.Background = new ImageBrush(logo);
                ((TextBlock)(c).Children[1]).Text = "";
                c.Resources.Add("taken", true);

                CheckValue(c);

            }
            if (!CanvasList.Contains(c))
            {
                CanvasList.Add(c);
            }
        }

        private void CheckValue(Canvas c)
        {
            Dictionary<int, Generatori> temp = new Dictionary<int, Generatori>();
            foreach (var pz in NetworkEntitiesViewModel.GeneratoriLista)
            {
                temp.Add(pz.Id, pz);
            }
            Task.Delay(1000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ((Border)(c).Children[0]).BorderBrush = Brushes.Transparent;

                    if (GeneratoriCanvas.Count != 0)
                    {
                        if (GeneratoriCanvas.ContainsKey(c.Name))
                        {
                            if (temp[GeneratoriCanvas[c.Name].Id].Value < 1)
                            {
                                ((Border)(c).Children[0]).BorderBrush = Brushes.Blue;
                            }
                            else if (temp[GeneratoriCanvas[c.Name].Id].Value > 5)
                            {
                                ((Border)(c).Children[0]).BorderBrush = Brushes.Red;
                            }
                            else
                            {
                                ((Border)(c).Children[0]).BorderBrush = Brushes.Green;
                            }
                        }
                    }
                });
                CheckValue(c);
            });
        }

        private void ShowHelp()
        {
            System.Windows.MessageBox.Show("Prevlacenjem polja u slicice pravite raspored generatora.\n Slika predstavlja tip generatora.\nKlikom na 'Remove' brise se zadati element iz mreze slicica.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Rasporedi()
        {
            foreach (Generatori g in GeneratoriDisplay)
            {
                /*if (g != null)
                {
                    //OnDrop(Cnv1);
                    //DropCommand();

                        if (!GeneratoriDisplay.Contains(g) && GeneratoriCanvas["Cnv1"].Id == g.Id)
                            GeneratoriDisplay.Add(g);
/                }*/
            }
        }
    }
}
