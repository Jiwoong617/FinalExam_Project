using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1ArrowShot : MonoBehaviour
{
    public GameObject arrow; //������ ȭ��
    public Transform bow; //Ȱ ��ġ
    private float power; //�߻���

    private bool click = false;
    Vector2 startpos; //���콺 Ŭ�� ��ġ
    Vector2 endpos; //���콺 �� ��ġ

    private AudioSource audios;

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(GameManager.instance.player1)
            look_mouse();
    }

    void look_mouse()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = true;
        }

        if (!click)
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bow_point = new Vector2(mouse_pos.x - bow.position.x, mouse_pos.y - bow.position.y);

            bow.right = bow_point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(endpos + "" + startpos);
            power = (endpos-startpos).magnitude*5;

            fire();
            click = false;

            //ȭ�� ���󰡱� On
            GameManager.instance.followingArrow = true;
        }

    }

    void fire()
    {
        audios.Play();

        GameObject firearrow = Instantiate(arrow, new Vector3(bow.position.x, bow.position.y, 3), bow.rotation);
        firearrow.GetComponent<Rigidbody2D>().velocity = firearrow.transform.right * power*0.75f;
    }
}
