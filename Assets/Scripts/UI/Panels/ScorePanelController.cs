namespace Asteroids.UI
{
    public class ScorePanelController : BasePanelController
    {
        public void ShowScore(int value)
        {
            message.text = "SCORE " + value;
        }
    }
}