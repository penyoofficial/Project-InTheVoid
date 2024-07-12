using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarBeingHurt : MonoBehaviour
{
    private Rigidbody2D avatar;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Trap"))
        {
            avatar.bodyType = RigidbodyType2D.Static;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
