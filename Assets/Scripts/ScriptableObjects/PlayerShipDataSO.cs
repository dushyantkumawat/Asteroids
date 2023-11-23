using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "PlayerShipData", menuName = "Scriptable Objects/Player Ship Data")]
    public class PlayerShipDataSO : ScriptableObject
    {
        public float rotationSensitivity = 2f;
        public float thrustForce = 10f;
        public float fireSpeed = 1f;
        public float maxVelocity = 5f;
        public int maxLives = 5;
    }
}
