using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //p1, p2, 화살 오브젝트
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    private GameObject arrow;
    private void LateUpdate()
    {
        following();
    }

    //오브젝트 추적
    void following()
    {
        if (GameManager.instance.followingArrow) // 화살 추척
        {
            //Arrow 태그로 화살 추적
            arrow = GameObject.FindGameObjectWithTag("Arrow");

            //화살의 위치로 카메라의 위치 이동
            Vector2 arrowPos = arrow.transform.position;
            transform.position = new Vector3(arrowPos.x, arrowPos.y, -10);
        }
        else if (GameManager.instance.player1) // p1의 차례일 때
        {
            //p1의 위치로 카메라 이동
            transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y + 4f, -10);
            //Debug.Log(1);
        }
        else if (GameManager.instance.player2) // p2 차례일 때
        {
            //p2 위치로 카메라 이동
            transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y + 4f, -10);
            //Debug.Log(2);
        }
    }
}
