using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStarter : MonoBehaviour //애니메이션 재생을 위한 스크립트
{

    public AudioSource intro; // 인트로동안 재생되는 브금
    
    public Animator anim1; // 눈깜빡 화면 애니메이션
    public Animator anim2; // 팀로고 애니메이션
    


    void Start()
    {
        StartCoroutine(PlayAnimations());
    }

    IEnumerator PlayAnimations()
    {
        intro.Play(); //브금 재생
        anim1.SetTrigger("Play1"); // 화면 애니메이션 재생

        yield return new WaitForSeconds(37f); //애니메이션 종료 대기 시간

        anim2.SetTrigger("Play2"); ; // 팀로고 애니메이션

        yield return new WaitForSeconds(9f); // 애니메이션 종료 대기 시간
        intro.Stop(); //브금 중지

        SceneManager.LoadScene(1); // 타이틀 씬 이동


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1); // esc 입력시 애니메이션 스킵하고 타이틀 씬으로 바로 이동

        }


    }


}
