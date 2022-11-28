using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P1Move : MonoBehaviour
{
    public float speed;
    public int jump_force;
    float x; //이동 입력
    Rigidbody2D rigid;

    public Animator animator;

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
        if (GameManager.instance.player1)
        {
            x = Input.GetAxisRaw("Horizontal"); 
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            animator.SetFloat("speed", Mathf.Abs(x));

            //Debug.Log(x);
            if (x != 0)
            {
                transform.localScale = new Vector3(x*2.5f,2.5f,1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0)
                rigid.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        if (GameManager.instance.hp1.fillAmount <= 0)
            Destroy(gameObject);
    }
}
