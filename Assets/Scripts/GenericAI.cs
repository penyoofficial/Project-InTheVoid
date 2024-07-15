using UnityEngine;
using UnityEngine.UI;

public abstract class GenericAI : MonoBehaviour
{
    protected Rigidbody2D ai;

    private static float difficutyFactor = 0;

    void Start()
    {
        ai = GetComponent<Rigidbody2D>();
        difficutyFactor += 0.02f;
        life = (int)(初始生命 * (1 + difficutyFactor));
        攻击力 = (int)(攻击力 * (1 + difficutyFactor));

        血条控件.gameObject.SetActive(false);
    }

    protected int life;

    [SerializeField] private int 初始生命 = 100;

    [SerializeField] private Slider 血条控件;

    [SerializeField] private int 攻击力 = 10;

    public void BeingHurt(int damage)
    {
        血条控件.gameObject.SetActive(true);
        life -= damage;
        血条控件.value = 1.0f * life / 初始生命;
    }

    public void BeingKnockedOff(float vx)
    {
        ai.velocity = new Vector2(vx, vx * 2);
    }

    protected void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
