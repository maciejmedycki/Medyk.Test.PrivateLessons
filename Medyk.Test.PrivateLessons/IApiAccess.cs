namespace Medyk.Test.PrivateLessons
{
    public interface IApiAccess
    {
        object GetData(int id);

        void SendData(object data);
    }
}