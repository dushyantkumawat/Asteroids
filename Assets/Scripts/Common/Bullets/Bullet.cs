using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class Bullet : MonoBehaviour, IPoolable<float, Vector2, Vector2, IMemoryPool>
    {
        private IMemoryPool pool;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IHitTarget target = collision.collider.GetComponentInParent<IHitTarget>();
            if (target != null)
            {
                target.OnHit();
            }
            CancelInvoke(nameof(DespawnAfterDelay));
            pool.Despawn(this);
        }

        public void OnDespawned()
        {
            pool = null;
        }

        public void OnSpawned(float despawnDelay, Vector2 pos, Vector2 speed, IMemoryPool pool)
        {
            this.pool = pool;
            transform.position = pos;
            GetComponent<Rigidbody2D>().velocity = speed;
            Invoke(nameof(DespawnAfterDelay), despawnDelay);
        }

        private void DespawnAfterDelay()
        {
            pool.Despawn(this);
        }

        public class Factory: PlaceholderFactory<float, Vector2, Vector2, Bullet> { }
    }

    public interface IHitTarget
    {
        public void OnHit();
    }
}
