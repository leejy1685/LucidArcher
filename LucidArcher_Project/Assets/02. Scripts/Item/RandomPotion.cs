using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomPotion : ItemManager
{
    public override void ItemAction(GameObject player)
    {
        // ���� ����

        int random = Random.Range(0, 4);

        switch (random)

        {
            case 0:
                //ü��ȸ��

                break;

            case 1:
                //���ݷ�����
                break;
            case 2:
                //������
                break;
            case 3:
                //�����̻� (����Ű ����, ȭ��, ���ο� ��)
                break;


        }

        Destroy(gameObject);
    }
}
