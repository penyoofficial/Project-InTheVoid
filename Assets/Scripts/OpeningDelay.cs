using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDelay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayAndLoadScene());
    }

    private IEnumerator DelayAndLoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
