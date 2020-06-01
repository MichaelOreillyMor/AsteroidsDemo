namespace Asteroids.Entities.ShipModules
{
    public interface IBasicModule
    {
        void Setup();
        void Unsetup();
        void Update();
    }
}