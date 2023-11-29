using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidMovement : MonoBehaviour
    {
        private Rigidbody2D m_rigidbody2D;

        public void Initialize(float initialSpeed)
        {
            Vector2 initialVelocity = Random.insideUnitCircle * initialSpeed;
            if (m_rigidbody2D == null)
                m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_rigidbody2D.velocity = initialVelocity;
        }
    }
}
