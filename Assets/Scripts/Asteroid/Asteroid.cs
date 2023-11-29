using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour, IPoolable<EAsteroidType, IMemoryPool>, IDisposable
    {
        #region Variables
        public event Action<Asteroid, EAsteroidType, Vector3> OnAsteroidDestroyed;

        [SerializeField] private List<AsteroidBody> bodyList;
        private EAsteroidType asteroidType;
        private IMemoryPool pool;
        #endregion

        public void Init(EAsteroidType asteroidType)
        {
            this.asteroidType = asteroidType;
            foreach(AsteroidBody body in bodyList)
            {
                body.gameObject.SetActive(asteroidType == body.asteroidType);
            }
        }

        private void OnDestroy()
        {
            OnAsteroidDestroyed?.Invoke(this, asteroidType, transform.position);
        }

        public void OnDespawned()
        {
            pool.Despawn(this);
        }

        public void OnSpawned(EAsteroidType p1, IMemoryPool p2)
        {
            pool = p2;
        }

        public void Dispose()
        {
        }

        [Serializable]
        struct AsteroidBody
        {
            public EAsteroidType asteroidType;
            public GameObject gameObject;
        }

        public class Factory : PlaceholderFactory<EAsteroidType, Asteroid>
        {
            private AsteroidInitializer asteroidInitializer;

            public Factory(AsteroidInitializer asteroidInitializer)
            {
                this.asteroidInitializer = asteroidInitializer;
            }

            public override Asteroid Create(EAsteroidType type)
            {
                Asteroid asteroid = base.Create(type);
                asteroidInitializer.InitializeAsteroid(asteroid, type);
                return asteroid;
            }
        }
    }

    public enum EAsteroidType
    {
        Large,
        Medium,
        Small
    }
}
