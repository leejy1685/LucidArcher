using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private PlayerStatHendler playerStatHendler;
    // [SerializeField] private WeaponStat weaponStat;
    [SerializeField] private Transform hpTransform;
    [SerializeField] private List<GameObject> heartPrefabs;
    [SerializeField] private List<GameObject> createdHeart = new List<GameObject>();
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
        UpdatePlayerHpHeart();
        UpdateStaminaSlider();
        UpdatePlayerStatus();
    }

    public void UpdateExpSlider(float percentage)
    {
        Exp.value = percentage;
    }

    public void UpdatePlayerHpHeart() //체력 변화에 따른 하트를 표시해주는 메소드
    {
        PlayerGetDamage();
        PlayerGetHeal();
    }

    public void AddHeartPrefabs()
    {
        int lastHeart = (playerStatHendler.Hp / 2) + (playerStatHendler.Hp % 2); // 몇개의 하트가 있는지 (맨 끝 하트)

        GameObject selectedHeart = heartPrefabs[0];

        for (int i = 0; i < lastHeart; i++) //체력 프리펩을 가져와 표시해주는 반복문
        {
            GameObject heart = Instantiate(selectedHeart, hpTransform); // List createdHeart에 변수를 저장하기 위한 임시 변수 생성
            heart.transform.localPosition = new Vector3((i) * 75, 0, 0);

            if (playerStatHendler.Hp % 2 == 1 && i == lastHeart - 1)
            {
                Transform full = heart.transform.GetChild(0);
                Transform half = heart.transform.GetChild(1);

                full.gameObject.SetActive(false);
                half.gameObject.SetActive(true);
            }

            createdHeart.Add(heart);

        }
    }

    public void PlayerGetDamage()
    {
        if (!isPlayerHit || playerStatHendler.Hp <= 0) return;

        int targetHeart = (playerStatHendler.Hp % 2 == 1) || (playerStatHendler.Hp == 10) ? // 이 부분은 체력이 10이라 고정해두고 짠 코드, 나중에 캐릭터 최대 체력으로 수정
                          (playerStatHendler.Hp / 2) + (playerStatHendler.Hp % 2) - 1 : (playerStatHendler.Hp / 2) + (playerStatHendler.Hp % 2); // 변화를 줄 하트 위치

        Transform full = createdHeart[targetHeart].transform.GetChild(0);
        Transform half = createdHeart[targetHeart].transform.GetChild(1);
        Transform empty = createdHeart[targetHeart].transform.GetChild(2);

        if (playerStatHendler.Hp % 2 == 1) // 플레이어가 데미지를 입었는데 체력이 홀수라면 반칸짜리 하트 표시
        {
            if (playerStatHendler.Hp <= 7)
            {
                Transform nextHeartFull = createdHeart[targetHeart + 1].transform.GetChild(0);
                Transform nextHeartHalf = createdHeart[targetHeart + 1].transform.GetChild(1);
                Transform nextHeartEmpty = createdHeart[targetHeart + 1].transform.GetChild(2);

                nextHeartEmpty.gameObject.SetActive(true);
                nextHeartFull.gameObject.SetActive(false);
                nextHeartHalf.gameObject.SetActive(false);
            }

            half.gameObject.SetActive(true);
            full.gameObject.SetActive(false);
            empty.gameObject.SetActive(false);
        }

        else if (playerStatHendler.Hp == 10)
        {
            half.gameObject.SetActive(false);
            full.gameObject.SetActive(true);
            empty.gameObject.SetActive(false);
        }

        else // 플레이어가 데미지를 입었는데 체력이 짝수라면 빈 하트 표시 (일단 음수의 경우는 생각 안함)
        {
            half.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
            full.gameObject.SetActive(false);
        }

    }

    public void PlayerGetHeal()
    {
        if (!isHeal || playerStatHendler.Hp <= 0) return;

        int targetHeart = (playerStatHendler.Hp / 2) + (playerStatHendler.Hp % 2) - 1; // 변화를 줄 하트 위치

        Transform full = createdHeart[targetHeart].transform.GetChild(0);
        Transform half = createdHeart[targetHeart].transform.GetChild(1);
        Transform empty = createdHeart[targetHeart].transform.GetChild(2);

        if (playerStatHendler.Hp % 2 == 1)
        {
            if (playerStatHendler.Hp >= 3)
            {
                Transform lastHeartFull = createdHeart[targetHeart - 1].transform.GetChild(0);
                Transform lastHeartHalf = createdHeart[targetHeart - 1].transform.GetChild(1);
                Transform lastHeartEmpty = createdHeart[targetHeart - 1].transform.GetChild(2);

                lastHeartFull.gameObject.SetActive(true);
                lastHeartHalf.gameObject.SetActive(false);
                lastHeartEmpty.gameObject.SetActive(false);
            }
            full.gameObject.SetActive(false);
            empty.gameObject.SetActive(false);
            half.gameObject.SetActive(true);
        }
        else
        {
            full.gameObject.SetActive(true);
            empty.gameObject.SetActive(false);
            half.gameObject.SetActive(false);
        }

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
