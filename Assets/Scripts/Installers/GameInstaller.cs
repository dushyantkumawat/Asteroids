using Asteroids;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private PlayerShipDataSO ShipData;
    [SerializeField] private AsteroidDataSO AsteroidData;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Asteroid AsteroidPrefab;
    [SerializeField] private AsteroidInitializer AsteroidInitializer;
    [SerializeField] private AsteroidManager AsteroidManager;
    [SerializeField] private Bullet PlayerBulletPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings).AsSingle();
        Container.BindInstance(ShipData).AsSingle();
        Container.BindInstance(AsteroidData).AsSingle();
        Container.BindInstance(PlayerController).AsSingle();
        Container.BindInstance(AsteroidInitializer).AsSingle();
        Container.BindInstance(AsteroidManager).AsSingle();
        Container.BindInstance(new BoundsProvider()).AsSingle();

        Container.BindFactory<EAsteroidType, Asteroid, Asteroid.Factory>()
            .FromPoolableMemoryPool<EAsteroidType, Asteroid, AsteroidPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(AsteroidPrefab)
                    .UnderTransformGroup("Asteroids"));

        Container.BindFactory<float, Vector2, Vector2, Bullet, Bullet.Factory>()
            .FromPoolableMemoryPool<float, Vector2, Vector2, Bullet, BulletPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(PlayerBulletPrefab)
                    .UnderTransformGroup("PlayerBullets"));
    }
}