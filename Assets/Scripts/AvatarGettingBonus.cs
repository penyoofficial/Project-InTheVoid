using UnityEngine;
using UnityEngine.UI;
using Utility;

public class AvatarGettingBonus : MonoBehaviour
{
    void Start()
    {
        RenderView();
    }

    private int score = 0;

    [SerializeField] private Text 分数显示;

    [SerializeField] private AudioClip 拾取音效;

    void RenderView()
    {
        分数显示.text = $"分数：{score}";
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Bonus"))
        {
            Destroy(c.gameObject);
            score += (int)Random.Range(10.0f, 15.9f);
            RenderView();
            StartCoroutine(HUD.FlashText(分数显示));

            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = 拾取音效;
            s.Play();
        }
    }
}
