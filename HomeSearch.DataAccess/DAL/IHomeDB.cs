using HomeSearch.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeSearch.DataAccess.DAL
{
    public interface IHomeDB
    {
        public Task Insert(IList<HomeModel> list);
        public Task<IList<HomeModel>> GetList(HomeQuery query);
        public Task<IList<CityModel>> GetCities();
        public Task<IList<StateModel>> GetStates();
        public Task<IList<PropertyTypeModel>> GetPropertyTypes();
    }
}