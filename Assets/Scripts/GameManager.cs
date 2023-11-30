using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        private AsteroidManager asteroidManager;
        private GameSettingsSO gameSettings;

        private int currentAsteroids = 0;
        private int initialAsteroids = 4;
        private int asteroidCountIncrease = 2;
        private float respawnDelay = 2f;

        [Inject]
        public void Construct(AsteroidManager asteroidManager, GameSettingsSO gameSettings)
        {
            this.asteroidManager = asteroidManager;
            this.gameSettings = gameSettings;
        }

        void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            currentAsteroids = gameSettings.initialAsteroids;
            asteroidManager.SpawnAsteroids(currentAsteroids);
            asteroidManager.WaveComplete += CallNextWave;
        }

        private void CallNextWave()
        {
            currentAsteroids += gameSettings.asteroidCountIncrease;
            asteroidManager.SpawnAsteroids(currentAsteroids);
        }
    }

}
