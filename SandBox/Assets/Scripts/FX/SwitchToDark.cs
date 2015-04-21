using UnityEngine;
using System.Collections;

public class SwitchToDark : ScriptableObject {

	private bool dark = false;
	private SpriteRenderer[] allSprites;

	private static SwitchToDark instance;
	
	public SwitchToDark ()
	{
		if (instance != null)
		{
			Debug.LogWarning("generate more than one spawn controller, return");
			return;
		}
		instance = this;
	}

	public static SwitchToDark Instance
	{
		get
		{
			if(instance == null)
			{
				ScriptableObject.CreateInstance<SwitchToDark>();
			}
			return instance;
		}
	}

	public bool Dark 
	{
		get{return dark;}
		set{dark=value;}
	}

	public Object[] AllSprites
	{
		get{return allSprites;}
	}
	public void selectAllGo()
	{
		if(!dark)
		{
			allSprites = Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer)) as SpriteRenderer[];
		}
	}

	public void switchColorSpritesToGray(Object[] sprites)
	{
		if(!dark)
		{
			foreach(SpriteRenderer obj in sprites)
			{
				obj.color = Color.gray;
			}
			dark = true;
		}

	}
	public void switchColorSpritesToWhite(Object[] sprites)
	{
		if(dark)
		{
			foreach(SpriteRenderer obj in sprites)
			{
				obj.color = Color.white;
			}
			dark = false;
		}
	}
}
