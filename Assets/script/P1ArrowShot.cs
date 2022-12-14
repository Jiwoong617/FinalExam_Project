using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1ArrowShot : MonoBehaviour
{
    public GameObject arrow; //생성할 화살
    public Transform bow; //활 위치

    private float power; //발사힘
    private bool click = false; //클릭 확인용

    Vector2 startpos; //마우스 클릭 위치
    Vector2 endpos; //마우스 뗀 위치

    private AudioSource audios; //화살 발사음 출력용 오디오 소스

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(GameManager.instance.player1)
            look_mouse();
    }

    //마우스를 기준으로 회전하게 하는 메소드
    void look_mouse()
    {
        //마우스가 눌렸으면
        if (Input.GetMouseButtonDown(0))
        {
            //클릭 위치를 월드공간 벡터로 반환
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = true; //클릭 true로
        }

        //마우스가 눌리지 않았다면
        if (!click)
        {
            //마우스 위치를 월드공간 벡터로 반환
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //활에서 마우스를 바라보는 벡터 구하기
            Vector2 bow_point = new Vector2(mouse_pos.x - bow.position.x, mouse_pos.y - bow.position.y);

            //활의 오른쪽 벡터를 마우스를 바라보는 벡터로 설정하여 회전 구현
            bow.right = bow_point;
        }

        //마우스가 떼어졌다면
        if (Input.GetMouseButtonUp(0))
        {
            //마우스 클릭이 끝난 위치를 월드공간 벡터로 반환
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(endpos + "" + startpos);

            //끝과 시작의 거리를 구하여 5배를 해준것을 화살 발사 힘으로 설정(드래그가 길 수록 쎄짐)
            power = (endpos-startpos).magnitude*5;

            fire();
            click = false; //마우스를 뗐으니 false

            //화살 따라가기 On
            GameManager.instance.followingArrow = true;
        }

    }

    //화살 발사 메서드
    void fire()
    {
        //화살 발사음 출력
        audios.Play();

        //화살 프리팹을 현재 활 위치에서 생성
        GameObject firearrow = Instantiate(arrow, new Vector3(bow.position.x, bow.position.y, 3), bow.rotation);
        //발사된 화살에게 힘을 부여해서 오른쪽으로 날아가도록
        firearrow.GetComponent<Rigidbody2D>().velocity = firearrow.transform.right * power*0.75f;
    }
}
