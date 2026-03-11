namespace Notizbuch.Models
{
    public class Notiz
    {
        public static Dictionary<Guid, Notiz> Notizen = new Dictionary<Guid, Notiz>();

        public Guid ID { get; private set; }
        public Kategorie Kategorie { get; private set; }
        public string Inhalt { get; set; }
        public DateTime ErstelltAm { get; private set; }

        public Notiz(Kategorie kat, string text)
        {
            ID = Guid.NewGuid();
            Kategorie = kat;
            Inhalt = text;
            ErstelltAm = DateTime.Now;
            Notizen.Add(ID, this);
        }

        public static bool Entfernen(Notiz notiz) => Notizen.Remove(notiz.ID);
    }
}