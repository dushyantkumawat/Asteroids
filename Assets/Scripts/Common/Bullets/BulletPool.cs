using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class BulletPool : MonoPoolableMemoryPool<float, Vector2, Vector2, IMemoryPool, Bullet>
    {
    }
}
