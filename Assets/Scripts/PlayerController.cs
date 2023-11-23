using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [Inject]
        private PlayerShipDataSO shipData;
        private PlayerControls controls;
        private Rigidbody2D m_Rigidbody2d;

        private InputAction thrustAction;
        private InputAction rotateAction;
        private InputAction fireAction;
        #endregion

        #region Monobehavior
        private void Awake()
        {
            m_Rigidbody2d = GetComponent<Rigidbody2D>();
            controls = new PlayerControls();
            thrustAction = controls.Movement.Thrust;
            rotateAction = controls.Movement.Rotate;
            fireAction = controls.Movement.Fire;
        }

        private void OnEnable()
        {
            controls.Movement.Enable();
        }

        private void OnDisable()
        {
            controls.Movement.Disable();
        }

        private void FixedUpdate()
        {
            if(rotateAction.IsPressed())
            {
                float dir = rotateAction.ReadValue<float>();
                Rotate(dir);
            }

            if (thrustAction.IsPressed())
            {
                Thrust();
                LimitVelocity();
            }

        }
        #endregion

        #region Movement
        private void Rotate(float direction)
        {
            m_Rigidbody2d.MoveRotation(m_Rigidbody2d.rotation + direction * shipData.rotationSensitivity * Time.fixedDeltaTime);
        }

        private void Thrust()
        {
            m_Rigidbody2d.AddForce(shipData.thrustForce * Time.fixedDeltaTime * transform.up);
        }

        private void Fire()
        {
            // TODO
        }

        private void LimitVelocity()
        {
            if (m_Rigidbody2d.velocity.magnitude > shipData.maxVelocity)
            {
                m_Rigidbody2d.velocity = m_Rigidbody2d.velocity.normalized * shipData.maxVelocity;
            }
        }
        #endregion

    }
}
