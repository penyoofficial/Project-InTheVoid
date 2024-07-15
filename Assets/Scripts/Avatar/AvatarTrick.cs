using UnityEngine;

/// <summary>
/// 古希腊掌握玩家技能机制的神
/// 
/// <para><b>该类尚未完工。</b></para>
/// </summary>
public class AvatarTrick : MonoBehaviour
{
    private Rigidbody2D avatar;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    private int pointer = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            switch (pointer % 4)
            {
                case 0: TShoot(); break;
                case 1: TFetch(); break;
                case 2: TCharm(); break;
                case 3: TElegy(); break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (++pointer % 4 == 0)
            {
                pointer = 0;
            }
        }
    }

    // -------------------------------------------------------
    // 下面是玩家的技能行为。具体描述请参考用户文档（README.md）。
    // -------------------------------------------------------

    void TShoot()
    {

    }

    void TFetch()
    {

    }

    void TCharm()
    {

    }

    void TElegy()
    {

    }
}
