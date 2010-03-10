using Sarabi.Core;
using Sarabi.Core.DataInterfaces;
using SharpArch.Data.NHibernate;

namespace Sarabi.Data
{
    public class CelebrityRepository : Repository<Celebrity>, ICelebrityRepository
    {
        public Celebrity GetCelebrityByName(string name)
        {
            return null;
        }
    }
}