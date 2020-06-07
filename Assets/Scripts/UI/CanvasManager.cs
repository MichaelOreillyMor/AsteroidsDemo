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
            Messenger<int>.AddListener(Messages.ON_START_LEVEL, ShowLevelPanel);
            Messenger<int>.AddListener(Messages.ON_SCORE_CHANGE, UpdateScorePanel);
            Messenger<bool>.AddListener(Messages.ON_ENDGAME, ShowEndGamePanel);
        }

        private void StopListeningEvents()
        {
            Messenger<int>.RemoveListener(Messages.ON_START_LEVEL, ShowLevelPanel);
            Messenger<int>.RemoveListener(Messages.ON_SCORE_CHANGE, UpdateScorePanel);
            Messenger<bool>.RemoveListener(Messages.ON_ENDGAME, ShowEndGamePanel);
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