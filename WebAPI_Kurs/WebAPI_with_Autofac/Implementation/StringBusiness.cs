using WebAPI_with_Autofac.Interface;

namespace WebAPI_with_Autofac.Implementation
{
    public class StringBusiness : IStringBusiness
    {
        public string StringToUpper(string personName)
        {
            return personName.ToUpper();
        }
    }
}
