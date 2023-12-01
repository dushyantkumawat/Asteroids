using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidInitializer : MonoBehaviour
    {
        [Inject]
        private AsteroidDataSO asteroidDataContainer;

        public void InitializeAsteroid(Asteroid asteroid, EAsteroidType type)
        {
            AsteroidData asteroidData = asteroidDataContainer.GetDataForType(type);
            asteroid.Init(type, asteroidData.score);
            var movementScript = asteroid.GetComponent<AsteroidMovement>();
            if (movementScript != null)
            {
                movementScript.Initialize(asteroidData.velocity);
            }
        }

    }

}
