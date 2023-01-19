using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebAPI_with_EFCore_Relationen_LazyLoading.DTO;
using WebAPI_with_EFCore_Relationen_LazyLoading.DTO.Mapper;
using WebAPI_with_EFCore_Relationen_LazyLoading.Models;

namespace WebAPI_Grundlagen.DTO.Mapper
{
    public static class CountryMapper
    {
        public static CountryDTO ToDTO(this Country country)
        {
            CountryDTO countryDTO = new CountryDTO() { Id = country.Id, Name = country.Name, Population = country.Population, Capitol = country.Capitol, ContinentId = country.ContinentId };

            if (country.ContinetRef != null)
            {
                countryDTO.ContinentDTO = new ContinentDTO()
                {
                    Id = country.ContinetRef.Id,
                    Name = country.ContinetRef.Name,
                };
            }

            return countryDTO;
        }

        public static IList<CountryDTO> ToDTOs(this IList<Country> countries)
        {
            IList<CountryDTO> countryDTOs = new List<CountryDTO>();

            foreach (Country country in countries)
            {
                countryDTOs.Add(ToDTO(country));
            }

            return countryDTOs;
        }


        public static Country ToEntity(this CountryDTO country)
        {
            return new Country() { Id = country.Id, Population = country.Population, Name = country.Name, Capitol = country.Capitol, ContinentId = country.ContinentId };
        }

        public static IList<Country> ToEntities (this IList<CountryDTO> countries)
        {
            IList<Country> entities = new List<Country>();

            foreach (CountryDTO country in countries)
            {
                entities.Add(ToEntity(country));
            }
            return entities;    
        }
    }
}
