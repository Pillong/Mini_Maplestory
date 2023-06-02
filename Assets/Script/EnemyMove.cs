using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove: MonoBehaviour 
{
	public int nextMove;
	public float movePower = 3.5f;
	public int HealthPoint = 100;
	Rigidbody2D rigid;
	Animator animator;
	SpriteRenderer spriteRenderer;
	bool isHit = false;
	// Use this for initialization
	void Awake () 
	{
		rigid = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		Invoke("Think", 5);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isHit == false)
		{
			Move();
		}
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
	}
	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;
		Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
		//지형 감지
		Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
		RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1.1f, LayerMask.GetMask("Terrain"));
		if (rayHit.collider == null)
		{
			Turn();
		}
		if (nextMove == 1)
		{
			moveVelocity = Vector3.right;
		}
		else if (nextMove == -1)
		{
			moveVelocity = Vector3.left;
		}
		else if (nextMove == 0)
		{
			moveVelocity = Vector3.zero;
		}
		transform.position += moveVelocity * movePower * Time.deltaTime;
	}
	void Think()
	{
		//다음 동작 설정
		nextMove = Random.Range(-1, 2);

		//애니메이션
		animator.SetInteger("WalkSpeed", nextMove);

		//방향전환
		if(nextMove != 0)
		{
			spriteRenderer.flipX = nextMove == 1;
		}
		//재귀함수
		float nextThinkTime = Random.Range(2.0f, 4.0f);
		Invoke("Think", nextThinkTime);
	}

	void Turn()
	{
		nextMove *= -1;
		spriteRenderer.flipX = nextMove == 1;
		CancelInvoke();
		Invoke("Think", 2);
	}
   /* void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Skill"))
        {
			OnDamaged ();
        }
		Invoke ("OffDamaged", 1);
    }*/
	/*void OnDamaged()
	{
		isHit = true;
		HealthPoint -= GameObject.Find ("Player").GetComponent<SkillControl> ().skillDamage;
		if (HealthPoint > 0)
		{
			animator.SetBool("IsHit", true);
			if (PlayerMove.instance.gameObject.transform.localScale == new Vector3(1, 1, 1))
			{
				rigid.AddForce (new Vector2 (-3, 0), ForceMode2D.Impulse);
			}
			if (PlayerMove.instance.gameObject.transform.localScale == new Vector3(-1, 1, 1))
			{
				rigid.AddForce (new Vector2 (3, 0), ForceMode2D.Impulse);
			}
		}
		else if (HealthPoint <= 0)
		{
			nextMove = 0;
			gameObject.layer = 13; //레이어를 EnemyDie로 변경해서 playermove 스크립트에 있는 데미지 조건에 어긋나게 하기
			animator.SetBool("IsDie", true);
			movePower = 0;//복잡하게 별거 안하고 움직임을 차단
		}
	}*/
	void OffDamaged()
	{
		isHit = false;
		animator.SetBool("IsHit", false);

	}
}
