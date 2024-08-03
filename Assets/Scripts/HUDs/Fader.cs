using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 渐隐器
/// </summary>
public class Fader : MonoBehaviour
{
    protected void Start()
    {
        This.Set(Context.FADER, gameObject);
    }

    float duration = 0.1f;
    float to = 1;

    bool hasBeenReady = false;

    protected void Update()
    {
        if (!hasBeenReady)
        {
            This.Get<World>(Context.WORLD).SetTimeout(() => FadeIn(3), 0.1f);
            hasBeenReady = true;
        }

        Image i = GetComponent<Image>();

        Color c = i.color;
        c.a = Mathf.MoveTowards(c.a, to, Time.deltaTime / duration);
        i.color = c;
    }

    public void FadeIn(float duration)
    {
        this.duration = duration;
        to = 0;
    }

    public void FadeOut(float duration)
    {
        this.duration = duration;
        to = 1;
    }
}
