using HomeSearch.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeSearch.DataAccess.DAL
{
    public interface IHomeDB
    {
        public Task Insert(IList<HomeModel> list);
        public Task<IList<HomeModel>> GetList(string key);
        //void Update(HomeModel model);
        //void Delete(HomeModel model);
    }
}