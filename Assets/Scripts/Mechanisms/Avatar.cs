using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// 玩家所操纵的角色
/// </summary>
public class Avatar : Entity
{
    Coroutine autoRecovery;

    protected new void Start()
    {
        This.Set(Context.AVATAR, gameObject);
        base.Start();
        needDieAtOnce = false;

        RenderView(false);
        autoRecovery = StartCoroutine(AutoRecover((g) => !This.Get<World>(Context.WORLD).HasNearbyEnemies(g), () => RenderView(false)));

        _PlainAttack = new(this);
        _ChargeAttack = new(this, 3);

        Load();
    }

    protected new void Update()
    {
        if (life <= 0)
        {
            StartCoroutine(OnDying());
        }

        base.Update();

        if (!isDying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (jumpedTime++ < 2)
                {
                    GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, velocityBase * 2);
                }
            }

            if ((Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<Rigidbody2D>().linearVelocity.x != 0) || Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().linearVelocity.x) * velocityBase * 2, GetComponent<Rigidbody2D>().linearVelocity.y);
            }

            float moveInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveInput) > linearTriggerThreshold)
            {
                if (moveInput < 0 && GetComponent<Rigidbody2D>().linearVelocity.x > -velocityBase)
                {
                    GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-velocityBase, GetComponent<Rigidbody2D>().linearVelocity.y);
                }
                else if (moveInput > 0 && GetComponent<Rigidbody2D>().linearVelocity.x < velocityBase)
                {
                    GetComponent<Rigidbody2D>().linearVelocity = new Vector2(velocityBase, GetComponent<Rigidbody2D>().linearVelocity.y);
                }
            }

            UpdateMedia();

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                foreach (var hit in This.Get<World>(Context.WORLD).NearbyEntities(gameObject, 3f))
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
                foreach (var hit in This.Get<World>(Context.WORLD).NearbyEntities(gameObject, 3f))
                {
                    _ChargeAttack.To(hit.GetComponent<Entity>()).Release();
                }
                isCharging = false;
            }

            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton3))
            {
                isCharging = false;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                if (isSaveAvailable)
                {
                    Save();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                if (isSaveAvailable)
                {
                    whereToVisitGodness = transform.position;
                    Vector2 to = This.Get(Context.STORE_STATUE).transform.position;
                    transform.position = new(to.x, to.y + 5);
                    isUnderGodness = true;
                }
                else if (isUnderGodness)
                {
                    transform.position = whereToVisitGodness;
                    isUnderGodness = false;

                    isStoreOn = false;
                    This.Get(Context.STORE).SetActive(isStoreOn);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
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
                if (isUnderGodness)
                {
                    isStoreOn = !isStoreOn;
                    This.Get(Context.STORE).SetActive(isStoreOn);
                }
                else
                {
                    isBackpackOn = !isBackpackOn;
                    This.Get(Context.BACKPACK).SetActive(isBackpackOn);
                }
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
                BeingHealed(10);
            }
            else if (isTrap)
            {
                PlayerPrefs.SetString("DEATH_REASON", "你试图生吃地雷");
                BeingHurt(10);
            }
            Destroy(obj.gameObject);
            RenderView();
        }

        if (obj.CompareTag("Hellfire"))
        {
            PlayerPrefs.SetString("DEATH_REASON", "你尝试在火焰里游泳");
            BeingHurt(life + defence);
        }
    }

    protected void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Crucifix"))
        {
            isSaveAvailable = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Crucifix"))
        {
            isSaveAvailable = false;
        }
    }

    protected void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            Tilemap groundTilemap = This.Get<World>(Context.WORLD).groundTilemap;
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

    bool isSaveAvailable = false;
    bool isUnderGodness = false;
    Vector2 whereToVisitGodness;

    bool isStoreOn = false;
    bool isBackpackOn = false;

    void UpdateMedia()
    {
        AvatarCamera avatarCamera = This.Get<AvatarCamera>(Context.AVATAR_CAMERA);
        if (This.Get<World>(Context.WORLD).HasNearbyBoss(gameObject) || isBossAlive)
        {
            bgmComponent.clip = bgmInKeyFight;
            isBossAlive = true;
            avatarCamera.SetFormatSize(11);
        }
        else if (This.Get<World>(Context.WORLD).HasNearbyMonsters(gameObject))
        {
            bgmComponent.clip = bgmInFight;
            avatarCamera.SetFormatSize(8);
        }
        else
        {
            bgmComponent.clip = bgmInPeace;
            avatarCamera.SetFormatSize(5);
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
            StartCoroutine(FlashText(lifeDisplayComponent));
        }
        lifeDisplayComponent.text = lifeLog;

        string manaLog = $"法力：{(int)mana}/{(int)manaLimition}";
        if (needShining && manaDisplayComponent.text != manaLog)
        {
            StartCoroutine(FlashText(manaDisplayComponent));
        }
        manaDisplayComponent.text = manaLog;

        string scoreLog = $"分数：{(int)score}";
        if (needShining && scoreDisplayComponent.text != scoreLog)
        {
            StartCoroutine(FlashText(scoreDisplayComponent));
        }
        scoreDisplayComponent.text = scoreLog;
    }

    IEnumerator FlashText(Text t)
    {
        for (int i = 0; i < 5; i++)
        {
            t.gameObject.SetActive(!t.gameObject.activeSelf);
            yield return new WaitForSeconds(0.2f);
        }
        t.gameObject.SetActive(true);
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
        This.Get<AvatarCamera>(Context.AVATAR_CAMERA).Shake();
    }

    public new void BeingHealed(float amount, HealType type = HealType.LIFE)
    {
        base.BeingHealed(amount, type);

        RenderView();
        PlaySFX(beingHealedSFX);
    }

    PlainAttack _PlainAttack;
    ChargeAttack _ChargeAttack;

    void Load()
    {
        if (PauseKVs.Keys.ToArray().Length > 0)
        {
            life = (float)PauseKVs["life"];
            mana = (float)PauseKVs["mana"];
            score = (float)PauseKVs["score"];
            transform.position = (Vector3)PauseKVs["position"];

            PauseKVs = new();
        }
        else if (PlayerPrefs.HasKey("life"))
        {
            life = PlayerPrefs.GetFloat("life");
            mana = PlayerPrefs.GetFloat("mana");
            score = PlayerPrefs.GetFloat("score");
            transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("position"));
        }
    }

    static Dictionary<object, object> PauseKVs = new();

    void Pause()
    {
        PauseKVs["life"] = life;
        PauseKVs["mana"] = mana;
        PauseKVs["score"] = score;
        PauseKVs["position"] = transform.position;

        SceneManager.LoadScene("Pause");
    }

    void Save()
    {
        PlayerPrefs.SetFloat("life", life);
        PlayerPrefs.SetFloat("mana", mana);
        PlayerPrefs.SetFloat("score", score);
        PlayerPrefs.SetString("position", JsonUtility.ToJson(transform.position));

        This.Get<GlobalNotice>(Context.GLOBAL_NOTICE).Push("保存成功！");
    }

    bool isDying = false;

    IEnumerator OnDying()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isDying = true;
        StopCoroutine(autoRecovery);
        This.Get<Fader>(Context.FADER).UnFade(5);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Failure");
    }
}
