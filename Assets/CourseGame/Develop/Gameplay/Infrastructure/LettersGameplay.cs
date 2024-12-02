using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class LettersGameplay : IGameplayVariant
    {
        DIContainer _container;

        private List<char> _letters = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private List<char> _inputLetters = new List<char>();

        private bool _isInputActive;
        private bool _isGameActive;
        private bool _isWin;

        public LettersGameplay(DIContainer container)
        {
            _container = container;
        }

        public void StartPerform()
        {
            CreateInputLetters();

            CompareLetters();

            SwitchScene();
        }

        public IEnumerator ShowLetters()
        {
            foreach (char letter in _letters)
            {
                Debug.Log(letter);
            }

            yield return null;

            _isInputActive = true;
        }

        private void CompareLetters()
        {
            if (_letters.Count == _inputLetters.Count)
            {
                for (int i = 0; i < _letters.Count; i++)
                {
                    if (_letters[i] != _inputLetters[i])
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

        private void CreateInputLetters()
        {
            if (_isInputActive)
            {
                string input = Input.inputString;

                foreach (char letter in input)
                    _inputLetters.Add(letter);
            }
        }

        private void SwitchScene()
        {
            if (!_isGameActive)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_isWin)
                        _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
                    else
                        _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new
                            OutputGameplayArgs(new GameplayInputArgs(((int)GameplayVariants.LettersGameplay))));
                }
        }
    }
}
