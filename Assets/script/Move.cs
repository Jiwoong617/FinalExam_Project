using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int speed;
    public int jump_force;
    float x; //이동 입력
    Rigidbody2D rigid;

    void Start()
    {
        speed = 10;
        jump_force = 5;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
    }
}
