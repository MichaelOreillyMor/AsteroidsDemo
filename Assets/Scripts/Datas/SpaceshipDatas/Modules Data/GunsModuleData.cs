using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New GunsData", menuName = "Data/GunsData")]
    public class GunsModuleData : ScriptableObject
    {
        public BaseGunData MainGunData;
        public SpecialGunData SpecialGunData;
    }
}