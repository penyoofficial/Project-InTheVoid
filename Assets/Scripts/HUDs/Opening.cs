using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 开屏画面
/// </summary>
public class Opening : MonoBehaviour
{
    public bool isButtonContinueGame;

    protected void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(DelayAndLoadScene());
        }

        if (!PlayerPrefs.HasKey("life") && isButtonContinueGame)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator DelayAndLoadScene()
    {
        Fader f = GetComponent<Fader>();
        f.Fade(0);
        yield return new WaitForSeconds(0.5f);
        f.UnFade();
        yield return new WaitForSeconds(3);
        f.Fade();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StartMenu");
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Main");
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
