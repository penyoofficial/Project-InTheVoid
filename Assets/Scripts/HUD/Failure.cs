using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 失败画面的结局
/// </summary>
public class Failure : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene(1);
    }
}
