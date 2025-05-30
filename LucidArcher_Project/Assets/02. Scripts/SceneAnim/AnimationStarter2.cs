using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStarter2 : MonoBehaviour //애니메이션 재생을 위한 스크립트
{


    public AudioSource main; // 메인화면 브금
    public Animator anim1; // 눈깜빡 애니메이션

    public Animator anim3; // 메인화면 전환 애니메이션

    bool gomain = false; // 키 입력이 true면 키 입력시 다음씬 이동

    void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(PlayAnimations());
    }

    IEnumerator PlayAnimations()
    {

        main.Play();
        yield return new WaitForSeconds(0.5f); //  (오디오 전환과 애니메이션 전환처리가 한 프레임내에 이루어지지 않도록 대기)

        anim1.SetTrigger("Play3");
        yield return new WaitForSeconds(2.0f);
        anim3.SetTrigger("Play4");

        gomain = true; //애니메이션 종료 시 게임 시작 가능

    }

    private void Update()
    {
        if (gomain == true && Input.GetKeyDown(KeyCode.Space)) //스페이스바 입력시 게임씬 이동
        {
            SceneManager.LoadScene(2);

        }


    }


}
