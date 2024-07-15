using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    private Rigidbody2D avatar;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    [SerializeField] private float 移速基数 = 5f;

    [SerializeField] private float 线性触发器阈值 = 0.2f;

    private int jumpedTime = 0;

    void Update()
    {
        if (avatar.velocity.y == 0)
        {
            jumpedTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (jumpedTime++ < 2)
            {
                avatar.velocity = new Vector2(avatar.velocity.x, 移速基数 * 2);
            }
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) && avatar.velocity.x != 0) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            avatar.velocity = new Vector2(Mathf.Sign(avatar.velocity.x) * 移速基数 * 2, avatar.velocity.y);
        }

        float moveInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveInput) > 线性触发器阈值)
        {
            if (moveInput < 0 && avatar.velocity.x > -移速基数)
            {
                avatar.velocity = new Vector2(-移速基数, avatar.velocity.y);
            }
            else if (moveInput > 0 && avatar.velocity.x < 移速基数)
            {
                avatar.velocity = new Vector2(移速基数, avatar.velocity.y);
            }
        }
    }
}
