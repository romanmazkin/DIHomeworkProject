using System.Collections;
using UnityEngine;
using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.LoadingScreen;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;

namespace Assets.CourseGame.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurrtain loadingCurrtain = container.Resolve<ILoadingCurrtain>();
            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();

            loadingCurrtain.Show();

            Debug.Log("Run services initialization");

            //initialize all services

            yield return new WaitForSeconds(1.5f);

            //disable launch display

            Debug.Log("Services initialization is done");

            loadingCurrtain.Hide();

            sceneSwitcher.ProcessSwitchSceneFor(new OutputBootstrapArgs(new MainMenuInputArgs()));
        }
    }
}
