namespace PrestamoBlazorApp.Models
{
    public class EnumModel2
    {
        public int Index { get; private set; }

        public string Text { get; private set; }

        public EnumModel2(int index, string text)
        {
            Index = index;
            Text = text;
        }

        public override string ToString() => $"{Index} {Text}";
    }


}
