using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OkoBere
{
    enum Stav
    {
        Nic,
        Start,
        Hit,
        Stand,
    }
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private HashSet<string> karty = new HashSet<string>();
        private int dealerovaRuka, hracovaRuka;
        private string dealerovaSkrytaKartaUri = "";
        private int skore = 0;
        private int dealerovaKartaObrazekId, hracovaKartaObrazekId;
        Stav stav = Stav.Start;
        public MainWindow()
        {
            InitializeComponent();
            AktualizovatSkore(0);
            CompositionTarget.Rendering += HerniSmycka;
        }
        private string NahodneUriZadnihoObrazkuKarty()
        {
            string[] uriZadnichObrazkuKaret = { "karty/BB.png", "karty/BR.png" };
            return uriZadnichObrazkuKaret[random.Next(0, 2)];
        }
        private (string, int) NahodneUriAHodnotaKarty()
        {
            // Karty vygenerovány na https://www.me.uk/cards/makeadeck.cgi
            while (true)
            {
                string[] hodnoty = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K" };
                string[] barvy = { "H", "D", "C", "S" };
                int nahodnaHodnota = random.Next(0, 12);
                int nahodnaBarva = random.Next(0, 4);
                string karta = hodnoty[nahodnaHodnota] + barvy[nahodnaBarva];
                if (!karty.Contains(karta))
                {
                    karty.Add(karta);
                    return ("karty/" + karta + ".png", nahodnaHodnota > 9 ? 10 : nahodnaHodnota + 1);
                }
            }
        }
        private void AktualizovatSkore(int noveSkore)
        {
            skore += noveSkore;
            Skore.Text = "Skóre: " + skore;
        }
        private void NastavitZdrojObrazku(Image obrazek, string uri)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri, UriKind.Relative); // new Uri("pack://application:,,,/" + uri);
            bitmap.EndInit();
            obrazek.Source = bitmap;
        }
        private Image DalsiDealerovaKartaObrazek()
        {
            Image[] obrazkyKaret = { DealerovaKarta1, DealerovaKarta2, DealerovaKarta3, DealerovaKarta4, DealerovaKarta5, DealerovaKarta6, DealerovaKarta7, DealerovaKarta8, DealerovaKarta9, DealerovaKarta10, DealerovaKarta11 };
            Image obrazekKarty = obrazkyKaret[dealerovaKartaObrazekId];
            dealerovaKartaObrazekId += 1;
            return obrazekKarty;
        }
        private Image DalsiHracovaKartaObrazek()
        {
            Image[] obrazkyKaret = { HracovaKarta1, HracovaKarta2, HracovaKarta3, HracovaKarta4, HracovaKarta5, HracovaKarta6, HracovaKarta7, HracovaKarta8, HracovaKarta9, HracovaKarta10, HracovaKarta11 };
            Image obrazekKarty = obrazkyKaret[hracovaKartaObrazekId];
            hracovaKartaObrazekId += 1;
            return obrazekKarty;
        }
        private void VymazatObrazkyKaret()
        {
            Image[] obrazkyKaret = {
                DealerovaKarta1, DealerovaKarta2, DealerovaKarta3, DealerovaKarta4, DealerovaKarta5, DealerovaKarta6, DealerovaKarta7, DealerovaKarta8, DealerovaKarta9, DealerovaKarta10, DealerovaKarta11,
                HracovaKarta1, HracovaKarta2, HracovaKarta3, HracovaKarta4, HracovaKarta5, HracovaKarta6, HracovaKarta7, HracovaKarta8, HracovaKarta9, HracovaKarta10, HracovaKarta11
            };
            foreach (Image obrazekKarty in obrazkyKaret) NastavitZdrojObrazku(obrazekKarty, "");
        }

        private void Vyhra()
        {
            AktualizovatSkore(1);
            Zprava.Text = $"Vyhrál jsi! {hracovaRuka}/{dealerovaRuka}";
        }
        private void Prohra()
        {
            AktualizovatSkore(-1);
            Zprava.Text = $"Prohrál jsi! {hracovaRuka}/{dealerovaRuka}";
        }
        private void Remiza()
        {
            Zprava.Text = $"Remíza! {hracovaRuka}/{dealerovaRuka}";
        }
        private void HerniSmycka(object sender, EventArgs e)
        {
            switch (stav)
            {
                case Stav.Start:
                    stav = Stav.Nic;
                    NastavitZdrojObrazku(BalicekKaret, NahodneUriZadnihoObrazkuKarty());
                    (string dealerovaKarta1Uri, int dealerovaKarta1Hodnota) = NahodneUriAHodnotaKarty();
                    (string dealerovaKarta2Uri, int dealerovaKarta2Hodnota) = NahodneUriAHodnotaKarty();
                    dealerovaKartaObrazekId = 0;
                    NastavitZdrojObrazku(DalsiDealerovaKartaObrazek(), dealerovaKarta1Uri);
                    NastavitZdrojObrazku(DalsiDealerovaKartaObrazek(), NahodneUriZadnihoObrazkuKarty());
                    dealerovaRuka = dealerovaKarta1Hodnota + dealerovaKarta2Hodnota;
                    DealerovaRuka.Text = dealerovaKarta1Hodnota + " + ?";
                    dealerovaSkrytaKartaUri = dealerovaKarta2Uri;
                    (string hracovaKarta1Uri, int hracovaKarta1Hodnota) = NahodneUriAHodnotaKarty();
                    (string hracovaKarta2Uri, int hracovaKarta2Hodnota) = NahodneUriAHodnotaKarty();
                    hracovaKartaObrazekId = 0;
                    NastavitZdrojObrazku(DalsiHracovaKartaObrazek(), hracovaKarta1Uri);
                    NastavitZdrojObrazku(DalsiHracovaKartaObrazek(), hracovaKarta2Uri);
                    hracovaRuka = hracovaKarta1Hodnota + hracovaKarta2Hodnota;
                    HracovaRuka.Text = hracovaRuka.ToString();
                    break;
                case Stav.Hit:
                    stav = Stav.Nic;
                    NastavitZdrojObrazku(BalicekKaret, NahodneUriZadnihoObrazkuKarty());
                    (string hracovaKartaUri, int hracovaKartaHodnota) = NahodneUriAHodnotaKarty();
                    NastavitZdrojObrazku(DalsiHracovaKartaObrazek(), hracovaKartaUri);
                    hracovaRuka += hracovaKartaHodnota;
                    HracovaRuka.Text = hracovaRuka.ToString();
                    if (hracovaRuka >= 21) stav = Stav.Stand;
                    break;
                case Stav.Stand:
                    stav = Stav.Nic;
                    TlacitkoHit.Visibility = Visibility.Hidden;
                    TlacitkoStand.Visibility = Visibility.Hidden;
                    TlacitkoDalsiKolo.Visibility = Visibility.Visible;
                    NastavitZdrojObrazku(BalicekKaret, NahodneUriZadnihoObrazkuKarty());
                    NastavitZdrojObrazku(DealerovaKarta2, dealerovaSkrytaKartaUri);
                    if (hracovaRuka > 21) Prohra();
                    else if (dealerovaRuka > hracovaRuka) Prohra();
                    else
                    {
                        while (dealerovaRuka <= hracovaRuka && random.NextDouble() < 1 - (dealerovaRuka - 10) / 11)
                        {
                            (string dealerovaKartaUri, int dealerovaKartaHodnota) = NahodneUriAHodnotaKarty();
                            NastavitZdrojObrazku(DalsiDealerovaKartaObrazek(), dealerovaKartaUri);
                            dealerovaRuka += dealerovaKartaHodnota;
                        }
                        if (dealerovaRuka > 21) Vyhra();
                        else if (dealerovaRuka > hracovaRuka) Prohra();
                        else if (dealerovaRuka < hracovaRuka) Vyhra();
                        else Remiza();
                    }
                    DealerovaRuka.Text = dealerovaRuka.ToString();
                    break;
            }
        }
        private void Hit(object sender, RoutedEventArgs e)
        {
            stav = Stav.Hit;
        }
        private void Stand(object sender, RoutedEventArgs e)
        {
            stav = Stav.Stand;
        }
        private void DalsiKolo(object sender, RoutedEventArgs e)
        {
            VymazatObrazkyKaret();
            karty.Clear();
            TlacitkoHit.Visibility = Visibility.Visible;
            TlacitkoStand.Visibility = Visibility.Visible;
            TlacitkoDalsiKolo.Visibility = Visibility.Hidden;
            Zprava.Text = "";
            stav = Stav.Start;
        }
    }
}
