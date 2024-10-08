using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 失败画面
/// </summary>
public class Failure : MonoBehaviour
{
    public Text deathReason;

    void Start()
    {
        string text = PlayerPrefs.GetString("DEATH_REASON");
        if (deathReason != null && text != null)
        {
            deathReason.text = text;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
