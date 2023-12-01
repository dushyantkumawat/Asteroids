using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidManager : MonoBehaviour
    {
        public event Action OnWaveComplete;
        public event Action<int> OnAsteroidDestroyed;

        private Asteroid.Factory asteroidFactory;
        private BoundsProvider boundsProvider;
        private GameManager gameManager;
        private List<Asteroid> activeAsteroids = new List<Asteroid>();

        [Inject]
        public void Construct(Asteroid.Factory asteroidFactory, BoundsProvider boundsProvider, GameManager gameManager)
        {
            this.asteroidFactory = asteroidFactory;
            this.boundsProvider = boundsProvider;
            this.gameManager = gameManager;
        }

        private void Awake()
        {
            gameManager.OnGameOver += ResetAsteroids;
        }

        private void OnDestroy()
        {
            gameManager.OnGameOver -= ResetAsteroids;
        }

        public void SpawnAsteroids(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            SpawnAsteroid(EAsteroidType.Large, GetRandomPosition());
        }

        private void SpawnAsteroid(EAsteroidType type, Vector2 position)
        {
            Asteroid asteroid = asteroidFactory.Create(type);
            asteroid.transform.position = position;
            asteroid.OnAsteroidDestroyed += HandleAsteroidDestroyed;
            activeAsteroids.Add(asteroid);
        }

        private void HandleAsteroidDestroyed(Asteroid asteroid, EAsteroidType asteroidType, Vector3 position)
        {
            asteroid.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
            OnAsteroidDestroyed?.Invoke(asteroid.Score);
            if (asteroidType != EAsteroidType.Small)
            {
                SpawnSmallerAsteroids(asteroidType, position);
            }
            activeAsteroids.Remove(asteroid);
            if (activeAsteroids.Count == 0)
            {
                OnWaveComplete?.Invoke();
            }
        }

        private void SpawnSmallerAsteroids(EAsteroidType type, Vector3 position)
        {
            switch (type)
            {
                case EAsteroidType.Large:
                    SpawnSplitAsteroids(EAsteroidType.Medium, position);
                    break;
                case EAsteroidType.Medium:
                    SpawnSplitAsteroids(EAsteroidType.Small, position);
                    break;
            }
        }

        private void SpawnSplitAsteroids(EAsteroidType type, Vector3 position)
        {
            Vector3 random = Random.insideUnitSphere;
            SpawnAsteroid(type, position + random);
            SpawnAsteroid(type, position - random);
        }

        private void ResetAsteroids()
        {
            for(int i=0; i< activeAsteroids.Count; i++)
            {
                activeAsteroids[i].OnAsteroidDestroyed -= HandleAsteroidDestroyed;
                activeAsteroids[i].Despawn();
            }
            activeAsteroids.Clear();
        }

        private Vector2 GetRandomPosition()
        {
            Vector2 pos = boundsProvider.WorldBounds;

            float f = Random.Range(0, 1f);
            if (f < 0.5f)
            {
                if (f < 0.25f)
                    pos.x = -pos.x;
                pos.y = Random.Range(-pos.y, pos.y);
            }
            else
            {
                if (f < 0.75f)
                    pos.y = -pos.y;
                pos.x = Random.Range(-pos.x, pos.x);
            }
            return pos;
        }
    }

}
