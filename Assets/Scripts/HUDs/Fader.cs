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

    public bool hasBeenReady = false;

    protected void Update()
    {
        if (!hasBeenReady)
        {
            This.Get<World>(Context.WORLD).SetTimeout(() => Fade(3), 0.1f);
            hasBeenReady = true;
        }

        Image i = GetComponent<Image>();

        Color c = i.color;
        c.a = duration > 0 ? Mathf.MoveTowards(c.a, to, Time.deltaTime / duration) : to;
        i.color = c;
    }

    public void Fade(float duration = 1)
    {
        this.duration = duration;
        to = 0;
    }

    public void UnFade(float duration = 1)
    {
        this.duration = duration;
        to = 1;
    }
}
