namespace Asteroids.Input
{
    //Will be have more functionalities in the future
    public class InputHandler
    {
        private GameControls gameControls;

        public InputHandler()
        {
            gameControls = new GameControls();
            gameControls.Enable();
        }

        public void SetSpaceshipActionsCallbacks(GameControls.ISpaceshipActions spaceship)
        {
            gameControls.Spaceship.SetCallbacks(spaceship);
        }

        public void SetGameActionsCallbacks(GameControls.IGameActions game)
        {
            gameControls.Game.SetCallbacks(game);
        }
    }
}
