using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPool : MonoMemoryPool<EAsteroidType, IMemoryPool, Asteroid>
    {
    }
}
