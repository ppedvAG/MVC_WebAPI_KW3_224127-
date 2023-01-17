namespace MVC_Einstieg.Models
{
    public class Person
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }


        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Email = default!;
        }
    }
}
