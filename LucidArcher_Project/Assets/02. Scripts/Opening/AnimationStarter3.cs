using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStarter3 : MonoBehaviour //애니메이션 재생을 위한 스크립트
{


    public AudioSource main; // 메인화면 브금
    public Animator anim1; // 눈깜빡 애니메이션
    public bool restart = false;


    void Start()
    {
        StartCoroutine(PlayAnimations());
    }

    IEnumerator PlayAnimations()
    {

        
        yield return new WaitForSeconds(16f); 

        anim1.SetTrigger("Ending");
        yield return new WaitForSeconds(15f);

        restart = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");

        }
        if (Input.GetKeyDown(KeyCode.Space) && restart ==true)
        {
            SceneManager.LoadScene("TitleScene"); 
        }





    }


}
