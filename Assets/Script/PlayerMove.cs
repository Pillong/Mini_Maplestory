using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public static PlayerMove instance;//다른 스크립트에서 이 스크립트의 public 변수를 사용할 수 있게 instance 선언. 이 기능이 싱글톤이라는 기능이다.
	//사용 방법은 playermove.instance.원하는 인자값 - 으로 사용하면된다
    public float movePower = 3.5f;
    public float jumpPower = 6f;
	public bool UseTeleport = false;
    public Animator animator;
    public Rigidbody2D rigid;
    public int randomAttack;
    SpriteRenderer spriteRenderer;
	bool isHit = false; //피격 bool값
    // Use this for initialization
	void Awake() //instance this로 인자 받기->이거 재민이 형이 알려준 존나 혁신 기능이다. 다만 player같은 단 하나만 있는 스크립트에만 써야 한다.
	{ 
		instance = this; 
	}
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
		Move();
		Jump();
		if (Input.GetKey(KeyCode.LeftControl) && 
			!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack" + randomAttack)) // 키 입력 받을때에만 randomAttack 랜덤으로 설정
			//그래서 키 입력 자체를 업데이트 함수로 올림
		{
			randomAttack = Random.Range(1, 4);
			Attack();
		}
        if (rigid.velocity.y < 0)//점프를 하지 않고 지형에서 떨어질 시 점프모션으로 변경
        {
            animator.SetBool("IsJump", true);
        }
        else if (rigid.velocity.y == 0) //착지 시 idle로 변경
        {
            animator.SetBool("IsJump", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack" + randomAttack) 
			&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)//공격모션 랜덤으로 변경
        {
            animator.SetInteger("IsAttack", 0);
        }
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack" + randomAttack))
        {
			if (isHit == false)
			{
				if (Input.GetAxisRaw("Horizontal") < 0)
				{
					moveVelocity = Vector3.left;
					transform.localScale = new Vector3(1, 1, 1);//게임오브젝트 자체를 뒤집는 방법
					animator.SetBool("IsWalk", true);

				}
				else if (Input.GetAxisRaw("Horizontal") > 0)
				{
					moveVelocity = Vector3.right;
					transform.localScale = new Vector3(-1, 1, 1);
					animator.SetBool("IsWalk", true);
				}
				else if (Input.GetAxisRaw("Horizontal") == 0)
				{
					animator.SetBool("IsWalk", false);
				}
				transform.position += moveVelocity * movePower * Time.deltaTime;
			}
        }
    }


    void Jump()
    {
		if (isHit == false)
		{
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack" + randomAttack))
			{
				if (Input.GetKeyDown(KeyCode.LeftAlt) && !animator.GetBool("IsJump"))
				{
					rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
					animator.SetBool("IsJump", true);
				}
			}
		}
    }

    public void Attack()
    {
		if (isHit == false)
		{
			if (animator.GetInteger("IsAttack") == 0)
			{
				animator.SetInteger("IsAttack", randomAttack);
			}
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("Enemy") && other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))//레이어와 태그가 모두 같으면 데미지 판정
		{
            OnDamaged(other.transform.position);
		}
	}
    void OnDamaged(Vector2 targetPos) //피격 이벤트
    {
		isHit = true;
        //레이어 변경
        gameObject.layer = 12;
        //피격 시 이미지 반투명화
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //살짝 밀려나기
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);
		StartCoroutine("PlayerHit");
        Invoke("OffDamaged", 2);
    }
    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
	IEnumerator PlayerHit()
	{
		yield return new WaitForSeconds(0.7f);
		isHit = false;
	}
}
