using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public event Action OnStartGame;
        public event Action OnRestartGame;

        [Header("Start Screen")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private Button startGameButton;

        [Header("Gameplay Screen")]
        [SerializeField] private GameObject gamePlayScreen;
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Game Over Screen")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private Button restartButton;

        [Inject] private ScoreManager scoreManager;
        [Inject] private GameManager gameManager;
        #endregion

        #region Monobehaviour
        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGameClicked);
            restartButton.onClick.AddListener(RestartGameClicked);
            scoreManager.OnScoreChanged += OnScoreChanged;
            gameManager.OnLivesChanged += OnLivesChanged;
            gameManager.OnGameOver += OnGameOver;
        }


        private void OnDestroy()
        {
            startGameButton.onClick.RemoveListener(StartGameClicked);
            restartButton.onClick.RemoveListener(RestartGameClicked);
            scoreManager.OnScoreChanged -= OnScoreChanged;
            gameManager.OnLivesChanged -= OnLivesChanged;
        }
        #endregion

        #region Button Callbacks
        private void StartGameClicked()
        {
            ActivateGameUI();
            startScreen.SetActive(false);
        }

        private void RestartGameClicked()
        {
            ActivateGameUI();
            gameOverScreen.SetActive(false);
        }
        #endregion

        #region Functionality
        private void SetLives(int lives)
        {
            livesText.SetText(lives.ToString());
        }
        private void SetScore(int lives)
        {
            livesText.SetText(lives.ToString());
        }

        private void ActivateGameUI()
        {
            ResetUIState();
            OnStartGame?.Invoke();
            gamePlayScreen.SetActive(true);
        }

        private void ActivateGameOverUI()
        {
            gamePlayScreen.SetActive(false);
            gameOverScreen.SetActive(true);
            finalScoreText.SetText(scoreText.text);
        }

        private void ResetUIState()
        {
            livesText.SetText(string.Empty);
            scoreText.SetText("0");
            finalScoreText.SetText("0");
        }
        #endregion

        #region Event Listeners
        private void OnScoreChanged(int score)
        {
            SetScore(score);
        }

        private void OnLivesChanged(int lives)
        {
            SetLives(lives);
        }

        private void OnGameOver()
        {
            ActivateGameOverUI();
        }
        #endregion
    }
}
