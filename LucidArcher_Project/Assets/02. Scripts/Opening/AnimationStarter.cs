using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStarter : MonoBehaviour //애니메이션 재생을 위한 스크립트
{

    public AudioSource intro; // 인트로동안 재생되는 브금
    
    public Animator anim1; // 눈깜빡 애니메이션
    public Animator anim2; // 팀로고 애니메이션
    


    void Start()
    {
        StartCoroutine(PlayAnimations());
    }

    IEnumerator PlayAnimations()
    {
        intro.Play();
        anim1.SetTrigger("Play1");

        yield return new WaitForSeconds(37f);

        anim2.SetTrigger("Play2"); ;

        yield return new WaitForSeconds(9f);
        intro.Stop();

        SceneManager.LoadScene("IntroMenuScene");


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("IntroMenuScene");

        }


    }


}
