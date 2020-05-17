using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    public abstract class SpecialGunState : BaseGunState
    {
        protected int maxAmmunition;
        protected int currentAmmunition;

        protected void StartListeningAddAmmunitionEvent()
        {
            Messenger<AsteroidDestroyedMessage>.AddListener("OnAsteroidDestroyed", AddAmunition);
        }

        protected void StopListeningAddAmmunitionEvent()
        {
            Messenger<AsteroidDestroyedMessage>.RemoveListener("OnAsteroidDestroyed", AddAmunition);
        }

        protected void AddAmunition(AsteroidDestroyedMessage  asteroidDestroyedMessage)
        {
            //TO-DO: if distance to gun < X add + 2
            if (currentAmmunition < maxAmmunition && !asteroidDestroyedMessage.ShotID.Contains(gunName))            
                currentAmmunition++;
            
        }
    }
}