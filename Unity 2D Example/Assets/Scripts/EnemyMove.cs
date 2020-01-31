using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anime;
    SpriteRenderer spriteRenderer;
    public int nextMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //다음 이동방향 확인
        Vector2 direction = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        //디버그용 레이캐스팅
        Debug.DrawRay(direction, Vector3.down, new Color(0, 1, 0));
        //실제 레이캐스팅, Raycast(시작점, 방향, 길이, 충돌 레이어)
        RaycastHit2D rayHit = Physics2D.Raycast(direction, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        anime.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;

        float Timer = Random.Range(2f, 5f);
        Invoke("Think", Timer);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        anime.SetInteger("WalkSpeed", nextMove);
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke();
        Invoke("Think", 5);
    }
}