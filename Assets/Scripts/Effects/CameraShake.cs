using UnityEngine;
using System.Collections;

namespace Asteroids.Effects
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        private const float MIN_SPEED = 1f;
        private const float DIST_EQUAL = 0.01f;
        private const float RETURN_ORIGIN_DURATION = 0.25f;

        private bool isRunning;
        private bool isDesirePos;

        private float duration;
        private float elapse;

        private float magnitude;
        private float initMagnitude;

        private float speed;
        private float initSpeed;

        private float decreaseFactor;

        private Vector3 originalPos;
        private Vector3 desirePos;

        public void Setup()
        {
            originalPos = transform.position;
        }

        public void Shake(float duration, float magnitude, float speed)
        {
            this.duration += duration;

            if (speed > initSpeed)
                initSpeed = speed;

            if (magnitude > initMagnitude)
                initMagnitude = magnitude;

            if (!isRunning)
                StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            isRunning = true;
            isDesirePos = false;
            desirePos = originalPos;

            while (elapse < duration)
            {
                if (isDesirePos)
                    desirePos = originalPos + GetRandShake();

                SetShakeValues(); 

                transform.position = Vector3.Lerp(transform.position, desirePos, Time.deltaTime * speed);
                isDesirePos = Vector3.SqrMagnitude(desirePos - transform.position) < DIST_EQUAL;

                elapse += Time.deltaTime;
                yield return null;
            }

            elapse = 0;
            duration = 0;
            initMagnitude = 0;
            initSpeed = 0;

            yield return StartCoroutine(ReturnToOriginPos());
            isRunning = false;
        }

        private void SetShakeValues()
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

        private IEnumerator ReturnToOriginPos()
        {
            Vector3 startPos = transform.position;

            float step = 0f;
            float time = 0f;

            while (step < RETURN_ORIGIN_DURATION)
            {
                time += Time.deltaTime;
                step = time / RETURN_ORIGIN_DURATION;
                transform.position = Vector3.Lerp(startPos, originalPos, step);
                yield return null;
            }
        }
    }
}