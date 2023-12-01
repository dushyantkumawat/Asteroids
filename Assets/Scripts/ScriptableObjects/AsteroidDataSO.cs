using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName ="AsteroidData",menuName = "Scriptable Objects/Asteroid Data")]
    public class AsteroidDataSO : ScriptableObject
    {
        public List<AsteroidData> data;

        public AsteroidData GetDataForType(EAsteroidType asteroidType)
        {
            int index = data.FindIndex(x => x.asteroidType == asteroidType);
            if(index >= 0)
            {
                return data[index];
            }
            else
            {
                Debug.LogError($"Failed to get data for type {asteroidType}");
                return new AsteroidData();
            }
        }
    }

    [Serializable]
    public class AsteroidData
    {
        public EAsteroidType asteroidType;
        public float velocity;
        public int score;
    }
}
