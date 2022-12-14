using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1ArrowShot : MonoBehaviour
{
    public GameObject arrow; //������ ȭ��
    public Transform bow; //Ȱ ��ġ

    private float power; //�߻���
    private bool click = false; //Ŭ�� Ȯ�ο�

    Vector2 startpos; //���콺 Ŭ�� ��ġ
    Vector2 endpos; //���콺 �� ��ġ

    private AudioSource audios; //ȭ�� �߻��� ��¿� ����� �ҽ�

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(GameManager.instance.player1)
            look_mouse();
    }

    //���콺�� �������� ȸ���ϰ� �ϴ� �޼ҵ�
    void look_mouse()
    {
        //���콺�� ��������
        if (Input.GetMouseButtonDown(0))
        {
            //Ŭ�� ��ġ�� ������� ���ͷ� ��ȯ
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = true; //Ŭ�� true��
        }

        //���콺�� ������ �ʾҴٸ�
        if (!click)
        {
            //���콺 ��ġ�� ������� ���ͷ� ��ȯ
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Ȱ���� ���콺�� �ٶ󺸴� ���� ���ϱ�
            Vector2 bow_point = new Vector2(mouse_pos.x - bow.position.x, mouse_pos.y - bow.position.y);

            //Ȱ�� ������ ���͸� ���콺�� �ٶ󺸴� ���ͷ� �����Ͽ� ȸ�� ����
            bow.right = bow_point;
        }

        //���콺�� �������ٸ�
        if (Input.GetMouseButtonUp(0))
        {
            //���콺 Ŭ���� ���� ��ġ�� ������� ���ͷ� ��ȯ
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(endpos + "" + startpos);

            //���� ������ �Ÿ��� ���Ͽ� 5�踦 ���ذ��� ȭ�� �߻� ������ ����(�巡�װ� �� ���� ����)
            power = (endpos-startpos).magnitude*5;

            fire();
            click = false; //���콺�� ������ false

            //ȭ�� ���󰡱� On
            GameManager.instance.followingArrow = true;
        }

    }

    //ȭ�� �߻� �޼���
    void fire()
    {
        //ȭ�� �߻��� ���
        audios.Play();

        //ȭ�� �������� ���� Ȱ ��ġ���� ����
        GameObject firearrow = Instantiate(arrow, new Vector3(bow.position.x, bow.position.y, 3), bow.rotation);
        //�߻�� ȭ�쿡�� ���� �ο��ؼ� ���������� ���ư�����
        firearrow.GetComponent<Rigidbody2D>().velocity = firearrow.transform.right * power*0.75f;
    }
}
