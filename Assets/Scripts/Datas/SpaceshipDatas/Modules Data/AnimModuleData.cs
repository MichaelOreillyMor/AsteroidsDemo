using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New AnimData", menuName = "Data/Modules/AnimModuleData")]
    public class AnimModuleData : ScriptableObject
    {
        public float AnimMoveSpeed;

        public float AnimThreshold;

        public float AnimLeft;
        public float AnimRight;

        public float AnimIdle;

        public float AnimForward;
    }
}

