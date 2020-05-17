using UnityEngine;

namespace Asteroids.Datas
{    
     /// <summary>
     /// This is an "Enum" of the different asteroids´ stages with additional info
     /// </summary>
    [CreateAssetMenu(fileName = "New AsteroidsStagesData", menuName = "Data/AsteroidsStagesData")]
    public class AsteroidsStagesData : ScriptableObject
    {
        public AsteroidStageData SmallStage;
        public AsteroidStageData MediumStage;
        public AsteroidStageData BigStage;
    }
}
