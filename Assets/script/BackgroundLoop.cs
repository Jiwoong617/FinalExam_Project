using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //움직이는 구름 구현

    //끝지점
    public float endPoint = 0;
    void Update()
    {
        //초마다 10씩 왼쪽으로 이동
        transform.Translate(Vector3.left * 10 * Time.deltaTime);

        //끝지점에 다다랐으면 위치 초기화
        if (transform.position.x <= endPoint)
            transform.position = new Vector3(245, 15, 1);
    }
}
