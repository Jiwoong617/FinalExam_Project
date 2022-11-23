using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P2Move : MonoBehaviour
{
    public float speed;
    public int jump_force;
    float x; //�̵� �Է�
    Rigidbody2D rigid;

    void Start()
    {
        speed = 10;
        jump_force = 10;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Moving();
        Die();
    }

    void Moving()
    {
        if (GameManager.instance.player2)
        {
            x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0)
                rigid.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        if (GameManager.instance.hp2.fillAmount <= 0)
            Destroy(gameObject);
    }
}