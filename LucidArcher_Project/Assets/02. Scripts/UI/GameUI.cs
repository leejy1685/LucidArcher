using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public Slider Exp;
    public TextMeshProUGUI stage;



    int playerHp = 10;

    [SerializeField] private Transform hpTransform;
    [SerializeField] private List<GameObject> heartPrefabs;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    private void Start()
    {
        UpdateExpSlider(0);
        UpdatePlayerHpHeart(playerHp);
    }

    public void UpdateExpSlider(float percentage)
    {
        Exp.value = percentage;
    }

    public void UpdatePlayerHpHeart(int hp) //체력에 따른 하트를 표시해주는 메소드
    {
        int lastHeart = (hp / 2) + (hp % 2); // 몇개의 하트가 있는지 and 변화를 줄 하트 위치(맨 끝 하트)
        int kindOfHeart = hp % 3; // full하트 half하트 enpty하트 셋 중 뭘 보여줄건지

        GameObject selectedHeart = heartPrefabs[0];

        for (int i = 0; i < lastHeart; i++) //체력 프리펩을 가져와 표시해주는 반복문
        {
            if (lastHeart % 2 == 0)
            {
                GameObject createdHeart = Instantiate(selectedHeart, hpTransform);
                createdHeart.transform.localPosition = new Vector3((i) * 25, 0, 0);
            }
            
            else
            {
                if (i <= lastHeart - 2) // 마지막 하트 전까지는 Full하트트 생성성
                {
                    GameObject createdHeart = Instantiate(selectedHeart, hpTransform);
                    createdHeart.transform.localPosition = new Vector3((i) * 25, 0, 0);
                }
                else // 마지막 하트에선 Half하트트 생성
                {
                    GameObject createdHeart = Instantiate(selectedHeart, hpTransform);
                    createdHeart.transform.localPosition = new Vector3((i) * 25, 0, 0);
                }

            }
        }


        // 만약 플레이어가 맞았어
        // 그러면 hit이이 true가 됨
        // 남아있는 체력에 따른 하트의 관리가 필요함
        // 위 요소들을 통해 하트의 SetActive를 관리하고자 함

    }

    public void AddHeartPrefab()
    {

    }

    public void RemoveHeartPrefab()
    {

    }

    public void UpdateStageText(int stage)
    {
        this.stage.text = stage.ToString();
    }
}
