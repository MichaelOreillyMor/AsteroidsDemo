using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Asteroids.Effects
{
    public class ExplosionPlayer : ParticlesSystemsPlayer
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private Light lightEmission;

        [SerializeField]
        private AnimationCurve lightIntesity;

        [SerializeField]
        private PostProcessVolume postProcessVolume;

        [SerializeField]
        private AnimationCurve chromaticAbIntesity;

        [SerializeField]
        private AnimationCurve lensDistIntesity;

        [SerializeField]
        private AnimationCurve bloomIntesity;

        private ChromaticAberration chromaticAberration;
        private LensDistortion lensDistortion;
        private Bloom bloom;

        private void Awake()
        {
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessVolume.profile.TryGetSettings(out lensDistortion);
            postProcessVolume.profile.TryGetSettings(out bloom);
        }

        protected override IEnumerator PlayFXs()
        {
            float time = 0;
            float step = 0;

            audioSource.Play();
            PlayParticleSystems();

            while (time < duration)
            {
                step = time / duration;

                chromaticAberration.intensity.value = chromaticAbIntesity.Evaluate(step);
                lensDistortion.intensity.value = lensDistIntesity.Evaluate(step);
                bloom.intensity.value = bloomIntesity.Evaluate(step);

                lightEmission.intensity = lightIntesity.Evaluate(step);

                time += Time.deltaTime;
                yield return null;
            }

            SimplePool.Despawn(this);
        }
    }
}