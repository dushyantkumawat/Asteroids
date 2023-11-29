using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        private Asteroid.Factory asteroidFactory;
        private AsteroidManager asteroidManager;

        private int currentAsteroids = 0;
        private int initialAsteroids = 4;
        private int asteroidCountIncrease = 2;
        private float respawnDelay = 2f;

        [Inject]
        public void Construct(Asteroid.Factory asteroidFactory, AsteroidManager asteroidManager)
        {
            this.asteroidFactory = asteroidFactory;
            this.asteroidManager = asteroidManager;
        }

        void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            currentAsteroids = initialAsteroids;
            CallNextWave();
        }

        private void CallNextWave()
        {
            asteroidManager.SpawnAsteroids(currentAsteroids);
        }
    }

}
