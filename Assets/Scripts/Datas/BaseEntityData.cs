using Asteroids.Effects;
using UnityEngine;

namespace Asteroids.Datas
{
    public class BaseEntityData : ScriptableObject
    {
        public bool FixPosOnLimits;
        public ParticlesFxPlayer DestroyFXPlayerPref;
    }
}