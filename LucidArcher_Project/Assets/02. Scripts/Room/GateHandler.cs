using UnityEngine;

public class GateHandler : MonoBehaviour
{
    // 상수
    private static readonly int UP = Animator.StringToHash("Up");
    private static readonly int DOWN = Animator.StringToHash("Down");

    // 외부 오브젝트
    [SerializeField] private Animator[] gateAnimator;
    [SerializeField] private GameObject[] gateCollider;

    // 게이트를 열거나 닫음
    public void ControllGate(bool isGateUp)
    {
        for(int i = 0; i < gateAnimator.Length; i++)
        {
            gateAnimator[i].SetTrigger(isGateUp ? UP : DOWN);
            gateCollider[i].SetActive(isGateUp);
        }
    }

    // target 한테 가장 가까운 게이트 반환
    public Transform NearestGate(Vector3 target) 
    {
        Transform gate = gateAnimator[0].gameObject.transform;

        float distance = float.MaxValue;
        foreach (Animator obj in gateAnimator)
        {
            if(distance > (obj.transform.position - target).magnitude)
            {
                distance = (obj.transform.position - target).magnitude;
                gate = obj.transform;
            }
        }

        return gate;
    }
}
