using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全局通知
/// </summary>
public class GlobalNotice : MonoBehaviour
{
    public float nClass;

    protected void Start()
    {
        This.Set(nClass == 1 ? Context.GLOBAL_NOTICE : Context.GLOBAL_NOTICE_2, gameObject);
        gameObject.SetActive(false);
    }

    public void Push(string msg)
    {
        Text t = GetComponent<Text>();
        t.text = msg;
        t.gameObject.SetActive(true);
        This.Get<World>(Context.WORLD).SetTimeout(() => t.gameObject.SetActive(false), 2);
    }
}