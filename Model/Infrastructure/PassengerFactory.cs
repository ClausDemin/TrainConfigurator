namespace TrainConfigurator.Model.Infrastructure
{
    public class PassangersFactory
    {
        public Queue<Passenger> CreatePassangersQueue(int minPassangersCount, int maxPassangersCount)
        {
            var random = new Random();
            var passengers = new Queue<Passenger>();

            int passangersCount = random.Next(minPassangersCount, maxPassangersCount + 1);

            for (int i = 0; i < passangersCount; i++)
            {
                passengers.Enqueue(new Passenger(i));
            }

            return passengers;
        }
    }
}
