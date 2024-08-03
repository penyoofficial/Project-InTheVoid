using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 暂停画面
/// </summary>
public class Pause : MonoBehaviour
{
    public void BackToGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
