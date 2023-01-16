namespace InterfaceSegerationPrinzip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    #region Bad Sample
    public interface IVehicle
    {
        void Drive();
        void Fly();
        void Swim();
    }

    public class Vehicle : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("kann fahren");
        }

        public void Fly()
        {
            Console.WriteLine("kann fliegen");
        } 

        public void Swim()
        {
            Console.WriteLine("kann schwimmen");
        }
    }

    public class AmphibischesFahrzeug : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("kann fahren");
        }

        public void Fly()
        {
            //???????????????????????????????
            throw new NotImplementedException();
        }

        public void Swim()
        {
            Console.WriteLine("kann schwimmen");
        }
    }
    #endregion

    public interface IFly
    {
        void Fly();
    }

    public interface IDrive
    {
        void Drive();   
    }

    public interface ISwim
    {
        void Swim();
    }

    public class AmphibischesFahrzeugBestPractice : ISwim, IDrive
    {
        public void Drive()
        {
            //kann fahren
        }

        public void Swim()
        {
            //kann schwimmen
        }
    }

    //weiteres Beispiel -> https://github.com/SharpRepository/SharpRepository/blob/develop/SharpRepository.Repository/Traits/ICanAdd.cs
}