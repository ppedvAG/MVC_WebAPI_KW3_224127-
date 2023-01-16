namespace LizkovischesPrincipe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    #region Anti Code

    public class Erdbeere
    {
        public string GetColor()
            => "rot";
    }

    public class Kirsche : Erdbeere
    {
        public string GetColor()
            => base.GetColor();
    }

    #endregion


    #region Lizkovisches Prinzip lehnt sich an Open-Close an 

    public abstract class Fruits
    {
        public abstract string GetColor();
    }

    public class Erbeere2 : Fruits
    {
        public override string GetColor()
        {
            return "rot";
        }
    }

    public class Cherry : Fruits 
    {
        public override string GetColor()
        {
            return "rot";
        }
    }

}