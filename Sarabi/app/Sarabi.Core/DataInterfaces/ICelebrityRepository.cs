namespace Sarabi.Core.DataInterfaces
{
    public interface ICelebrityRepository
    {
        Celebrity GetCelebrityByName(string name);
    }
}