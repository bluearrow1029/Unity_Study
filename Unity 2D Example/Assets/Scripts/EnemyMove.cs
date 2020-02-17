using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anime;
    SpriteRenderer sprite;
    public int nextMove;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Move",5);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //다음 이동방향 확인
        Vector2 direction = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);

        //디버그용 레이캐스팅
        Debug.DrawRay(direction, Vector3.down, new Color(0, 1, 0));

        //실제 레이캐스팅, Raycast(시작점, 방향, 길이, 충돌 레이어)
        RaycastHit2D rayHit = Physics2D.Raycast(direction, Vector2.down, 1, LayerMask.GetMask("Platform"));
        
        if(rayHit.collider == null)
            Turn();
    }

    // 이동방향 함수
    void Move()
    {
        nextMove = Random.Range(-1, 2);
        anime.SetInteger("WalkSpeed", nextMove);

        if (nextMove == -1)
            sprite.flipX = true;
        else if (nextMove == 1)
            sprite.flipX = false;

        Invoke("Move", 5);
    }

    // 방향전환 함수
    void Turn()
    {
        // 이동 방향 반대로 변경
        nextMove = nextMove * -1;
        anime.SetInteger("WalkSpeed", nextMove);

        if (nextMove == -1)
            sprite.flipX = true;
        else if (nextMove == 1)
            sprite.flipX = false;

        CancelInvoke();
        Invoke("Move", 5);
    }
}