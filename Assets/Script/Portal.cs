using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour 
{
	private bool isClicked = false;
	private bool GetPortal = false;
	private GameObject Player;
	public GameObject Portal1;
	private void OnTriggerEnter2D(Collider2D collision)//게임오브젝트 인자 받기
	{
		if (collision.CompareTag("Player"))
		{
			GetPortal = true;
			Player = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)//포탈 사용 
    {
        if (isClicked && PlayerMove.instance.UseTeleport == false)
        {
            isClicked = false;
            Player.transform.position = Portal1.transform.position;
            PlayerMove.instance.UseTeleport = true;
            StartCoroutine("PortalCoolTime");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetPortal = false;
    }
    IEnumerator PortalCoolTime ()//포탈 사용 쿨타임 2초
	{
		yield return new WaitForSeconds(1.0f);
		PlayerMove.instance.UseTeleport = false;
	}
    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow) && GetPortal == true)
        {

			isClicked = true;
        }
    }
}