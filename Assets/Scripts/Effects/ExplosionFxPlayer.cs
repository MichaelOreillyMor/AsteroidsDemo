using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Asteroids.Effects
{
    public class ExplosionFxPlayer : ParticlesFxPlayer
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private Light lightEmission;

        [SerializeField]
        private AnimationCurve lightIntesity;

        [SerializeField]
        private Volume postProcessVolume;

        [SerializeField]
        private AnimationCurve chromaticAbIntesity;

        [SerializeField]
        private AnimationCurve lensDistIntesity;

        private ChromaticAberration chromaticAberration;
        private LensDistortion lensDistortion;

        private void Awake()
        {
            postProcessVolume.profile.TryGet(out chromaticAberration);
            postProcessVolume.profile.TryGet(out lensDistortion);
        }

        protected override IEnumerator PlayFXs()
        {
            float time = 0;
            float step = 0;

            audioSource.Play();
            PlayParticleSystems();

            while (time < delayDespawn)
            {
                step = time / delayDespawn;

                chromaticAberration.intensity.Override(chromaticAbIntesity.Evaluate(step));
                lensDistortion.intensity.Override(lensDistIntesity.Evaluate(step));
                lightEmission.intensity = lightIntesity.Evaluate(step);

                time += Time.deltaTime;
                yield return null;
            }

            SimplePool.Despawn(this);
        }
    }
}