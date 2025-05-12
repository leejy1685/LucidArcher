using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HeartType
{
    Empty,
    Half,
    Full
}


public class GameUI : BaseUI
{
    [SerializeField] private PlayerStatHendler playerStatHendler;
    // [SerializeField] private WeaponStat weaponStat;
    [SerializeField] private Transform hpTransform;
    [SerializeField] private List<GameObject> heartPrefabs;
    [SerializeField] private List<GameObject> createdHearts = new List<GameObject>();
    [SerializeField] private List<Sprite> heartSprites;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider Exp;
    // [SerializeField] private TextMeshProUGUI playerDamageText;
    [SerializeField] private TextMeshProUGUI playerAttackDelayText;
    [SerializeField] private TextMeshProUGUI playerSpeedText;

    [SerializeField] private bool isPlayerHit;
    [SerializeField] private bool isHeal;
    public TextMeshProUGUI stage;



    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    private void Start()
    {
        UpdateExpSlider(0);
        AddHeartPrefabs();
    }

    void FixedUpdate()
    {
        HeartChargeDegree();
        UpdateStaminaSlider();
        UpdatePlayerStatus();
    }

    public void UpdateExpSlider(float percentage)
    {
        Exp.value = percentage;
    }


    public void AddHeartPrefabs()
    {
        int maxHp = playerStatHendler.MaxHp;

        int MaxHeart = (maxHp / 2) + (maxHp % 2); // 몇개의 하트가 있는지 (맨 끝 하트)

        GameObject selectedHeart = heartPrefabs[0];

        for (int i = 0; i < MaxHeart; i++) //체력 프리펩을 가져와 표시해주는 반복문
        {
            GameObject heart = Instantiate(selectedHeart, hpTransform); // List createdHeart에 변수를 저장하기 위한 임시 변수 생성
            heart.transform.localPosition = new Vector3((i) * 75 - 300, 0, 0);

            if (maxHp % 2 == 1 && i == MaxHeart - 1)
            {
                Transform full = heart.transform.GetChild(0);
                Transform half = heart.transform.GetChild(1);

                full.gameObject.SetActive(false);
                half.gameObject.SetActive(true);
            }

            createdHearts.Add(heart);
        }
    }

    private void HeartChargeDegree()
    {
        int maxHeartCount = playerStatHendler.MaxHp / 2;
        int hp = playerStatHendler.Hp;

        int fullHeartCount = hp / 2;
        bool hasHalfHeart = hp % 2 == 1;

        for (int i = 0; i < maxHeartCount; i++)
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
    }

    private void SetHeartSprite(Image img, HeartType type)
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




    public void UpdateStaminaSlider()
    {
        staminaSlider.value = playerStatHendler.Stamina / 3;
    }

    public void UpdateStageText(int stage)
    {
        this.stage.text = stage.ToString();
    }

    public void UpdatePlayerStatus()
    {
        // playerDamageText.text = weaponStat.Damage.ToString();
        playerAttackDelayText.text = playerStatHendler.AttackDelay.ToString();
        playerSpeedText.text = playerStatHendler.Speed.ToString();
    }
}
