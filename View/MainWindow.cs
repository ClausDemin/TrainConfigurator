using TrainConfigurator.Contorller;
using System.Drawing;
using TrainConfigurator.View.Intefaces;

namespace TrainConfigurator.View
{
    public class MainWindow:ITrainConfiguratorView
    {
        private readonly SwitchableMenu _menu;
        private TextBox _textBox;

        private readonly TrainPresenter _presenter;
        private bool _isExitRequest;

        public MainWindow()
        {
            _presenter = new TrainPresenter(this);

            var items = new List<MenuItem>()
            {
                new MenuItem("Создать поезд", HadnleTrainCreation, 4),
                new MenuItem("Закрыть приложение", CloseApplication, 4)
            };

            _menu = new SwitchableMenu(new Point(0, 0), items);

            var textBoxPosition = GetPositionAfter(_menu);

            _textBox = new TextBox(textBoxPosition, Console.BufferWidth - textBoxPosition.X);
        }

        public void HadnleTrainCreation()
        {
            string from = _textBox.GetUserInput("Пожалуйста введите пункт отправления поезда: ");
            string to = _textBox.GetUserInput("Пожалуйста введите пункт назначения поезда: ");

            if (_presenter.TryGetTrain(from, to, out var train))
            {
                string[] trainInfo = _presenter.GetFullTrainInfo(train);

                PrintMessage(trainInfo, ConsoleColor.Yellow);
            }

            _textBox.AppendText("Для продолжения нажмите любую клавишу", ConsoleColor.Gray);

            Console.ReadKey();

            PrintMessage(_presenter.GetTrainInfoForEachTrain(), ConsoleColor.DarkYellow);
        }

        public void PrintMessage(string[] message, ConsoleColor textColor = ConsoleColor.Gray) 
        {
            _textBox.UpdateText(message, textColor);
        }

        public void Run()
        {
            Show();

            Console.CursorVisible = false;

            _isExitRequest = false;

            while (_isExitRequest == false)
            {
                var userControl = Console.ReadKey(true).Key;

                switch (userControl)
                {
                    case ConsoleKey.UpArrow:
                        _menu.MoveCursor(CursorMovement.Up);
                        break;

                    case ConsoleKey.DownArrow:
                        _menu.MoveCursor(CursorMovement.Down);
                        break;

                    case ConsoleKey.Enter:
                        _menu.Click();
                        break;
                }
            }
        }

        private void Show()
        {
            _menu.Show();
        }

        private void CloseApplication()
        {
            _isExitRequest = true;

            Console.Clear();
        }

        private Point GetPositionAfter(IUIElement element, int offsetX = 10)
        {
            return new Point(element.Position.X + element.Width + offsetX, element.Position.Y);
        }
    }
}
