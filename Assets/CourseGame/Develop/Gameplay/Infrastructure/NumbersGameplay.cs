using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class NumbersGameplay : IGameplayVariant
    {
        DIContainer _container;

        private List<int> _numbers = new List<int>();
        private List<int> _inputNumbers = new List<int>();

        private int _maxNumbersCount = 10;
        private int _maxNumberValue = 9;

        private bool _isInputActive;
        private bool _isGameActive;
        private bool _isWin;

        public NumbersGameplay(DIContainer container)
        {
            _container = container;
        }

        public void StartPerform()
        {
            CreateInputNumbers();

            CompareNumbers();

            SwitchScene();
        }

        public IEnumerator GenerateNumbers()
        {
            for (int i = 0; i < _maxNumbersCount; i++)
            {
                int randomNumber = Random.Range(0, _maxNumberValue);
                _numbers.Add(randomNumber);
                Debug.Log(randomNumber);
            }

            yield return null;

            _isInputActive = true;
        }

        private void CompareNumbers()
        {
            if (_numbers.Count == _inputNumbers.Count)
            {
                for (int i = 0; i < _numbers.Count; i++)
                {
                    if (_numbers[i] != _inputNumbers[i])
                    {
                        Lose();
                        return;
                    }
                }

                Win();
            }
        }

        private void Win()
        {
            _isGameActive = false;
            _isWin = true;

            Debug.Log("Win");
        }

        private void Lose()
        {
            _isGameActive = false;
            _isWin = false;

            Debug.Log("Lose");
        }

        private void CreateInputNumbers()
        {
            if (_isInputActive)
                for (int input = 0; input < _maxNumberValue; input++)
                    if (Input.GetKeyDown(input.ToString()))
                        _inputNumbers.Add(input);
        }

        private void SwitchScene()
        {
            if (_isGameActive == false)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_isWin)
                        _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
                    else
                        _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new
                            OutputGameplayArgs(new GameplayInputArgs(((int)GameplayVariants.NumbersGameplay))));
                }
        }
    }
}