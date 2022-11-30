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
    private bool canjump;
    public GameObject dieEffect;
    public Transform bow;
    public Animator animator;

    void Start()
    {
        speed = 10;
        jump_force = 12;
        canjump = true;
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
                bow.transform.localScale = new Vector3(x,1,1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                rigid.velocity = Vector2.up * jump_force;
                canjump = false;
            }

            if (rigid.velocity.y != 0)
                animator.SetBool("jumpAnim", true);
            else
                animator.SetBool("jumpAnim", false);

        }
        else
        {
            animator.SetFloat("speed", 0);
            animator.SetBool("jumpAnim", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
            canjump = true;
    }

    void Die()
    {
        if (GameManager.instance.hp1.fillAmount <= 0)
        {
            dieEffect.gameObject.SetActive(true);
            dieEffect.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
