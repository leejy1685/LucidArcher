using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombard : MonoBehaviour
{
    public SpriteRenderer fallingBomb;
    Vector3 fallingBombDestPosition;
    public SpriteRenderer warningArea;

    public ParticleSystem effect;
    public GameObject collisionObject;
    public PoolElement poolComponent;

    public float fallingDuration;
    public float fallingSpeed;

    float elapsedTime;
    Color warningAreaColor;
    float warningAreaColorAlpha;

    LayerMask playerMask;
    // Start is called before the first frame update
    private void Awake()
    {
        playerMask = LayerMask.GetMask("Player");
        fallingBombDestPosition = fallingBomb.transform.localPosition;
        warningAreaColor = warningArea.color;
    }
    void OnEnable()
    {
        warningAreaColorAlpha = warningAreaColor.a;
        fallingBomb.transform.localPosition = fallingBombDestPosition + new Vector3(0, fallingDuration * fallingSpeed, 0);
        elapsedTime = 0;
        fallingBomb.gameObject.SetActive(true);
        warningArea.gameObject.SetActive(true);
        effect.gameObject.SetActive(false);
        collisionObject.SetActive(false);
        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < fallingDuration)
        {
            elapsedTime += Time.deltaTime;
            fallingBomb.transform.position -= new Vector3(0, Time.deltaTime * fallingSpeed);
            warningAreaColor.a = Mathf.PingPong(elapsedTime + warningAreaColorAlpha, warningAreaColorAlpha) + 0.1f;
            warningArea.color = warningAreaColor;   
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(fallingDuration);

        fallingBomb.gameObject.SetActive(false);
        warningArea.gameObject.SetActive(false);
        effect.gameObject.SetActive(true);
        effect.Play();

        collisionObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        collisionObject.SetActive(false);

        yield return new WaitForSeconds(effect.main.duration - 0.1f);
        poolComponent.Retrive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerMask | 1 << collision.gameObject.layer) == playerMask)
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}
