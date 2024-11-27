using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.CourseGame.Develop.CommonServices.SceneManagement
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    }
}
