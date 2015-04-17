using UnityEngine;
using System.Collections;

public class SwitchToDark : MonoBehaviour {

	private bool dark = false;
	private SpriteRenderer[] allSprites;

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
