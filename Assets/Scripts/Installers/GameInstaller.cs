using Asteroids;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerShipDataSO ShipData;
    [SerializeField] private AsteroidDataSO AsteroidData;
    [SerializeField] private Asteroid AsteroidPrefab;
    [SerializeField] private AsteroidInitializer AsteroidInitializer;
    [SerializeField] private AsteroidManager AsteroidManager;

    public override void InstallBindings()
    {
        Container.BindInstance(ShipData).AsSingle();
        Container.BindInstance(AsteroidData).AsSingle();
        Container.BindInstance(AsteroidInitializer).AsSingle();
        Container.BindInstance(AsteroidManager).AsSingle();
        Container.BindInstance(new BoundsProvider()).AsSingle();

        Container.BindFactory<EAsteroidType, Asteroid, Asteroid.Factory>()
            .FromPoolableMemoryPool<EAsteroidType, Asteroid, AsteroidPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(AsteroidPrefab)
                    .UnderTransformGroup("Asteroids"));
    }
}