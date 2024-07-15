using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开屏画面的行为
/// </summary>
public class Opening : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(DelayAndLoadScene());
    }

    private IEnumerator DelayAndLoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void TeminateGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
