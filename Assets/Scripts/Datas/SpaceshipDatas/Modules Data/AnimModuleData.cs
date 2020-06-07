using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New AnimData", menuName = "Data/Modules/AnimModuleData")]
    public class AnimModuleData : ScriptableObject
    {
        public float AnimMoveSpeed;

        public float RotThreshold;

        public float RotLeft;
        public float RotRight;

        public float Idle;

        public float Forward;
    }
}

