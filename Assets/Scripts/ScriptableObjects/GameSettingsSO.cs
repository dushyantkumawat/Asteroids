using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {
        public int initialAsteroids;
        public int asteroidCountIncrease;
        public int playerMaxLives;
        public int playerRespawnDelay;
    }
}
