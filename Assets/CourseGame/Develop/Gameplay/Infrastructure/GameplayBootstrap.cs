using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        private IGameplayVariant _gameplayVariant;

        private float _registrationDuration = 1f;

        private bool _isGameInitialized;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            if (gameplayInputArgs.LevelNumber == (int)GameplayVariants.NumbersGameplay)
            {
                StartNumbersGameplay(container);
            }
            if (gameplayInputArgs.LevelNumber == (int)GameplayVariants.LettersGameplay)
            {
                StartLettersGameplay(container);
            }

            yield return new WaitForSeconds(_registrationDuration);

            _isGameInitialized = true;
        }

        private void StartNumbersGameplay(DIContainer container)
        {
            NumbersGameplay numbersGameplay = new NumbersGameplay(container);
            _gameplayVariant = numbersGameplay;
            _container.Resolve<ICoroutinePerformer>().StartPerform(numbersGameplay.GenerateNumbers());
        }

        private void StartLettersGameplay(DIContainer container)
        {
            LettersGameplay lettersGameplay = new LettersGameplay(container);
            _gameplayVariant = lettersGameplay;
            _container.Resolve<ICoroutinePerformer>().StartPerform(lettersGameplay.ShowLetters());
        }

        private void ProcessRegistrations()
        {
            _container.Initialize();
        }

        private void Update()
        {
            if (_isGameInitialized)
            {
                _gameplayVariant.StartPerform();
            }
        }
    }
}