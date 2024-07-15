using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

public class AvatarBeingHurt : MonoBehaviour
{
    void Start()
    {
        life = 生命值上限;
        RenderView();
    }

    [SerializeField] private int 生命值上限 = 100;

    private int life;

    [SerializeField] private Text 生命值显示;

    [SerializeField] private AudioClip 受伤音效;

    [SerializeField] private AudioClip 回复音效;

    void RenderView()
    {
        生命值显示.text = $"生命：{life}/{生命值上限}";
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Trap"))
        {
            Destroy(c.gameObject);
            life -= 10;
            RenderView();
            StartCoroutine(HUD.FlashText(生命值显示));

            if (life <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(0);
            }

            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = 受伤音效;
            s.Play();
        }
        else if (c.gameObject.CompareTag("Recovery"))
        {
            Destroy(c.gameObject);
            life += 10;
            if (life >= 生命值上限)
            {
                life = 生命值上限;
            }
            RenderView();
            StartCoroutine(HUD.FlashText(生命值显示));

            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = 回复音效;
            s.Play();
        }
    }
}
