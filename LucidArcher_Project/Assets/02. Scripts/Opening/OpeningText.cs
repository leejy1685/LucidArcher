using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningText : MonoBehaviour
{
    public Text subtitleText;        // UI 텍스트
    public string[] lines;           // 자막 내용 배열
    public float[] delays;           // 각 줄 대기 시간
    public float typingSpeed = 0.05f; // 한 글자당 출력 속도

    void Start()
    {
        StartCoroutine(ShowSubtitlesWithTyping());
    }

    IEnumerator ShowSubtitlesWithTyping()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            subtitleText.text = "";
            yield return StartCoroutine(TypeLine(lines[i]));
            yield return new WaitForSeconds(delays[i]);
        }

        subtitleText.text = ""; // 마지막 자막 비우기 (선택)
    }

    IEnumerator TypeLine(string line)
    {
        foreach (char c in line)
        {
            subtitleText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
