using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPool : MonoPoolableMemoryPool<EAsteroidType, IMemoryPool, Asteroid>
    {
    }
}
