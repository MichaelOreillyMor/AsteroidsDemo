using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Check and correct the position to be always inside the camera view
    /// </summary>
    public class ScreenLismitsHandler
    {
        private const float FIX_STUCK_ON_LIMIT_MULT = 0.95f;

        private Vector2 limitsAxis_X;
        private Vector2 limitsAxis_Z;
        private Camera cameraMain;

        public ScreenLismitsHandler()
        {
            cameraMain = Camera.main;
            CalculateLimits();
        }

        #region Calculate limits methods

        private void CalculateLimits()
        {
            float height = cameraMain.orthographicSize * 2.0f;
            float width = height * cameraMain.aspect;

            Vector3 cameraPosition = cameraMain.transform.position;
            limitsAxis_X = new Vector2(cameraPosition.x - width, cameraPosition.x + width) * 0.5f;
            limitsAxis_Z = new Vector2(cameraPosition.z - height, cameraPosition.z + height) * 0.5f;
        }

        #endregion

        #region Check limits methods

        public void FixLimitsPosition(Transform tr)
        {
            FixLimitsPositionAxis_Z(tr);
            FixLimitsPositionAxis_X(tr);
        }

        private void FixLimitsPositionAxis_Z(Transform tr)
        {
            if (tr.position.z < limitsAxis_Z.x || tr.position.z > limitsAxis_Z.y)
            {
                Vector3 positionFixed = tr.position;
                positionFixed.z = -positionFixed.z * FIX_STUCK_ON_LIMIT_MULT;
                tr.position = positionFixed;
            }
        }

        private void FixLimitsPositionAxis_X(Transform tr)
        {
            if (tr.position.x < limitsAxis_X.x || tr.position.x > limitsAxis_X.y)
            {
                Vector3 positionFixed = tr.position;
                positionFixed.x = -positionFixed.x * FIX_STUCK_ON_LIMIT_MULT;
                tr.position = positionFixed;
            }
        }

        #endregion
    }
}