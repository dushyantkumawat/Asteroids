using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IHitTarget
    {
        #region Variables
        public Action PlayerHit;

        [SerializeField]
        private GameObject visuals;
        [SerializeField]
        private Transform firePoint;

        [Inject]
        private Bullet.Factory bulletFactory;
        [Inject]
        private PlayerShipDataSO shipData;

        private PlayerControls controls;
        private Rigidbody2D m_Rigidbody2d;
        private Collider2D collider2d;

        private InputAction thrustAction;
        private InputAction rotateAction;
        private InputAction fireAction;

        private bool movementActive;
        private float timeDiffBetweenShots;
        private float lastFireTime;
        private Vector2 startPos;
        #endregion

        #region Monobehavior
        private void Awake()
        {
            SetupInput();
            m_Rigidbody2d = GetComponent<Rigidbody2D>();
            collider2d = GetComponentInChildren<Collider2D>();
            timeDiffBetweenShots = 1f / shipData.fireSpeed;
            movementActive = true;
            startPos = transform.position;
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
            if (!movementActive) return;

            ReadInput();
        }
        #endregion

        #region Input Handling
        private void SetupInput()
        {
            controls = new PlayerControls();
            thrustAction = controls.Movement.Thrust;
            rotateAction = controls.Movement.Rotate;
            fireAction = controls.Movement.Fire;
        }

        private void ReadInput()
        {
            if (rotateAction.IsPressed())
            {
                float dir = rotateAction.ReadValue<float>();
                Rotate(dir);
            }

            if (thrustAction.phase == InputActionPhase.Started)
            {
                Thrust();
                LimitVelocity();
            }

            if (fireAction.IsPressed())
            {
                Fire();
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

        private void LimitVelocity()
        {
            if (m_Rigidbody2d.velocity.magnitude > shipData.maxVelocity)
            {
                m_Rigidbody2d.velocity = m_Rigidbody2d.velocity.normalized * shipData.maxVelocity;
            }
        }
        #endregion

        #region Firing
        private void Fire()
        {
            if (!CanFire()) return;
            bulletFactory.Create(shipData.bulletLifetime, firePoint.position, transform.up * shipData.bulletSpeed);
            lastFireTime = Time.time;
        }

        private bool CanFire()
        {
            if (lastFireTime + timeDiffBetweenShots > Time.time)
                return false;
            return true;
        }
        #endregion

        #region Ship State Handling
        public void OnHit()
        {
            SetState(false);
            m_Rigidbody2d.velocity = Vector2.zero;
            m_Rigidbody2d.angularVelocity = 0;
            PlayerHit?.Invoke();
        }

        public void ResetShip()
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            SetState(true);
        }

        private void SetState(bool enabled)
        {
            movementActive = enabled;
            collider2d.enabled = enabled;
            visuals.SetActive(enabled);
        }
        #endregion

    }
}
