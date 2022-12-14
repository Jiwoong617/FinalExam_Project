using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //p1, p2, ȭ�� ������Ʈ
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    private GameObject arrow;
    private void LateUpdate()
    {
        following();
    }

    //������Ʈ ����
    void following()
    {
        if (GameManager.instance.followingArrow) // ȭ�� ��ô
        {
            //Arrow �±׷� ȭ�� ����
            arrow = GameObject.FindGameObjectWithTag("Arrow");

            //ȭ���� ��ġ�� ī�޶��� ��ġ �̵�
            Vector2 arrowPos = arrow.transform.position;
            transform.position = new Vector3(arrowPos.x, arrowPos.y, -10);
        }
        else if (GameManager.instance.player1) // p1�� ������ ��
        {
            //p1�� ��ġ�� ī�޶� �̵�
            transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y + 4f, -10);
            //Debug.Log(1);
        }
        else if (GameManager.instance.player2) // p2 ������ ��
        {
            //p2 ��ġ�� ī�޶� �̵�
            transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y + 4f, -10);
            //Debug.Log(2);
        }
    }
}
