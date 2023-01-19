namespace WebAPI_with_EFCore_Relationen_LazyLoading.DTO
{
    public class ContinentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<CountryDTO> Countries { get; set; }

    }
}
