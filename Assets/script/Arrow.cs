using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool rotate; //ȸ��

    private AudioSource audios;

    private void Start()
    {
        rotate = true;
        rb = GetComponent<Rigidbody2D>();
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        //���� ȭ���� ���� �ʾ����� �ӵ��� ȸ�� �� �̵� ���� �ֱ�
        if(rotate)
            transform.right = GetComponent<Rigidbody2D>().velocity;

        destroyArrow();
    }

    //ȭ�� �ǰݽ� ȿ������ ����Ʈ ����� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            GameManager.instance.hpDecrease(2); //p2�� hp ����
            GetComponent<ParticleSystem>().Play(); //ǪȮ �ϰ� ������ ����Ʈ �߻���Ű��
            audios.Play(); //�ǰ��� ���
        }
    }

    //ȭ���� ������ �����ϱ� ���� Exit���� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        //P2�� �¾�����
        if (collision.tag == "Player2")
        {
            stuckArrow(); //ȭ�� ����
            transform.SetParent(collision.transform); //���� ȭ�� �����°�
             //p2�� �θ�� ���������� p2�̵� �� ���� ȭ�쵵 ���� �����̰�
        }
        else if (collision.tag == "Land")
            stuckArrow(); //���� �¾����� ȭ�츸 ����

        //�� ����
        GameManager.instance.player1 = false;
        GameManager.instance.player2 = true;
    }

    //��� ���
    private void wait()
    {
        //ȭ���� ������ �ٷ� ī�޶� �Ѿ�� �ʰ� 1�ʵ� �Ѿ����
        GameManager.instance.followingArrow = false;
        gameObject.tag = "Stick"; //ȭ���� �±׸� Stick���� ����
                                  //ī�޶� Arrow�� ���󰡾ߵǴ� Arrow�� �ϳ����� �ȴ�

        //ȭ���� �ݶ��̴� ��Ȱ��ȭ�� ���� ���� (���� �⵹�� �߻����� �ʵ���)
        GetComponent<Collider2D>().enabled = false;
    }

    //ȭ�� ������Ű�� �޼���
    private void stuckArrow()
    {
        rb.isKinematic = true; //ȭ�� ����
        rotate = false; //ȸ�� ����
        rb.velocity = Vector2.zero; //�ӵ� ����

        Invoke("wait", 1); //1�ʵ� wait �޼��� ����
    }

    //���� ��� �� ȭ�� �ı� �޼���
    private void destroyArrow()
    {
        //ȭ���� ��ġ�� -20 �̸��̸� �� �ѱ�� ������Ʈ �ı�
        if(transform.position.y < -20f)
        {
            GameManager.instance.player1 = false;
            GameManager.instance.player2 = true;

            GameManager.instance.followingArrow = false;
            Destroy(gameObject);
        }
    }
}
