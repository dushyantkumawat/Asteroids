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
        private PlayerController playerController;

        private int currentAsteroids = 0;
        private int playerLives;

        [Inject]
        public void Construct(AsteroidManager asteroidManager, GameSettingsSO gameSettings, PlayerController playerController)
        {
            this.asteroidManager = asteroidManager;
            this.gameSettings = gameSettings;
            this.playerController = playerController;
        }

        void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            currentAsteroids = gameSettings.initialAsteroids;
            playerLives = gameSettings.playerMaxLives;
            asteroidManager.SpawnAsteroids(currentAsteroids);
            asteroidManager.WaveComplete += CallNextWave;
            playerController.PlayerHit += OnPlayerHit;
            playerController.ResetShip();
        }

        private void GameOver()
        {
            asteroidManager.WaveComplete -= CallNextWave;
            playerController.PlayerHit -= OnPlayerHit;
        }

        private void OnPlayerHit()
        {
            playerLives--;
            if(playerLives == 0)
            {
                GameOver();
            }
            else
            {
                Invoke(nameof(RespawnPlayer), gameSettings.playerRespawnDelay);
            }
        }
        
        private void RespawnPlayer()
        {
            playerController.ResetShip();
        }

        private void CallNextWave()
        {
            currentAsteroids += gameSettings.asteroidCountIncrease;
            asteroidManager.SpawnAsteroids(currentAsteroids);
        }
    }

}
