using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStarter : MonoBehaviour //애니메이션 재생을 위한 스크립트
{
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;

    bool gomain = false; // 키 입력이 true면 키 입력시 다음씬 이동
    void Start()
    {
        StartCoroutine(PlayAnimations());
    }

    IEnumerator PlayAnimations()
    {
        anim1.SetTrigger("Play1");

        yield return new WaitForSeconds(37f);

        anim2.SetTrigger("Play2"); ;

        yield return new WaitForSeconds(9f);
        anim1.SetTrigger("Play3");
        yield return new WaitForSeconds(1.5f);
        anim3.SetTrigger("Play4");

        gomain = true;

    }

    private void Update()
    {
        if (gomain == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TestMain");

        }
    }


}
