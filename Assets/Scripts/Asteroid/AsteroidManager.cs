using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidManager : MonoBehaviour
    {
        public Action WaveComplete;

        private Asteroid.Factory asteroidFactory;
        private BoundsProvider boundsProvider;

        private int activeAsteroids;

        [Inject]
        public void Construct(Asteroid.Factory asteroidFactory, BoundsProvider boundsProvider)
        {
            this.asteroidFactory = asteroidFactory;
            this.boundsProvider = boundsProvider;
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
            activeAsteroids++;
        }

        private void HandleAsteroidDestroyed(Asteroid asteroid, EAsteroidType asteroidType, Vector3 position)
        {
            asteroid.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
            if (asteroidType != EAsteroidType.Small)
            {
                SpawnSmallerAsteroids(asteroidType, position);
            }
            activeAsteroids--;
            if (activeAsteroids == 0)
            {
                WaveComplete?.Invoke();
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
