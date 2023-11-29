using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class BoundsProvider
    {
        public Vector2 WorldBounds { get;private set; }
        private Camera mainCamera;

        public BoundsProvider()
        {
            mainCamera = Camera.main;
            CalucalateWorldBounds();
        }

        private void CalucalateWorldBounds()
        {
            WorldBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
    }
}
