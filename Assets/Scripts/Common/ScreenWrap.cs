using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class ScreenWrap : MonoBehaviour
    {
        #region Variables
        [Inject]
        private BoundsProvider bounds;
        private Camera mainCamera;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            CheckWrapPosition();
        }
        #endregion

        private void CheckWrapPosition()
        {
            Vector2 worldBounds = bounds.WorldBounds;
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
