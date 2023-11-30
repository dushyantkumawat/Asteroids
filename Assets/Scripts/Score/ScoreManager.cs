using System;
using UnityEngine;

namespace Asteroids
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables
        public event Action<int> OnScoreChanged;

        public int Score => score;

        private int score;
        #endregion

        #region Functionality
        public void IncreaseScore(int amount)
        {
            score += amount;
            OnScoreChanged?.Invoke(score);
        }

        public void ResetScore()
        {
            score = 0;
            OnScoreChanged?.Invoke(score);
        }
        #endregion
    }
}
