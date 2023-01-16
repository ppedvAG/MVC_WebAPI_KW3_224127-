namespace DependencyInversion
{
   

    #region Anti Code - Beispiel
    //Programmierer A: -> benötigt 5 Tage

    //Start und Dauer der Entwicklung: von Tag 1 - bis Tag 5

    public class Vehicles
    {
        public string Marke { get; set; }
        public string Modell { get; set; }

        public int ConstructionYerar { get; set; }

        public int Spritstand
        {
            get
            {
                return 123; 
            }
        }

    }

    //Programmierer B: benötogt -> 3 Tage
    //Start der Entwicklung: von Tag 5 - Tag 8
    public class CarService
    {
        public void Repair(Vehicles vehicle) //Feste Kopplung 
        {
            Console.WriteLine("Auto wird repariert");

            Console.WriteLine($"{vehicle.Marke}");
        }
    }

    #endregion



    #region Best Practise 


    //Programmierer A: (5 Tage) 
    public interface ICar
    {
        public string Modell { get; set; }  
        public string Marke { get; set; }

        public int ConstructionYerar { get; set; }
    }

    public class Car2 : ICar
    {
        public string Modell { get; set; }
        public string Marke { get; set; }
        public int ConstructionYerar { get; set; }
    }

    //Programmieer B: (3 Tage) 
    public interface ICarService
    {
        void Repair(ICar vehicle);  
    }

    public class CarService2 : ICarService
    {
        public void Repair(ICar vehicle)
        {
           //Repariere Auto
        }
    }

    public class MockCar : ICar
    {
        public string Modell { get; set; } = "VW";
        public string Marke { get; set; } = "Polo";
        public int ConstructionYerar { get; set; } = 2020;
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            ICarService carService = new CarService2();
            carService.Repair(new MockCar());

            //Ab Tag 5, wenn Car2 Klasse fertig ist

            carService.Repair(new Car2());
        }
    }
    #endregion
}