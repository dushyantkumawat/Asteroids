using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ScreenWrap : MonoBehaviour
    {
        #region Variables
        private Camera mainCamera;
        private Vector2 worldBounds;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            CalucalateWorldBounds();
        }

        private void Update()
        {
            CheckWrapPosition();
        }
        #endregion

        private void CalucalateWorldBounds()
        {
            worldBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }

        private void CheckWrapPosition()
        {
            Vector3 pos = transform.position;
            if(pos.x < -worldBounds.x)
            {
                pos.x = worldBounds.x;
            }
            else if(pos.x > worldBounds.x)
            {
                pos.x = -worldBounds.x;
            }
            if (pos.y < -worldBounds.y)
            {
                pos.y = worldBounds.y;
            }
            else if (pos.y > worldBounds.y)
            {
                pos.y = -worldBounds.y;
            }
            transform.position = pos;
        }
    }
}
