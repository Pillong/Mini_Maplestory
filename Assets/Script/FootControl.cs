using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootControl : MonoBehaviour 
{
    BoxCollider2D boxcollider;
	void Start()
	{
        boxcollider = gameObject.GetComponent<BoxCollider2D>();
	}
	void Update()
	{
        if (PlayerMove.instance.rigid.velocity.y > 0)
        {
            boxcollider.isTrigger = true;
        }
        else
        {
            boxcollider.isTrigger = false;
        }
	}
	/*void OnCollisionEnter2D(Collision2D other)
    {
        if (PlayerMove.instance.rigid.velocity.y == 0)
        {
            Debug.Log("111");
            if (PlayerMove.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            {
                Debug.Log("222");
                if (other.gameObject.tag.Equals("Terrain"))
                {
                    Debug.Log("333");
                    //other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    other.collider.isTrigger = false;
                }
            }
        }
        else if (PlayerMove.instance.rigid.velocity.y > 0)
        {
            Debug.Log("444");
            if (PlayerMove.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            {
                Debug.Log("555");
                other.collider.isTrigger = true;
            }
        }
    }*/
	
}
