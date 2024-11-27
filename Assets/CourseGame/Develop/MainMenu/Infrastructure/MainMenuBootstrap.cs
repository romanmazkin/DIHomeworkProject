using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        yield return new WaitForSeconds(1f);
    }

    private void ProcessRegistrations()
    {
        //registrations foe this scene

        _container.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(1)));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
        }
    }
}
