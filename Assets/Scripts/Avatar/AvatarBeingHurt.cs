using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

/// <summary>
/// 古希腊掌握玩家生命机制的神
/// </summary>
public class AvatarBeingHurt : MonoBehaviour
{
    private IEnumerator AutoRecover()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (!Game2D.HasNearbyEnemy(GetComponent<Rigidbody2D>()) && life < 生命值上限)
            {
                life++;
                RenderView();
            }
        }
    }

    void Start()
    {
        life = 生命值上限;
        RenderView();
        StartCoroutine(AutoRecover());
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

    void PlaySFX(AudioClip c)
    {
        AudioSource s = gameObject.AddComponent<AudioSource>();
        s.clip = c;
        s.Play();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Trap") || c.gameObject.CompareTag("Attachment"))
        {
            Destroy(c.gameObject);
            AbstractAI ai = c.GetComponent<AbstractAI>();
            life -= ai != null ? ai.Hurt() : 10;
            RenderView();
            StartCoroutine(HUD.FlashText(生命值显示));
            PlaySFX(受伤音效);
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
            PlaySFX(回复音效);
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Monster") || c.gameObject.CompareTag("Boss"))
        {
            int hurt = c.GetComponent<AbstractAI>().Hurt();
            if (hurt > 0)
            {
                life -= hurt;
                RenderView();
                StartCoroutine(HUD.FlashText(生命值显示));
                PlaySFX(受伤音效);
            }
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
