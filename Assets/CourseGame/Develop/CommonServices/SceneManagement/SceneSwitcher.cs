using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.CommonServices.LoadingScreen;
using System;
using System.Collections;
using Object = UnityEngine.Object;
using Assets.CourseGame.Develop.Gameplay.Infrastructure;
using Assets.CourceGame.Develop.DI;

namespace Assets.CourseGame.Develop.CommonServices.SceneManagement
{
    public class SceneSwitcher
    {
        private const string ErrorSceneTransicionMessage = "This transicion is not allowed";

        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ILoadingCurrtain _loadingCurrtain;
        private readonly ISceneLoader _sceneLoader;
        private readonly DIContainer _projectContainer;

        private DIContainer _currentSceneContainer;

        public SceneSwitcher(
            ICoroutinePerformer coroutinePerformer,
            ILoadingCurrtain loadingCurrtain,
            ISceneLoader sceneLoader,
            DIContainer projectContainer)
        {
            _coroutinePerformer = coroutinePerformer;
            _loadingCurrtain = loadingCurrtain;
            _sceneLoader = sceneLoader;
            _projectContainer = projectContainer;
        }

        public void ProcessSwitchSceneFor(IOutputSceneArgs outputSceneArgs)
        {
            switch (outputSceneArgs)
            {
                case OutputBootstrapArgs outputBootstrapArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromBootstrapScene(outputBootstrapArgs));
                    break;

                case OutputMainMenuArgs outputMainMenuArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromMainMenuScene(outputMainMenuArgs));
                    break;

                case OutputGameplayArgs outputGameplayArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromGameplayScene(outputGameplayArgs));
                    break;

                default:
                    throw new ArgumentException(nameof(outputSceneArgs));
            }
        }

        private IEnumerator ProcessSwitchFromBootstrapScene(OutputBootstrapArgs outputBootstrapArgs)
        {
            switch (outputBootstrapArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransicionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromMainMenuScene(OutputMainMenuArgs outputMainMenuArgs)
        {
            switch (outputMainMenuArgs.NextSceneInputArgs)
            {
                case GameplayInputArgs gameplayInputArgs:
                    yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransicionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromGameplayScene(OutputGameplayArgs outputGameplayArgs)
        {
            switch (outputGameplayArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;
                case GameplayInputArgs gameplayInputArgs:
                    yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransicionMessage);
            }
        }

        private IEnumerator ProcessSwitchToMainMenuScene(MainMenuInputArgs mainMenuInputArgs)
        {
            _loadingCurrtain.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.MainMenu);

            MainMenuBootstrap mainMenuBootstrap = Object.FindAnyObjectByType<MainMenuBootstrap>();

            if (mainMenuBootstrap == null)
                throw new NullReferenceException(nameof(MainMenuBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return mainMenuBootstrap.Run(_currentSceneContainer, mainMenuInputArgs);

            _loadingCurrtain.Hide();
        }

        private IEnumerator ProcessSwitchToGameplayScene(GameplayInputArgs gameplayInputArgs)
        {
            _loadingCurrtain.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.Gameplay);

            GameplayBootstrap gameplayBootstrap = Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(GameplayBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return gameplayBootstrap.Run(_currentSceneContainer, gameplayInputArgs);

            _loadingCurrtain.Hide();
        }
    }
}
