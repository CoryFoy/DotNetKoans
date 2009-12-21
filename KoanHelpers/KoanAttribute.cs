namespace DotNetKoans
{
    //This attribute is because we can't guarantee the order
    //of the methods when we walk a class. So this allows us
    //to order them to make sure we evaluate them as intended.
    //Note that positioning is relative to the class, not to the
    //entire project
    public class KoanAttribute : Xunit.FactAttribute
    {
        public int Position { get; set; }

        public KoanAttribute(int position) { this.Position = position; }
    }
}
