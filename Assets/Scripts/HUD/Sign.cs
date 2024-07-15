using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏中告示牌的行为
/// </summary>
public class Sign : MonoBehaviour
{
    void Start()
    {
        文本显示.gameObject.SetActive(false);
    }

    [SerializeField] private string 文本内容 = "在这里填写文本。";

    [SerializeField] private Text 文本显示;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            文本显示.text = 文本内容;
            文本显示.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            文本显示.gameObject.SetActive(false);
        }
    }
}
