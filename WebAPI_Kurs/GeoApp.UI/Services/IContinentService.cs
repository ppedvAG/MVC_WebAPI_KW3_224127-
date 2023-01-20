using GeoApp.Shared.Entities;

namespace GeoApp.UI.Services
{
    public interface IContinentService
    {
        Task<IList<Continent>> GetAllContinents();

        Task<Continent> GetById(int id);

        Task AddContinent(Continent continent);

        Task UpdateContinent(Continent modifiedContinent);

        Task DeleteContinent(int id);

    }
}
