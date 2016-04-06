using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ButtonMouseOver : MonoBehaviour {

	public float startPosY;

	public float mouseOverPosY;

	public float transitionTime;

	public bool mouseIsOver = false;

	private Stopwatch stopWatch;

	private RectTransform rectTransform;

	private float lastLerp;

	// Use this for initialization
	void Start () {
	
		rectTransform = gameObject.GetComponent<RectTransform> ();

		stopWatch = new Stopwatch ();

		stopWatch.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ButtonMove ();
	}

	void ButtonMove()
	{
		if (mouseIsOver)
		{
			rectTransform.anchoredPosition = Vector3.Lerp (new Vector3 (rectTransform.anchoredPosition.x, startPosY, 0), new Vector3 (rectTransform.anchoredPosition.x, mouseOverPosY, 0), (stopWatch.ElapsedMilliseconds / transitionTime) + lastLerp);
		}
		else
		{
			rectTransform.anchoredPosition = Vector3.Lerp (new Vector3 (rectTransform.anchoredPosition.x, mouseOverPosY, 0), new Vector3 (rectTransform.anchoredPosition.x, startPosY, 0), (stopWatch.ElapsedMilliseconds / transitionTime) + lastLerp);
		}
	}

	public void SetMouseOverFalse()
	{
		lastLerp = 1 - Mathf.Clamp01 (stopWatch.ElapsedMilliseconds / transitionTime);

		mouseIsOver = false;

		stopWatch.Reset ();
		stopWatch.Start();
	}

	public void SetMouseOverTrue()
	{
		lastLerp = 1 - Mathf.Clamp01 (stopWatch.ElapsedMilliseconds / transitionTime);

		mouseIsOver = true;

		stopWatch.Reset ();
		stopWatch.Start();
	}
}
