using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏中告示牌的行为
/// </summary>
public class Sign : MonoBehaviour
{
    public string title = "在这里填写文本。";
    public string subtitle = "在这里填写二级文本。";

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            This.Get<Text>(Context.GLOBAL_NOTICE).text = title;
            This.Get<GlobalNotice>(Context.GLOBAL_NOTICE).gameObject.SetActive(true);

            This.Get<Text>(Context.GLOBAL_NOTICE_2).text = subtitle;
            This.Get<GlobalNotice>(Context.GLOBAL_NOTICE_2).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            This.Get<GlobalNotice>(Context.GLOBAL_NOTICE).gameObject.SetActive(false);
            This.Get<GlobalNotice>(Context.GLOBAL_NOTICE_2).gameObject.SetActive(false);
        }
    }
}
