using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New BaseGunData", menuName = "Data/GunDatas/BaseGunData")]
    public class BaseGunData : ScriptableObject
    {
        public int PreloadRocketPrefs;

        public float ReloadTime;

        public RocketData RocketData;
    }
}
