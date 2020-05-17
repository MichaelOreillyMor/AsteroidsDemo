using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New ShotgunData", menuName = "Data/GunDatas/ShotgunData")]
    public class ShotgunData : SpecialGunData
    {
        public int RocketsPerShot;

        [Range(0, 360)]
        public int ShotArc;
    }
}
