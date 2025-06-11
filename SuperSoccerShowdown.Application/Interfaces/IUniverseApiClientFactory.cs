namespace SuperSoccerShowdown.Application.Interfaces
{
    public interface IUniverseApiClientFactory
    {
        IUniverseApiClient GetClient(string universe);
    }
}
