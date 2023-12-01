using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class Bullet : MonoBehaviour, IPoolable<float, Vector2, Vector2, IMemoryPool>
    {
        private IMemoryPool pool;
        private bool isActive;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isActive) return;
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
            isActive = false;
        }

        public void OnSpawned(float despawnDelay, Vector2 pos, Vector2 speed, IMemoryPool pool)
        {
            isActive = true;
            this.pool = pool;
            transform.position = pos;
            GetComponent<Rigidbody2D>().velocity = speed;
            Invoke(nameof(DespawnAfterDelay), despawnDelay);
        }

        private void DespawnAfterDelay()
        {
            if(isActive)
                pool.Despawn(this);
        }

        public class Factory: PlaceholderFactory<float, Vector2, Vector2, Bullet> { }
    }

    public interface IHitTarget
    {
        public void OnHit();
    }
}
