using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
