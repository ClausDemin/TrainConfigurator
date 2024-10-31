using TrainConfigurator.Model.Infrastructure;
using TrainConfigurator.Model;
using TrainConfigurator.View.Intefaces;

namespace TrainConfigurator.Contorller
{
    public class TrainPresenter
    {
        private readonly TrainFactory _trainFactory;
        private readonly List<Train> _trains;

        private ITrainConfiguratorView _view;

        public TrainPresenter(ITrainConfiguratorView view)
        {
            _trainFactory = new TrainFactory();
            _trains = new List<Train>();
            _view = view;
        }

        public bool TryGetTrain(string from, string to, out Train train, int minPassengersCount = 200, int maxPassengersCount = 500)
        {
            if (string.IsNullOrEmpty(from.Trim()) || string.IsNullOrEmpty(to.Trim())) 
            {
                _view.PrintMessage(["Пункт назначения или отправления не могут быть пустыми!"], ConsoleColor.Red);

                train = null;

                return false;
            }

            train = _trainFactory.CreateTrain(from, to, minPassengersCount, maxPassengersCount);

            _trains.Add(train);

            return true;
        }

        public string[] GetFullTrainInfo(Train train) 
        {
            List<string> trainInfo = new List<string>();

            trainInfo.Add(GetTrainInfo(train));

            foreach (var wagon in train) 
            {
                trainInfo.Add($"Вагон номер: {wagon.Number}, вместимостью {wagon.Capacity} пассажиров. " +
                    $"Пассажиров в вагоне {wagon.PassangersCount}");
            }

            return trainInfo.ToArray();
        }

        public string[] GetTrainInfoForEachTrain() 
        { 
            var overallTrainsInfo = new List<string>();

            foreach (var train in _trains) 
            {
                overallTrainsInfo.Add(GetTrainInfo(train));
            }

            return overallTrainsInfo.ToArray();
        }

        private string GetTrainInfo(Train train)
        {

            string trainInfo = $"Поезд {train.Route}, длина состава {train.Length} вагонов, число пассажиров {train.PassengersCount}";

            return trainInfo;
        }

    }
}
