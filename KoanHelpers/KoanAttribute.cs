namespace DotNetKoans
{
    public class KoanAttribute : Xunit.FactAttribute
    {
        public int Position { get; set; }

        public KoanAttribute(int position) { this.Position = position; }
    }
}
