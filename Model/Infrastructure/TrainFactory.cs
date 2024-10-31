namespace TrainConfigurator.Model.Infrastructure
{
    public class TrainFactory
    {
        private WagonFactory _wagonFactory;
        private PassangersFactory _passangersFactory;

        public TrainFactory()
        {
            _wagonFactory = new WagonFactory();
            _passangersFactory = new PassangersFactory();
        }

        public Train CreateTrain(string from, string to, int minPassangersCount, int maxPassangersCount, float wagonsPercentage = 1 / 3f)
        {
            var route = new Route(from, to);

            var passengers = _passangersFactory.CreatePassangersQueue(minPassangersCount, maxPassangersCount);

            var train = new Train(route);

            PlacePassangers(train, passengers, wagonsPercentage);

            return train;
        }

        private void PlacePassangers(Train train, Queue<Passenger> passengers, float wagonsPercentage)
        {
            int coupeWagonsCount = GetCoupeWagonsCount(passengers.Count, wagonsPercentage);

            while (passengers.Count > 0)
            {
                Wagon wagon = null;

                if (coupeWagonsCount > 0)
                {
                    wagon = _wagonFactory.CreateWagon(WagonType.Coupe);
                    coupeWagonsCount--;
                }
                else
                {
                    wagon = _wagonFactory.CreateWagon(WagonType.SecondClass);
                }

                FillWagon(passengers, wagon);

                train.AddWagon(wagon);
            }
        }

        private int GetCoupeWagonsCount(int passengersCount, float wagonsPercentage)
        {
            int coupeCapacity = (int)WagonType.Coupe;
            int secondClassCapacity = (int)WagonType.SecondClass;

            int coupeWagonsCount =
                (int)Math.Round
                (
                    passengersCount / (secondClassCapacity * (1 - wagonsPercentage) / wagonsPercentage + coupeCapacity)
                );

            return coupeWagonsCount;
        }

        private void FillWagon(Queue<Passenger> passengers, Wagon wagon)
        {
            while (passengers.Count > 0 && !wagon.IsFull)
            {
                var passenger = passengers.Dequeue();

                if (wagon.AddPassenger(passenger) == false)
                {
                    passengers.Enqueue(passenger);
                }
            }
        }
    }
}
