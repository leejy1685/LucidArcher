using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoplayParticleOnEnable : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] float maxLifeTime;


    private void OnEnable()
    {
        ps.Play();
        StartCoroutine(DisableSelf());
    }

    IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(maxLifeTime);
        gameObject.SetActive(false);        
    }
    private void OnDisable()
    {
        ps.Clear();
    }
}
