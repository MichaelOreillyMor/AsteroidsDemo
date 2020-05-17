using Asteroids.Datas;
using Asteroids.Effects;
using Asteroids.Entities;
using Asteroids.UI;
using Asteroids.Input;
using Asteroids.Utilities.Messages;

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Systems
{
    /// <summary>
    /// Entry point to the game, manage game initialization and main logic
    /// </summary>
    public class GameState : MonoBehaviour, GameControls.IGameActions
    {
        private const int SCORE_PER_ASTEROID = 50;
        private const int TIME_TO_RESET_GAME = 2;

        [SerializeField]
        private SpaceshipData spaceshipData;

        [SerializeField]
        private AsteroidsLevelsData AsteroidsLevelsData;

        [SerializeField]
        private AsteroidsLevelsController asteroidsLevelsController;

        [SerializeField]
        private CanvasManager canvasManager;

        [SerializeField]
        private SpaceshipState spaceshipState;

        [SerializeField]
        private CameraShake cameraShake;

        [SerializeField]
        private BackgroundController backgroundController;

        private InputHandler inputHandler;

        private int score;

        void Start()
        {
            //Small hack to don´t play music all the time
#if UNITY_EDITOR
            AudioListener.volume = 0;
#else
            AudioListener.volume = 1;
#endif
            Setup();
        }

        #region Setup/Unsetup methods

        public void Setup()
        {
            canvasManager.Setup();
            asteroidsLevelsController.Setup(AsteroidsLevelsData);
            spaceshipState.Setup(spaceshipData);
            cameraShake.Setup();
            backgroundController.Setup();

            score = 0;

            StartListeningMensajes();

            inputHandler = new InputHandler();
            inputHandler.SetGameActionsCallbacks(this);
            inputHandler.SetSpaceshipActionsCallbacks(spaceshipState);
        }

        //method not used yet
        public void Unsetup()
        {
            canvasManager.Unsetup();
            asteroidsLevelsController.Unsetup();
            spaceshipState.Unsetup();

            StopListeningMensajes();
        }
        private void StartListeningMensajes()
        {
            Messenger<AsteroidDestroyedMessage>.AddListener("OnAsteroidDestroyed", OnAsteroidDestroyed);
            Messenger<bool>.AddListener("OnEndGame", OnGameEnded);
        }

        private void StopListeningMensajes()
        {
            Messenger<AsteroidDestroyedMessage>.RemoveListener("OnAsteroidDestroyed", OnAsteroidDestroyed);
            Messenger<bool>.RemoveListener("OnEndGame", OnGameEnded);
        }

        #endregion

        #region Callback events

        private void OnAsteroidDestroyed(AsteroidDestroyedMessage asteroidDestroyedMessage)
        {
            cameraShake.Shake(1f, 1f, 40f);
            score += SCORE_PER_ASTEROID;

            Messenger<int>.Broadcast("OnScoreChange", score);
        }

        private void OnGameEnded(bool playerWins)
        {
            if(!playerWins)
                cameraShake.Shake(1f, 2f, 70f);

            StartCoroutine(DelayResetGame(playerWins));
        }

        #endregion

        private IEnumerator DelayResetGame(bool winState)
        {
            yield return new WaitForSeconds(TIME_TO_RESET_GAME);

            score = 0;
            spaceshipState.ResetState();
            asteroidsLevelsController.ResetLevels();
            canvasManager.ResetPanels();
        }

        public void OnCloseGame(InputAction.CallbackContext context)
        {
            Application.Quit();
        }
    }
}

