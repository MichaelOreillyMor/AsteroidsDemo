using Asteroids.Input;
using Asteroids.Utilities.Messages;
using UnityEngine.InputSystem;

namespace Asteroids.Entities.ShipModules
{
    public class InputStateModule : GameControls.ISpaceshipActions
    {
        private bool IsShotingMainGun;
        private bool wasShotingMainGun;

        private bool IsShotingSpecialGun;
        private bool wasShotingSpecialGun;

        private float rotationDir;
        private float prevRotationDir;

        private bool isMovingForward;
        private bool wasMovingForward;


        public void OnMainShot(InputAction.CallbackContext context)
        {
            IsShotingMainGun = context.ReadValue<float>() == 1f;

            if (IsShotingMainGun != wasShotingMainGun)
                Messenger<bool>.Broadcast(Messages.ON_SHOTING_MAIN_GUN_CHANGE, IsShotingMainGun);

            wasShotingMainGun = IsShotingMainGun;
        }

        public void OnSpecialShot(InputAction.CallbackContext context)
        {
            IsShotingSpecialGun = context.ReadValue<float>() == 1f;

            if (IsShotingSpecialGun != wasShotingSpecialGun)
                Messenger<bool>.Broadcast(Messages.ON_SHOTING_SPECIAL_GUN_CHANGE, IsShotingSpecialGun);

            wasShotingSpecialGun = IsShotingSpecialGun;
        }

        public void OnMoveForward(InputAction.CallbackContext context)
        {
            isMovingForward = context.ReadValue<float>() == 1f;
 
            if (isMovingForward != wasMovingForward)
                Messenger<bool>.Broadcast(Messages.ON_MOVE_FORWARD_CHANGE, isMovingForward);

            wasMovingForward = isMovingForward;
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            rotationDir = context.ReadValue<float>();

            if (rotationDir != prevRotationDir)
                Messenger<float>.Broadcast(Messages.ON_ROTATE_DIR_CHANGE, rotationDir);

            prevRotationDir = rotationDir;
        }
    }
}
