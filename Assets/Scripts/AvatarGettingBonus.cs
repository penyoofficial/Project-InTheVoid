using UnityEngine;
using UnityEngine.UI;

public class AvatarGettingBonus : MonoBehaviour
{
    private Rigidbody2D avatar;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    private int score = 0;

    [SerializeField] private Text 分数显示;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Bonus"))
        {
            Destroy(c.gameObject);
            score += (int)Random.Range(0.1f, 3.9f);
            分数显示.text = $"分数：{score}";
        }
    }
}
