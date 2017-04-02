using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	private string currentActiveGroup;

	public void HideAll()
	{
		for(int i=0; i<transform.childCount; ++i)
		{
			var child = transform.GetChild(i);
			child.gameObject.SetActive(false);
		}
	}

	public void SwitchTo(string groupName)
	{

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
