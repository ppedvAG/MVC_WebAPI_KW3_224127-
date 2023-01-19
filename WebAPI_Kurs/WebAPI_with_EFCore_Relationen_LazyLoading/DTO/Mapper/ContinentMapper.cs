using WebAPI_Grundlagen.DTO.Mapper;
using WebAPI_with_EFCore_Relationen_LazyLoading.Models;

namespace WebAPI_with_EFCore_Relationen_LazyLoading.DTO.Mapper
{
    public static class ContinentMapper
    {
        public static ContinentDTO ToDTO (this Continent continent)
        {

            ContinentDTO continentDTO = new ContinentDTO { Id = continent.Id, Name = continent.Name };

            if (continent.Countries != null)
            {
                continentDTO.Countries = new List<CountryDTO>();
                continentDTO.Countries = continent.Countries.ToDTOs();
            }
            return continentDTO;
        }

        public static IList<ContinentDTO> ToDTOs(this IList<Continent> continents)
        {
            IList<ContinentDTO> result = new List<ContinentDTO>();

            foreach (Continent continent in continents)
            {
                result.Add(ToDTO(continent));
            }

            return result;
        }

        public static Continent ToEntity(this ContinentDTO continentDTO)
        {
            return new Continent { Id = continentDTO.Id, Name = continentDTO.Name };
        }

        public static IList<Continent> ToEntities(this IList<ContinentDTO> continentDTOs)
        {
            IList<Continent> continents = new List<Continent>();

            foreach (ContinentDTO currentDTO in continentDTOs)
                continents.Add(ToEntity(currentDTO));

            return continents;
        }
    }
}
