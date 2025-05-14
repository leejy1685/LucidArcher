using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HeartType
{
    Empty,
    Half,
    Full,
    Shield
}


public class GameUI : BaseUI
{
    [SerializeField] private PlayerStatHendler playerStatHendler;
    [SerializeField] private Transform hpTransform;
    [SerializeField] private List<GameObject> heartPrefabs;
    [SerializeField] private List<GameObject> createdHearts = new List<GameObject>();
    [SerializeField] private List<Sprite> heartSprites;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider Exp;
    [SerializeField] private Slider lucidPowerSlider;
    [SerializeField] private TextMeshProUGUI playerDamageText;
    [SerializeField] private TextMeshProUGUI playerAttackDelayText;
    [SerializeField] private TextMeshProUGUI playerSpeedText;

    private WeaponStat weaponStat;

    public TextMeshProUGUI stage;



    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    private void Start()
    {
        weaponStat = playerStatHendler.GetComponentInChildren<WeaponStat>(true);
    }

    private void OnEnable()
    {
        InitHeartPrefabs();
    }
    void FixedUpdate()
    {
        ControlHeart();
        UpdatePlayerInfo();
    }

    public void UpdatePlayerInfo()
    {
        UpdateExpSlider((float)playerStatHendler.EXP / (float)playerStatHendler.MaxEXP);
        UpdateStaminaSlider();
        UpdatePlayerStatus();
        UpdateLucidPowerSlider();
    }

    public void UpdateExpSlider(float percentage)
    {
        Exp.value = percentage;
    }


    public void InitHeartPrefabs()
    {
        // 기존 하트 오브젝트 모두 파괴
        foreach (var heart in createdHearts)
        {
            Destroy(heart);
        }

        createdHearts.Clear(); // 리스트 초기화


        int maxHp = playerStatHendler.MaxHp;
        int shieldHp = playerStatHendler.LucidHp;

        int heartCount = (maxHp / 2) + (maxHp % 2) + shieldHp; // 몇개의 하트가 있는지 (맨 끝 하트)

        GameObject selectedHeart = heartPrefabs[0];

        for (int i = 0; i < heartCount; i++) //하트 프리펩을 가져와 표시해주는 반복문
        {
            GameObject heart = Instantiate(selectedHeart, hpTransform); // List createdHeart에 변수를 저장하기 위한 임시 변수 생성
            heart.transform.localPosition = new Vector3((i) * 75 - 300, 0, 0);

            createdHearts.Add(heart);
        }
    }

    public void ControlHeart() // 추가되는 하트를 통제하는 메서드
    {
        if (playerStatHendler.MaxHp > 6 && playerStatHendler.MaxHp % 2 == 1 && playerStatHendler.HasAdditionalMaxHp)
        {
            AdditionalRedHeartPrefabs();
            playerStatHendler.HasAdditionalMaxHp = false;
        }

        if (playerStatHendler.LucidHp > 0 && playerStatHendler.LucidHp <= 3 && playerStatHendler.HasAddionalLucidHp)
        {
            AdditionalShieldHeartPrefabs();
            playerStatHendler.HasAddionalLucidHp = false;
        }
        DestroyShieldHeart();
        Relocation();
        HeartChargeDegree();
    }

    public void AdditionalRedHeartPrefabs() // 추가 체력에 따른 붉은 하트 프리펩 생성
    {
        int maxHp = playerStatHendler.MaxHp;

        int addHeartPos = maxHp / 2;

        GameObject selectedHeart = heartPrefabs[0];
        GameObject heart = Instantiate(selectedHeart, hpTransform);
        heart.transform.localPosition = new Vector3((addHeartPos) * 75 - 300, 0, 0);

        createdHearts.Add(heart);

        Image heartSR = GetHeartImageComponent(addHeartPos);
        SetHeartSprite(heartSR, HeartType.Empty);
    }

        public void AdditionalShieldHeartPrefabs() // 추가 체력에 따른 쉴드 하트 프리펩 생성
    {
        int maxHp = playerStatHendler.MaxHp;
        int shieldHp = playerStatHendler.LucidHp;

        int addHeartPos = maxHp / 2 + shieldHp - 1;

        GameObject selectedHeart = heartPrefabs[0];
        GameObject heart = Instantiate(selectedHeart, hpTransform);
        heart.transform.localPosition = new Vector3((addHeartPos) * 75 - 300, 0, 0);

        createdHearts.Add(heart);

        Image heartSR = GetHeartImageComponent(addHeartPos);
        SetHeartSprite(heartSR, HeartType.Empty);
    }

    public void DestroyShieldHeart()
    {
        int maxHp = playerStatHendler.MaxHp;
        int shieldHeartCount = createdHearts.Count - (maxHp / 2 + maxHp % 2);

        if (playerStatHendler.LucidHp < shieldHeartCount)
        {
            GameObject lastHeart = createdHearts[createdHearts.Count - 1];
            createdHearts.RemoveAt(createdHearts.Count - 1);
            Destroy(lastHeart);
        }
    }

    private void HeartChargeDegree() // 하트 채워짐 정도
    {
        int hp = playerStatHendler.Hp;

        int fullHeartCount = hp / 2;
        bool hasHalfHeart = hp % 2 == 1;

        for (int i = 0; i < createdHearts.Count; i++)
        {
            Image heartSR = GetHeartImageComponent(i);

            if (i < fullHeartCount)
            {
                SetHeartSprite(heartSR, HeartType.Full);
            }
            else if (i == fullHeartCount && hasHalfHeart)
            {
                SetHeartSprite(heartSR, HeartType.Half);
            }
            else
            {
                SetHeartSprite(heartSR, HeartType.Empty);
            }
        }

        ShowShieldHeart();
    }

    public void ShowShieldHeart() // 실드 하트 보여주는 메서드
    {
        int redHeartCount = playerStatHendler.MaxHp / 2 + playerStatHendler.MaxHp % 2 - 1;
        int shieldHeartCount = playerStatHendler.LucidHp;

        for (int i = redHeartCount + 1; i < createdHearts.Count; i++)
        {
            Image heartSR = GetHeartImageComponent(i);
            SetHeartSprite(heartSR, HeartType.Shield);
        }
    }

    private void SetHeartSprite(Image img, HeartType type) // 하트 이미지 준비
    {
        switch (type)
        {
            case HeartType.Full:
                img.sprite = heartSprites[2];
                break;
            case HeartType.Half:
                img.sprite = heartSprites[1];
                break;
            case HeartType.Empty:
                img.sprite = heartSprites[0];
                break;
            case HeartType.Shield:
                img.sprite = heartSprites[3];
                break;
        }
    }


    private Image GetHeartImageComponent(int index) // 생성된 하트의 SpriteRenderer을 가져오는 메서드
    {
        Image img = createdHearts[index].GetComponent<Image>();

        if (img == null)
        {
            img = createdHearts[index].GetComponentInChildren<Image>();
        }

        return img;
    }

    public void Relocation() // 쉴드 하트가 붉은 하트 뒤로 가도록 재정렬해주는 메서드
    {
        for (int i = 0; i < createdHearts.Count; i++)
        {
            createdHearts[i].transform.localPosition = new Vector3((i)*75 - 300, 0, 0);
        }
    }




    public void UpdateStaminaSlider()
    {
        staminaSlider.value = playerStatHendler.Stamina / 3;
    }

    public void UpdateLucidPowerSlider()
    {
        lucidPowerSlider.value = playerStatHendler.LucidPower / 100f;
    }

    public void UpdateStageText(int stage)
    {
        this.stage.text = stage.ToString();
    }

    public void UpdatePlayerStatus()
    {
        playerDamageText.text = weaponStat.Damage.ToString();
        playerAttackDelayText.text = playerStatHendler.AttackDelay.ToString();
        playerSpeedText.text = playerStatHendler.Speed.ToString();
    }
}
