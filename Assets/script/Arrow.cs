using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool rotate; //회전

    private AudioSource audios;

    private void Start()
    {
        rotate = true;
        rb = GetComponent<Rigidbody2D>();
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        //아직 화살이 맞지 않았으면 속도로 회전 및 이동 방향 주기
        if(rotate)
            transform.right = GetComponent<Rigidbody2D>().velocity;

        destroyArrow();
    }

    //화살 피격시 효과음과 이펙트 출력을 위해
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            GameManager.instance.hpDecrease(2); //p2의 hp 감소
            GetComponent<ParticleSystem>().Play(); //푸확 하고 터지느 이펙트 발생시키기
            audios.Play(); //피격음 출력
        }
    }

    //화살의 관통을 구현하기 위해 Exit으로 구현
    private void OnTriggerExit2D(Collider2D collision)
    {
        //P2에 맞았으면
        if (collision.tag == "Player2")
        {
            stuckArrow(); //화살 고정
            transform.SetParent(collision.transform); //몸에 화살 박히는거
             //p2를 부모로 설정함으로 p2이동 시 박힌 화살도 같이 움직이게
        }
        else if (collision.tag == "Land")
            stuckArrow(); //땅에 맞았으면 화살만 고정

        //턴 변경
        GameManager.instance.player1 = false;
        GameManager.instance.player2 = true;
    }

    //잠시 대기
    private void wait()
    {
        //화살이 박히고 바로 카메라가 넘어가지 않고 1초뒤 넘어가도록
        GameManager.instance.followingArrow = false;
        gameObject.tag = "Stick"; //화살의 태그를 Stick으로 변경
                                  //카메라가 Arrow를 따라가야되니 Arrow는 하나여야 된다

        //화살의 콜라이더 비활성화로 오류 제거 (다중 출돌이 발생하지 않도록)
        GetComponent<Collider2D>().enabled = false;
    }

    //화살 고정시키는 메서드
    private void stuckArrow()
    {
        rb.isKinematic = true; //화살 고정
        rotate = false; //회전 고정
        rb.velocity = Vector2.zero; //속도 삭제

        Invoke("wait", 1); //1초뒤 wait 메서드 실행
    }

    //범위 벗어날 시 화살 파괴 메서드
    private void destroyArrow()
    {
        //화살의 위치가 -20 미만이면 턴 넘기고 오브젝트 파괴
        if(transform.position.y < -20f)
        {
            GameManager.instance.player1 = false;
            GameManager.instance.player2 = true;

            GameManager.instance.followingArrow = false;
            Destroy(gameObject);
        }
    }
}
