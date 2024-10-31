namespace TrainConfigurator.View
{
    public class MenuItem
    {
        private string _text;
        private Action _handler;

        public MenuItem(string text, Action handler, int whiteSpacesCount = 2)
        {

            _text = AddWhiteSpaces(text, whiteSpacesCount);
            _handler = handler;
        }

        public string Text => _text;
        public int Width => _text.Length;

        public void Click()
        {
            _handler.Invoke();
        }

        public override string ToString()
        {
            return _text;
        }

        private string AddWhiteSpaces(string text, int whiteSpacesCount)
        {
            string result = string.Empty;

            for (int i = 0; i < whiteSpacesCount; i++)
            {
                result += ' ';
            }

            result += text;

            return result;
        }
    }
}
