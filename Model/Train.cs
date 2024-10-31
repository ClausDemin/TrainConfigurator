namespace TrainConfigurator.Model
{
    public class Train
    {
        private Wagon _head;
        private Wagon _tail;

        public Train(Route route)
        {
            Route = route;
        }

        public Route Route { get; }
        public int Length => _tail == null ? 0 : _tail.Number;
        public int PassengersCount => GetOverallPassengersCount();

        public void AddWagon(Wagon wagon)
        {
            if (_head == null)
            {
                _head = wagon;
            }
            else
            {
                _tail.AttachWagon(wagon);
            }

            _tail = wagon;
        }

        public IEnumerator<Wagon> GetEnumerator()
        {
            var wagon = _head;

            while (wagon != null)
            {
                yield return wagon;
                wagon = wagon.Next;
            }
        }

        private int GetOverallPassengersCount() 
        { 
            int passengersCount = 0;

            foreach (var wagon in this) 
            {
                passengersCount += wagon.PassangersCount;
            }

            return passengersCount;
        }
    }
}
