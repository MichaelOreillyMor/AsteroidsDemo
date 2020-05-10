using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
        private Volume postProcessVolume;

        [SerializeField]
        private AnimationCurve chromaticAbIntesity;

        [SerializeField]
        private AnimationCurve lensDistIntesity;

        [SerializeField]
        private AnimationCurve bloomIntesity;

        private ChromaticAberration chromaticAberration;
        private LensDistortion lensDistortion;
        private Bloom bloom;

        private float chromaticAberrationValue;
        private float lensDistortionValue;
        private float bloomValue;

        private void Awake()
        {
            postProcessVolume.profile.TryGet(out chromaticAberration);
            postProcessVolume.profile.TryGet(out lensDistortion);
            postProcessVolume.profile.TryGet(out bloom);

            chromaticAberrationValue = chromaticAberration.intensity.value;
            lensDistortionValue = lensDistortion.intensity.value;
            bloomValue = bloom.intensity.value;
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

                //chromaticAberration.intensity.value = chromaticAbIntesity.Evaluate(step);
                //lensDistortion.intensity.value = lensDistIntesity.Evaluate(step);
                //bloom.intensity.value = bloomIntesity.Evaluate(step);
                bloom.intensity.Override(bloomIntesity.Evaluate(step));
                lightEmission.intensity = lightIntesity.Evaluate(step);
                Debug.Log("step: " + step + " bloom: " + bloomIntesity.Evaluate(step));
                time += Time.deltaTime;
 
                yield return null;
            }

            SimplePool.Despawn(this);
        }
    }
}