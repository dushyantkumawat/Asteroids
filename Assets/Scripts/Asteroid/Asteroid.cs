using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour, IPoolable<EAsteroidType, IMemoryPool>, IHitTarget
    {
        #region Variables
        public event Action<Asteroid, EAsteroidType, Vector3> OnAsteroidDestroyed;

        public int Score => score;

        [SerializeField] private List<AsteroidBody> bodyList;
        private EAsteroidType asteroidType;
        private IMemoryPool pool;
        private int score;
        #endregion

        public void Init(EAsteroidType asteroidType, int score)
        {
            this.asteroidType = asteroidType;
            this.score = score;
            foreach (AsteroidBody body in bodyList)
            {
                body.gameObject.SetActive(asteroidType == body.asteroidType);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            IHitTarget target = collision.collider.GetComponentInParent<IHitTarget>();
            if (target != null)
            {
                OnHit();
                target.OnHit();
            }
        }

        public void OnHit()
        {
            OnAsteroidDestroyed?.Invoke(this, asteroidType, transform.position);
            pool.Despawn(this);
        }

        public void OnDespawned()
        {
        }

        public void OnSpawned(EAsteroidType p1, IMemoryPool p2)
        {
            pool = p2;
        }

        public void Despawn()
        {
            pool.Despawn(this);
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
