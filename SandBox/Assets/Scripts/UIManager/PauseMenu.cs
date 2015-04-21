using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool pause = false;
	public GameObject menu;
	private bool firstTouch = true;
	//private SwitchToDark darker;

	//darker = GetComponent<SwitchToDark>();

	void Awake()
	{
		SwitchToDark.Instance.selectAllGo();
		SwitchToDark.Instance.switchColorSpritesToWhite(SwitchToDark.Instance.AllSprites);	

	}

	void Update ()
	{
		if(Input.touchCount == 0 && !pause && !firstTouch && !pause)
		{
			if(!SwitchToDark.Instance.Dark)
			{
				SwitchToDark.Instance.switchColorSpritesToGray(SwitchToDark.Instance.AllSprites);
			}
			menu.SetActive(true);

			Time.timeScale = 0.0f;
			pause = true;
		}
		else if (Input.touchCount >= 2 && firstTouch)
		{
			firstTouch = false;
			if(pause)
			{
				firstTouch = false;
				Time.timeScale = 1.0f;
				pause = false;
			}
		}
	}

	public void BackToGame ()
	{
		SwitchToDark.Instance.selectAllGo();
		SwitchToDark.Instance.switchColorSpritesToWhite(SwitchToDark.Instance.AllSprites);
		firstTouch = true;
		menu.SetActive(false);

	}
}
