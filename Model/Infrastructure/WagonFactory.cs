namespace TrainConfigurator.Model.Infrastructure
{
    public class WagonFactory
    {
        public Wagon CreateWagon(WagonType wagonType)
        {
            return new Wagon((int)wagonType);
        }
    }

    public enum WagonType
    {
        Coupe = 36,
        SecondClass = 54
    }
}
