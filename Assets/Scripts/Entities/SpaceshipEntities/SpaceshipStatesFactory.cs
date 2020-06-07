using Asteroids.Datas;
using Asteroids.Entities.ShipModules;
using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Factory to instantiate SpaceshipStates and inject on it its modules (dependencies).
    /// I´m still working on the SpaceshipStatesFactory, this is just a first aproach.
    /// </summary>
    public class SpaceshipStatesFactory
    {
        public static SpaceshipState CreateSpaceshipState(SpaceshipData spaceshipData)
        {
            SpaceshipState spaceshipState = Object.Instantiate(spaceshipData.SpaceshipPref);
            spaceshipState.name = "SpaceshipState";

            GameObject spaceshipModel = Object.Instantiate(spaceshipData.SpaceshipModel);
            AddModelToSpaceship(spaceshipState, spaceshipModel);

            GameControls.ISpaceshipActions inputModule = CreateInputModule();
            IBasicModule thrusterModule = CreateThrusterModule(spaceshipState, spaceshipModel, spaceshipData.ThrusterData);
            IResetModule gunsModule = CreateGunsModule(spaceshipState, spaceshipData.GunsData);
            IResetModule animModule = CreateAnimatorModule(spaceshipModel, spaceshipData.AnimData);

            spaceshipState.InjectDependencies(thrusterModule, gunsModule, animModule, inputModule, spaceshipModel);

            return spaceshipState;
        }

        private static IResetModule CreateAnimatorModule(GameObject spaceshipModel, AnimModuleData animData)
        {
            Animator animator = spaceshipModel.GetComponent<Animator>();
            AnimModule animModule = new AnimModule(animData, animator);

            return animModule;
        }

        private static IResetModule CreateGunsModule(SpaceshipState spaceshipState, GunsModuleData gunsData)
        {
            GunsStateModule gunsModule = new GunsStateModule(gunsData.MainGunData, spaceshipState.MainGunTr,
                                                                        gunsData.SpecialGunData, spaceshipState.SpecialGunTr,
                                                                        spaceshipState.Rigidbody);

            return gunsModule;
        }

        private static IBasicModule CreateThrusterModule(SpaceshipState spaceshipState, GameObject spaceshipModel, ThrusterModuleData thrusterData)
        {
            ParticleSystem thruster = spaceshipModel.GetComponentInChildren<ParticleSystem>();

            ThrusterStateModule thrusterModule = new ThrusterStateModule(thrusterData, spaceshipState.Rigidbody, thruster);
            return thrusterModule;
        }

        private static GameControls.ISpaceshipActions CreateInputModule()
        {
            InputStateModule inputModule = new InputStateModule();
            return inputModule;
        }

        private static void AddModelToSpaceship(SpaceshipState spaceshipState, GameObject spaceshipModel)
        {
            Transform modelTr = spaceshipModel.transform;

            modelTr.SetParent(spaceshipState.transform);
            modelTr.localPosition = Vector3.zero;
            modelTr.localRotation = Quaternion.identity;
        }
    }
}