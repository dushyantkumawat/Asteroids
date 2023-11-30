using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public event Action<int> OnLivesChanged;
        public event Action OnGameOver;

        private AsteroidManager asteroidManager;
        private GameSettingsSO gameSettings;
        private PlayerController playerController;
        private UIManager uiManager;

        private int currentAsteroids = 0;
        private int playerLives;
        #endregion

        [Inject]
        public void Construct(AsteroidManager asteroidManager, GameSettingsSO gameSettings, PlayerController playerController, UIManager uiManager)
        {
            this.asteroidManager = asteroidManager;
            this.gameSettings = gameSettings;
            this.playerController = playerController;
            this.uiManager = uiManager;
        }

        #region Monobehaviour
        private void Awake()
        {
            uiManager.OnStartGame += StartGame;
            uiManager.OnRestartGame += StartGame;
        }

        private void OnDestroy()
        {
            uiManager.OnStartGame -= StartGame;
            uiManager.OnRestartGame -= StartGame;
        }
        #endregion

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
            OnGameOver?.Invoke();
        }

        private void OnPlayerHit()
        {
            playerLives--;
            OnLivesChanged?.Invoke(playerLives);
            if (playerLives == 0)
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
