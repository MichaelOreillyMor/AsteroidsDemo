using Asteroids.Utilities.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Effects
{
    public class ParticlesSystemsPlayer : PoolMember
    {
        [SerializeField]
        protected float delayDespawn;

        [SerializeField]
        protected ParticleSystem[] particleSystems;

        public void Play()
        {
            StartCoroutine(PlayFXs());
        }

        protected virtual IEnumerator PlayFXs()
        {
            PlayParticleSystems();
            yield return new WaitForSeconds(delayDespawn);
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