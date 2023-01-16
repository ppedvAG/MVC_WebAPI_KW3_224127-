namespace SingleResponsibilityPrincipe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class AntiCodeSample_Employee
    {
        public int Id { get; set; }
        public string Vorname { get; set; } 
        public string Nachname { get; set; }    


        public void GenerateEmployeeReport(Employee employee)
        {
            //Erstelle ein Report vom Mitarbeiter
        }

        public void InsertEmployeeToDb(Employee employee)
        {
            //Employee-Datensatz wird in Datenbank gespeichert 
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }

    public class EmployeeReportGenerator
    {
        public void GenerateEmployeeReport(Employee employee)
        {
            //Erstelle ein Report vom Mitarbeiter
        }

        //... 
    }


    //Datenbank Modifikationen stehen im DAL (DataAccess-Layer) 

    //Repository ist ein Design-Pattern. Eine Repository Klasse repräsentiert eine Datenbank-Tabelle. 


    public class EmployeeRepository
    {
        //GetAll / GetById - Methoden

        //Insert
        public void InsertEmployeeToDb(Employee employee)
        {
            //Employee-Datensatz wird in Datenbank gespeichert 
        }

        // Update - Methode

        // Delete - Methode
    }





}