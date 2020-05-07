namespace Asteroids.UI
{
    public class MessagePanelController : BasePanelController
    {
        public void ShowMessage(string text)
        {
            message.text = text;
            FadeIn();
        }
    }
}