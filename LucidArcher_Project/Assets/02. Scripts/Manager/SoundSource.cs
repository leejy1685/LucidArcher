using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;// 소리를 재생할 AudioSource 컴포넌트

    // 효과음을 재생하는 함수
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        // AudioSource가 비어 있으면 가져오기 (최초 한 번)
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke(); // 이전 Invoke 예약이 있다면 취소
        _audioSource.clip = clip; // 재생할 오디오 클립 설정
        _audioSource.volume = soundEffectVolume;// 볼륨 설정
        _audioSource.Play();// 사운드 재생

        // 피치에 약간의 랜덤성을 줘서 같은 사운드라도 다양하게 들리게 함
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        // 사운드 길이 + 여유 시간 이후에 자동 제거
        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        _audioSource.Stop();
        Destroy(this.gameObject);
    }
}
