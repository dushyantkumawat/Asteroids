using Asteroids;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerShipDataSO ShipData;

    public override void InstallBindings()
    {
        Container.BindInstance(ShipData).AsSingle();
    }
}