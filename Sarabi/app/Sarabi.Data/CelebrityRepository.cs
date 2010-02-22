using Sarabi.Core;
using SharpArch.Data.NHibernate;

namespace Sarabi.Data
{
    public interface ICelebrityRepository
    {
        Celebrity GetCelebrityByName(string name);
    }

    public class CelebrityRepository : Repository<Celebrity>, ICelebrityRepository
    {
        public Celebrity GetCelebrityByName(string name)
        {
            return null;
        }
    }
}