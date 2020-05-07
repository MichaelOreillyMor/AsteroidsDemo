namespace Asteroids.UI
{
    public class LevelStartsPanelController : BasePanelController
    {
        public void ShowLevel(int value)
        {
            value++;
            message.text = "LEVEL "+ value + " STARTS NOW";
            FadeInOut();
        }
    }
}