namespace Assets.CourseGame.Develop.CommonServices.SceneManagement
{
    public interface IInputSceneArgs
    {
    }

    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }

    public class MainMenuInputArgs : IInputSceneArgs
    {
    }
}
