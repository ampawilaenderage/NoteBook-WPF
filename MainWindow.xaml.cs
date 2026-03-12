using Notizbuch.Models;
using System.Windows;
using System.Windows.Controls;

namespace Notizbuch
{
    public partial class MainWindow : Window
    {
        private Notiz _AktuelleNotiz;

        public Notiz AktuelleNotiz
        {
            get => _AktuelleNotiz;

            set
            {
                _AktuelleNotiz = value;

                tbxNotiz.Text = value?.Inhalt ?? "";

                tbxNotiz.IsEnabled = value != null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            new Notiz(Kategorie.Geburtstage, "Mutter: 18.03.1945");
            new Notiz(Kategorie.Geburtstage, "Vater: 21.08.1940");
            new Notiz(Kategorie.Internet, "www.ibb.com");
            new Notiz(Kategorie.Urlaub, "Mallorca war nicht gut!");
            new Notiz(Kategorie.Wichtig, "Steuererklärung machen!");

            cbxKategorie.Items.Clear();

            foreach (var kat in Enum.GetValues(typeof(Kategorie)))
                cbxKategorie.Items.Add(kat);

            cbxKategorie.SelectedItem = Kategorie.Alle;

            listeAktualisieren();
        }

        void listeAktualisieren(string suchtext = "")
        {
            var gewählteKat = (Kategorie)cbxKategorie.SelectedItem;

            var query = Notiz.Notizen.Values.Where(
                notiz => notiz.Inhalt.Contains(suchtext)
                && (gewählteKat == Kategorie.Alle
                || notiz.Kategorie == gewählteKat))
                .OrderBy(notiz => notiz.Kategorie)
                .ThenBy(notiz => notiz.Inhalt);

            DataContext = query;

            lbxNotizen.SelectedItem = AktuelleNotiz;
        }

        private void cbxKategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listeAktualisieren();
        }

        private void lbxNotizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AktuelleNotiz = (Notiz)lbxNotizen.SelectedItem;
        }

        private void tbxNotiz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AktuelleNotiz != null)
                AktuelleNotiz.Inhalt = tbxNotiz.Text;
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            if ((Kategorie)cbxKategorie.SelectedItem != Kategorie.Alle)
            {
                AktuelleNotiz = new Notiz((Kategorie)cbxKategorie.SelectedItem, "Neue Notiz");

                listeAktualisieren();
            }
        }

        private void btnLöschen_Click(object sender, RoutedEventArgs e)
        {
            if (AktuelleNotiz != null)
            {
                Notiz.Entfernen(AktuelleNotiz);

                AktuelleNotiz = null;

                listeAktualisieren();
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            listeAktualisieren();
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSuche_Click(object sender, RoutedEventArgs e)
        {
            listeAktualisieren((sender as Button).Name == "btnSuche"
                ? tbxSuche.Text
                : "");
        }
    }
}