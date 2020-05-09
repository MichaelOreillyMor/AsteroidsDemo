using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private Transform asreoidsParent;
#endif

    [SerializeField]
    private Transform cloudsParticlesTr;

    [SerializeField]
    private Transform[] asreoids;

    [SerializeField]
    private float asreoidsMaxSpeed;

    private Vector3[] asreoidsVelocity;

    public void Setup()
    {
        SetCloundsPosition();
        SetAsteroidsVelocity();
    }

    private void SetAsteroidsVelocity()
    {
        asreoidsVelocity = new Vector3[asreoids.Length];
        for (int i = 0; i < asreoids.Length; i++)
        {
            asreoids[i].rotation = Quaternion.Euler(Random.insideUnitSphere * 90f);
            asreoidsVelocity[i] = Random.insideUnitSphere * asreoidsMaxSpeed;
        }
    }

    private void SetCloundsPosition()
    {
        float posY = cloudsParticlesTr.position.y;
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        position.y = posY;

        cloudsParticlesTr.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < asreoids.Length; i++)
        {
            asreoids[i].Rotate(asreoidsVelocity[i] * Time.deltaTime);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (asreoidsParent != null)
        {
            int asteroidsCount = asreoidsParent.childCount;
            asreoids = new Transform[asteroidsCount];

            for (int i = 0; i < asteroidsCount; i++)
            {
                asreoids[i] = asreoidsParent.GetChild(i);
            }
        }
    }
#endif
}
