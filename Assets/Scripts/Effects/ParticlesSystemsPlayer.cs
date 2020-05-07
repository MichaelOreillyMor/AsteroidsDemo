using Asteroids.Utilities.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Effects
{
    public class ParticlesSystemsPlayer : PoolMember
    {
        [SerializeField]
        protected float duration;

        [SerializeField]
        protected ParticleSystem[] particleSystems;

        public void Play()
        {
            StartCoroutine(PlayFXs());
        }

        protected virtual IEnumerator PlayFXs()
        {
            PlayParticleSystems();
            yield return new WaitForSeconds(duration);
            SimplePool.Despawn(this);
        }

        protected void PlayParticleSystems()
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
        }
    }
}