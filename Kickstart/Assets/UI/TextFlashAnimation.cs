using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlashAnimation : MonoBehaviour
{
	public float MaxAlpha = 1;
	public float MinAlpha = 0;
	public float frequency = 1.0f;

	private Text text;
	private float time = 0;

	private void Start()
	{
		text = GetComponent<Text>();
	}

	private void Update()
	{
		time += Time.deltaTime;

		Color color = text.color;
		color.a = Mathf.Min((Mathf.Sin(2 * Mathf.PI * frequency * time) + 1.0f) / 2.0f, 0.99f);
		text.color = color;
	}
}
