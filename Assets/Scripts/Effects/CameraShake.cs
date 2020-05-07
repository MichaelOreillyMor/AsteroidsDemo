using UnityEngine;
using System.Collections;

namespace Asteroids.Effects
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        private const float MIN_SPEED = 1f;
        private const float DIST_EQUAL = 0.01f;

        private bool isRunning;

        private float duration;
        private float magnitude;
        private float speed;

        private float initMagnitude;
        private float initSpeed;
        private float decreaseFactor;
        private bool isDesirePos;

        private float elapse;

        private Vector3 originalPos;
        private Vector3 desirePos;

        public void Setup()
        {
            originalPos = transform.position;
        }

        public void Shake(float duration, float magnitude, float speed)
        {
            this.duration += duration;
            this.magnitude += magnitude;
            this.speed = (speed > this.speed) ? speed : this.speed;

            initMagnitude = magnitude;
            initSpeed = speed;

            if (!isRunning)
                StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            isRunning = true;
            isDesirePos = false;
            desirePos = originalPos;

            while (elapse < duration || !isDesirePos)
            {
                if (elapse < duration && isDesirePos)
                    desirePos = originalPos + GetRandShake();

                transform.position = Vector3.Lerp(transform.position, desirePos, Time.deltaTime * speed);
                isDesirePos = Vector3.SqrMagnitude(desirePos - transform.position) < DIST_EQUAL;

                DecreaseShake();

                elapse += Time.deltaTime;
                yield return null;
            }

            elapse = 0;
            duration = 0;
            magnitude = 0;
            speed = 0;

            transform.position = originalPos;
            isRunning = false;
        }

        private void DecreaseShake()
        {
            decreaseFactor = 1 - (elapse / duration);
            magnitude = initMagnitude * decreaseFactor;
            speed = initSpeed * decreaseFactor;

            if (speed < MIN_SPEED)
                speed = MIN_SPEED;
        }

        private Vector3 GetRandShake()
        {
            Vector3 randAmountShake = Vector3.zero;
            randAmountShake.x = Random.Range(-1, 1) * magnitude;
            randAmountShake.z = Random.Range(-1, 1) * magnitude;

            return randAmountShake;
        }
    }
}