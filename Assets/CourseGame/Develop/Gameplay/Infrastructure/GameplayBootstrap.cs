using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"Loading resources for level {gameplayInputArgs.LevelNumber}");

            yield return new WaitForSeconds(1f);

            Debug.Log($"Launch level {gameplayInputArgs.LevelNumber}");
        }

        private void ProcessRegistrations()
        {
            //registrations foe this scene

            _container.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }
        }
    }
}