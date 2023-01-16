namespace OpenClosePrincipe
{
   

    public class Employee
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }

    public class AntiCodeSample_ReportGenerator
    {
        public object GenerateReport(Employee emp, int Type) 
        {
            object obj = new object();

            if (Type == 1) 
            {
                //Type == 1, verwende den CrystalReport
                //obj = 
            }
            else if(Type == 2)
            {
                //Type == 2, verwende den List10 
                //obj = 
            }
            else if(Type == 3 )
            {
                //Type == 3, verwende anderer Reporter
                //obj = 
            }
            else
            {
                //Type == XML Report 
                //obj = 
            }

            return obj; 
        }
    }


    #region Open Close Prinzip mit abstrakter Klasse

    public abstract class ReportGeneratorBase
    {
        public abstract void GenerateReport(Employee em);

        protected void InterneMethode(Employee em)
        {
            //
        }
    }

    public class CrystalReportGenerator : ReportGeneratorBase
    {
        //Spezifische Properties für CR 

        //Optionen für Crystal Reports kann man als Properties dieser Klasse hinterlegen und via GenerateReport darauf zugreifen 
        public override void GenerateReport(Employee em)
        {
            //

            InterneMethode(em);
        }
    }

    public class List10ReportGenerator : ReportGeneratorBase
    {



        public override void GenerateReport(Employee em)
        {
            InterneMethode(em);
        }
    }
    #endregion


    #region Open Close Prinzip mit einem Interface

    public interface IReportGenerator
    {
        void GenerateReport(Employee em);   
    }

    public class CrystalReport2Generator : IReportGenerator
    {
        public void GenerateReport(Employee em)
        {
            //Erstelle einen Report
        }
    }

    #endregion


    #region Kombination von Interfaces und Abstrakter Klasse gehen auch
    public interface IReportGenerator2
    {
        void GenerateReport(Employee em);
    }

    

    public abstract class ReportGenerator2Base : IReportGenerator2
    {
        public abstract void GenerateReport(Employee em);

        public abstract void EineZweiteAbstrakteMethode();
    }

    public class TelerikReportGenerator : ReportGenerator2Base
    {
        public override void EineZweiteAbstrakteMethode()
        {
           //Macht irgendwas was
        }

        public override void GenerateReport(Employee em)
        {
            //Erstelle ein Report mit dem Telerik-Reporter Lib
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            TelerikReportGenerator telerikReportGenerator = new();
            telerikReportGenerator.EineZweiteAbstrakteMethode();
            telerikReportGenerator.GenerateReport(new Employee());


            IReportGenerator2 reportGenerator = new TelerikReportGenerator();
            //IReportGenerator2 als Basis wird nur die GenerateReport Methode angeboten. 
            reportGenerator.GenerateReport(new Employee());

        }
    }
    #endregion



}