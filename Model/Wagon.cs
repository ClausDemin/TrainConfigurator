namespace TrainConfigurator.Model
{
    public class Wagon
    {
        private readonly Passenger[] _passengers;

        public Wagon(int capacity)
        {
            Capacity = capacity;

            _passengers = new Passenger[Capacity];

            PassangersCount = 0;
        }

        public Wagon Previous { get; private set; }
        public Wagon Next { get; private set; }

        public int Capacity { get; }
        public int PassangersCount { get; private set; }
        public int Number => Previous == null ? 1 : Previous.Number + 1;
        public bool IsFull => PassangersCount == Capacity;

        public bool AddPassenger(Passenger passenger)
        {
            if (IsFull == false)
            {
                _passengers[PassangersCount] = passenger;

                PassangersCount++;

                return true;
            }

            return false;
        }

        public void AttachWagon(Wagon wagon)
        {
            wagon.Previous = this;
            Next = wagon;
        }

        public IEnumerator<Passenger> GetEnumerator()
        {
            for (int i = 0; i < PassangersCount; i++)
            {
                yield return _passengers[i];
            }
        }
    }
}
