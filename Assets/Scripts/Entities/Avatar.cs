using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Utility;

/// <summary>
/// 玩家所操纵的角色
/// </summary>
public class Avatar : Entity
{
    protected new void Start()
    {
        base.Start();

        RenderView(false);
        StartCoroutine(AutoRecover((g) => !Game2D.HasNearbyEnemies(g.GetComponent<Rigidbody2D>()), () => RenderView(false)));

        _PlainAttack = new(this);
        _ChargeAttack = new(this);
    }

    protected void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (jumpedTime++ < 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, velocityBase * 2);
            }
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<Rigidbody2D>().velocity.x != 0) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * velocityBase * 2, GetComponent<Rigidbody2D>().velocity.y);
        }

        float moveInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveInput) > linearTriggerThreshold)
        {
            if (moveInput < 0 && GetComponent<Rigidbody2D>().velocity.x > -velocityBase)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-velocityBase, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (moveInput > 0 && GetComponent<Rigidbody2D>().velocity.x < velocityBase)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocityBase, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        UpdateBGM();

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            foreach (var hit in Game2D.NearbyEntities(GetComponent<Rigidbody2D>(), 3f, new string[] { "Boss", "Monster" }))
            {
                _PlainAttack.To(hit.GetComponent<Enemy>()).Release();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }

        if (isCharging && Time.time >= chargeStartTime + chargingTime)
        {
            foreach (var hit in Game2D.NearbyEntities(GetComponent<Rigidbody2D>(), 3f, new string[] { "Boss", "Monster" }))
            {
                _ChargeAttack.To(hit.GetComponent<Enemy>()).Release();
            }
            isCharging = false;
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton3))
        {
            isCharging = false;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            switch (trickPointer % 4)
            {
                default: break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (++trickPointer % 4 == 0)
            {
                trickPointer = 0;
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D obj)
    {
        bool isBonus = obj.CompareTag("Bonus");
        bool isRecovery = obj.CompareTag("Recovery");
        bool isTrap = obj.CompareTag("Trap");

        if (isBonus || isRecovery || isTrap)
        {
            if (isBonus)
            {
                score += (int)Random.Range(10.0f, 15.9f);
                PlaySFX(pickupSFX);
            }
            else if (isRecovery)
            {
                BeingHealed(10f);
            }
            else if (isTrap)
            {
                Pinia.Set(PiniaItem.DEATH_REASON, "你试图生吃地雷");
                BeingHurt(10f);
            }
            Destroy(obj.gameObject);
            RenderView();
        }

        bool isHellfire = obj.CompareTag("Hellfire");

        if (isHellfire)
        {
            Pinia.Set(PiniaItem.DEATH_REASON, "你尝试在火焰里游泳");
            BeingHurt(life + defence);
        }
    }

    protected void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            Tilemap groundTilemap = GetComponent<World>().groundTilemap;
            Vector3Int tilePosition = groundTilemap.WorldToCell(obj.GetContact(0).point);

            if (groundTilemap.GetTile(new(tilePosition.x, tilePosition.y + 1, tilePosition.z)) == null)
            {
                jumpedTime = 0;
            }
        }
    }

    float score = 0;

    public Text lifeDisplayComponent;
    public Text manaDisplayComponent;
    public Text scoreDisplayComponent;

    public AudioClip beingHurtSFX;
    public AudioClip beingHealedSFX;
    public AudioClip pickupSFX;

    public float linearTriggerThreshold;

    int jumpedTime = 0;

    public AudioSource bgmComponent;
    public AudioClip bgmInPeace;
    public AudioClip bgmInFight;
    public AudioClip bgmInKeyFight;

    bool isBossAlive = false;

    public float chargingMagnification;
    public float chargingTime;
    float chargeStartTime = 0;
    bool isCharging = false;

    int trickPointer = 0;

    void UpdateBGM()
    {
        if (Game2D.HasNearbyBoss(GetComponent<Rigidbody2D>()) || isBossAlive)
        {
            bgmComponent.clip = bgmInKeyFight;
            isBossAlive = true;
            GetComponent<World>().SetFormatSize(15f);
        }
        else if (Game2D.HasNearbyMonsters(GetComponent<Rigidbody2D>()))
        {
            bgmComponent.clip = bgmInFight;
            GetComponent<World>().SetFormatSize(8f);
        }
        else
        {
            bgmComponent.clip = bgmInPeace;
            GetComponent<World>().SetFormatSize(5f);
        }

        if (!bgmComponent.isPlaying)
        {
            bgmComponent.Play();
        }
    }

    public void KilledBoss()
    {
        isBossAlive = false;
    }

    void RenderView(bool needShining = true)
    {
        string lifeLog = $"生命：{(int)life}/{(int)lifeLimition}";
        if (needShining && lifeDisplayComponent.text != lifeLog)
        {
            StartCoroutine(HUD.FlashText(lifeDisplayComponent));
        }
        lifeDisplayComponent.text = lifeLog;

        string manaLog = $"法力：{(int)mana}/{(int)manaLimition}";
        if (needShining && manaDisplayComponent.text != manaLog)
        {
            StartCoroutine(HUD.FlashText(manaDisplayComponent));
        }
        manaDisplayComponent.text = manaLog;

        string scoreLog = $"分数：{(int)score}";
        if (needShining && scoreDisplayComponent.text != scoreLog)
        {
            StartCoroutine(HUD.FlashText(scoreDisplayComponent));
        }
        scoreDisplayComponent.text = scoreLog;
    }

    void PlaySFX(AudioClip c)
    {
        AudioSource s = gameObject.AddComponent<AudioSource>();
        s.clip = c;
        s.Play();
    }

    public override void BeingHurt(float damage)
    {
        base.BeingHurt(damage);

        RenderView();
        PlaySFX(beingHurtSFX);
    }

    public new void BeingHealed(float amount, HealType type = HealType.LIFE)
    {
        base.BeingHealed(amount, type);

        RenderView();
        PlaySFX(beingHealedSFX);
    }

    PlainAttack _PlainAttack;
    PlainAttack _ChargeAttack;
}
