using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : BaseUI
{
    [SerializeField] private List<GameObject> skillUpButton;
    [SerializeField] private Transform LevelUp;

    List<GameObject> skillUp = new List<GameObject>();

    protected override UIState GetUIState()
    {
        return UIState.LevelUp;
    }

    void Awake()
    {
        CreatCard();
    } 

    void OnEnable()
    {
        ShowCard();
    }

    public void CreatCard()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(skillUpButton[i], LevelUp);
            card.SetActive(false);
            skillUp.Add(card);
        }
    }

    public void ShowCard()
    {
        MixList();

        for (int i = 0; i < 3; i++)
        {
            skillUp[i].transform.localPosition = new Vector3((i) * 600 - 600, 0, 0);
            skillUp[i].SetActive(true);
        }
    }

    public void InvisibleCard()
    {
        for (int i = 0; i < 5; i++)
        {
            skillUp[i].SetActive(false);
        }
    }

    public void MixList()
    {
        for (int i = 0; i < skillUp.Count; i++)
        {
            int randIndex = Random.Range(i, skillUp.Count);
            GameObject temp = skillUp[i];
            skillUp[i] = skillUp[randIndex];
            skillUp[randIndex] = temp;
        }
    }


    public void ChangeColor(GameObject skillCard, int level)
    {
        Image[] images = skillCard.GetComponentsInChildren<Image>(true);
        List<Image> countImages = new List<Image>();

        foreach (Image img in images)
        {
            if (img.name.Contains("CountImage"))
            {
                countImages.Add(img);
            }
        }

        for (int i = 0; i < countImages.Count; i++)
        {
            if (i <= level)
                countImages[i].color = Color.black;
            else
                countImages[i].color = Color.white;
        }
    }
}
