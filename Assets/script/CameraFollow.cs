using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    private GameObject arrow;
    private void Update()
    {
        following();
    }

    void following()
    {
        if (GameManager.instance.followingArrow) // 화살
        {
            arrow = GameObject.FindGameObjectWithTag("Arrow");
            Vector2 arrowPos = arrow.transform.position;
            transform.position = new Vector3(arrowPos.x, arrowPos.y, -10);
            //Debug.Log("Arrow");
        }
        else if (GameManager.instance.player1) // p1 따라가기
        {
            transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y, -10);
            //Debug.Log(1);
        }
        else if (GameManager.instance.player2) // p2 따라가기
        {
            transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y, -10);
            //Debug.Log(2);
        }

    }
}
