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

            // Test Notes
            new Notiz(Kategorie.Geburtstage, "Mutter: 18.03.1945");
            new Notiz(Kategorie.Geburtstage, "Vater: 21.08.1940");
            new Notiz(Kategorie.Internet, "www.ibb.com\r\nViele interessante Kurse");
            new Notiz(Kategorie.Urlaub, "Mallorca\r\nwar nicht gut!");
            new Notiz(Kategorie.Wichtig, "Steuererklärung\r\nmuss noch gemacht werden!!!");

            // Fill ComboBox
            cbxKategorie.Items.Clear();
            foreach (var kat in Enum.GetValues(typeof(Kategorie)))
                cbxKategorie.Items.Add(kat);

            cbxKategorie.SelectedItem = Kategorie.Alle;

            // Update List
            listeAktualisieren();
        }

        private void listeAktualisieren()
        {
            var aktuelleNotiz = AktuelleNotiz;
            var gewählteKat = (Kategorie)cbxKategorie.SelectedItem;

            lbxNotizen.Items.Clear();

            foreach (Notiz notiz in Notiz.Notizen.Values)
            {
                if (gewählteKat == Kategorie.Alle || notiz.Kategorie == gewählteKat)
                    lbxNotizen.Items.Add(notiz);
            }

            // Restore selection if possible
            lbxNotizen.SelectedItem = aktuelleNotiz;
        }

        private void cbxKategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listeAktualisieren();
            btnNeu.IsEnabled = (Kategorie)cbxKategorie.SelectedItem != Kategorie.Alle;
        }

        private void lbxNotizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AktuelleNotiz = lbxNotizen.SelectedItem as Notiz;
            btnLöschen.IsEnabled = lbxNotizen.SelectedIndex > -1;
        }

        private void tbxNotiz_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSpeichern.IsEnabled = AktuelleNotiz != null && tbxNotiz.Text != "";
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if (AktuelleNotiz != null)
            {
                AktuelleNotiz.Inhalt = tbxNotiz.Text;
                listeAktualisieren();
                btnSpeichern.IsEnabled = false;
            }
        }

        private void btnLöschen_Click(object sender, RoutedEventArgs e)
        {
            if (AktuelleNotiz != null)
            {
                var result = MessageBox.Show("Soll die Notiz wirklich gelöscht werden?", "Notiz löschen", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    Notiz.Entfernen(AktuelleNotiz);
                    AktuelleNotiz = null;
                    listeAktualisieren();
                }
            }
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            if ((Kategorie)cbxKategorie.SelectedItem != Kategorie.Alle)
            {
                AktuelleNotiz = new Notiz((Kategorie)cbxKategorie.SelectedItem, "Neue Notiz");
                listeAktualisieren();
                tbxNotiz.Focus();
                tbxNotiz.SelectAll();
            }
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSuche_Click(object sender, RoutedEventArgs e)
        {
            string suchText = tbxSuche.Text.Trim().ToLower();
            lbxNotizen.Items.Clear();

            var gewählteKat = (Kategorie)cbxKategorie.SelectedItem;

            foreach (Notiz notiz in Notiz.Notizen.Values)
            {
                bool inKategorie = gewählteKat == Kategorie.Alle || notiz.Kategorie == gewählteKat;
                bool enthältText = string.IsNullOrEmpty(suchText) || notiz.Inhalt.ToLower().Contains(suchText);

                if (inKategorie && enthältText)
                    lbxNotizen.Items.Add(notiz);
            }
        }

        private void btnSucheAufheben_Click(object sender, RoutedEventArgs e)
        {
            tbxSuche.Clear();
            listeAktualisieren();
        }


    }
}