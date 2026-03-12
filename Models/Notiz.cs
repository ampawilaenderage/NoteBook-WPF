namespace Notizbuch.Models
{
    public class Notiz
    {
        public static Dictionary<int, Notiz> Notizen = new Dictionary<int, Notiz>();

        public Kategorie Kategorie { get; set; }

        public string Inhalt { get; set; }

        public DateTime ErstelltAm { get; set; } = DateTime.Now;

        public Notiz(Kategorie kategorie, string inhalt)
        {
            Kategorie = kategorie;
            Inhalt = inhalt;

            Notizen[Notizen.Count] = this;
        }

        public static void Entfernen(Notiz notiz)
        {
            var key = Notizen.FirstOrDefault(x => x.Value == notiz).Key;

            if (Notizen.ContainsKey(key))
                Notizen.Remove(key);
        }

        public override string ToString()
        {
            return Inhalt;
        }
    }
}