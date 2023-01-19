using WebAPI_with_Autofac.Interface;

namespace WebAPI_with_Autofac.Implementation
{
    public class PersonBusiness : IPersonBusiness
    {
        public List<string> GetPersonList()
        {
            List<string> personList = new List<string>();
            personList.Add("ppedv AG");
            personList.Add("Kevin Winter");
            return personList;
        }
    }
}
