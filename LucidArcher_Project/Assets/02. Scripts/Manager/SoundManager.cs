using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] public float SoundEffectVolume;// ȿ���� ����
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;// ȿ���� ��ġ ������
    [SerializeField][Range(0f, 1f)] public float MusicVolume;// ��� ���� ����

    private AudioSource musicAudioSource;// ��� ���ǿ� AudioSource
    public AudioClip defalutBGM;// �⺻ ��� ���� Ŭ��
    public AudioClip BattleBGM;//������ ��� ���� Ŭ��

    public SoundSource soundSourcePrefab;// ȿ������ ����� ������ (SoundSource ���)
    private void Awake()
    {
        instance = this;

        // ����� ����� AudioSource ����
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = MusicVolume;
        musicAudioSource.loop = true;
    }

    private void FixedUpdate()
    {
        musicAudioSource.volume = MusicVolume;
    }

    //���� ����
    public void StartBattle()
    {
        ChangeBackGroundMusic(BattleBGM);
    }

    //���� ����
    public void EndBattle()
    {
        ChangeBackGroundMusic(defalutBGM);
    }

    // ��� ������ �ٸ� Ŭ������ ��ü�ϴ� �Լ�
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }


    // ȿ������ ����ϴ� ���� �޼��� (�ܺ� ��𼭵� ȣ�� ����)
    public static void PlayClip(AudioClip clip)
    {
        // SoundSource ������ �ν��Ͻ� ���� �� ���
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.SoundEffectVolume, instance.soundEffectPitchVariance);
    }
}
