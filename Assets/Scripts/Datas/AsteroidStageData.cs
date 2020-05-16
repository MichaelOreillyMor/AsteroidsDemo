﻿using UnityEngine;

namespace Asteroids.Datas
{
    /// <summary>
    /// This is an "Enum" element with additional info
    /// </summary>
    [CreateAssetMenu(fileName = "New AsteroidStageData", menuName = "Data/AsteroidStageData")]
    public class AsteroidStageData : ScriptableObject
    {
        public float InitSpeed;
        public float InitMaxRot;

        public float Scale;
        public float Mass;
    }
}
