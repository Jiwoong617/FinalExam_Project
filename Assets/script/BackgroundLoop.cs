using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //�����̴� ���� ����

    //������
    public float endPoint = 0;
    void Update()
    {
        //�ʸ��� 10�� �������� �̵�
        transform.Translate(Vector3.left * 10 * Time.deltaTime);

        //�������� �ٴٶ����� ��ġ �ʱ�ȭ
        if (transform.position.x <= endPoint)
            transform.position = new Vector3(245, 15, 1);
    }
}
