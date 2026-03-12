namespace Notizbuch.Models
{
    public class Notiz
    {
        public static Dictionary<int, Notiz> Notizen = new Dictionary<int, Notiz>();
        public Kategorie Kategorie { get; set; }
        public string Inhalt { get; set; }

        public Notiz(Kategorie k, string inhalt)
        {
            Kategorie = k;
            Inhalt = inhalt;
            Notizen[Notizen.Count] = this;
        }

        public static void Entfernen(Notiz n)
        {
            if (Notizen.ContainsValue(n))
                Notizen.Remove(Notizen.First(x => x.Value == n).Key);
        }

        public override string ToString() => Inhalt;
    }
}