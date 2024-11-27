using UnityEngine;

namespace Assets.CourseGame.Develop.CommonServices.LoadingScreen
{
    public class StandartLoadingCurtain : MonoBehaviour, ILoadingCurrtain
    {
        public bool IsShown => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}
