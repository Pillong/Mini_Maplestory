using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControl : MonoBehaviour 
{
    public GameObject Skill;
    public GameObject Player;
	public int skillDamage = 20;
	Animator animator;
    // Use this for initialization
    void Start () 
    {
		animator = Skill.GetComponentInChildren<Animator> ();
    }

    // Update is called once per frame
    void Update () 
    {
		if (PlayerMove.instance.animator.GetInteger("IsAttack") == 0)
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
                PlayerMove.instance.randomAttack = Random.Range(1, 4);//PlayerMove에 있는 랜덤 공격 함수 실행 및 랜덤공격 변수 재설정
				GameObject.Find("Player").GetComponent<PlayerMove>().Attack();
				if (PlayerMove.instance.gameObject.transform.localScale == new Vector3(1, 1, 1))
				{
					Skill.transform.position = new Vector3(Player.transform.position.x - 2f, Player.transform.position.y, 0);
				}
				else if (PlayerMove.instance.gameObject.transform.localScale == new Vector3(-1, 1, 1))
				{
					Skill.transform.position = new Vector3(Player.transform.position.x + 2.5f, Player.transform.position.y, 0);
				}
				Skill.SetActive(true);
			}
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("DoubleStep")
			         && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
			{
				Skill.SetActive(false);
			}
		}
    }
}
