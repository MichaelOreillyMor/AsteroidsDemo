using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.UI
{
    public class CanvasManager : MonoBehaviour
    {
        private const string MESSAGE_ON_GAME_COMPLETED = "GAME COMPLETED";
        private const string MESSAGE_ON_DIED = "YOU DIED";

        [SerializeField]
        private ScorePanelController scorePanelController;

        [SerializeField]
        private LevelStartsPanelController levelStartsPanelController;

        [SerializeField]
        private MessagePanelController messagePanelController;

        public void Setup()
        {
            StartListeningEvents();
            scorePanelController.ShowScore(0);
        }

        public void Unsetup()
        {
            StopListeningEvents();
        }

        public void ResetPanels()
        {
            scorePanelController.ShowScore(0);
            messagePanelController.Hide();
        }

        #region events listen methods

        private void StartListeningEvents()
        {
            Messenger<int>.AddListener("OnStartLevel", ShowLevelPanel);
            Messenger<int>.AddListener("OnScoreChange", UpdateScorePanel);
            Messenger<int>.AddListener("OnScoreChange", UpdateScorePanel);
            Messenger<bool>.AddListener("OnEndGame", ShowEndGamePanel);
        }

        private void StopListeningEvents()
        {
            Messenger<int>.RemoveListener("OnStartLevel", ShowLevelPanel);
            Messenger<int>.RemoveListener("OnScoreChange", UpdateScorePanel);
            Messenger<bool>.RemoveListener("OnEndGame", ShowEndGamePanel);
        }

        #endregion

        #region Show panels on events methods

        private void ShowEndGamePanel(bool playerWins)
        {
            if(playerWins)
                messagePanelController.ShowMessage(MESSAGE_ON_GAME_COMPLETED);
            else
                messagePanelController.ShowMessage(MESSAGE_ON_DIED);
        }

        private void UpdateScorePanel(int score)
        {
            scorePanelController.ShowScore(score);
        }

        private void ShowLevelPanel(int level)
        {
            levelStartsPanelController.ShowLevel(level);
        }

        #endregion
    }
}