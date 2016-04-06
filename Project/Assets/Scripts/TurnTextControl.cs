using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

public class TurnTextControl : MonoBehaviour {

	public Vector3 startSize;
	public Vector3 largeSize;
	public Vector3 startPos;
	public Vector3 largePos;

	public Text turnText;

	public float expandTime;
	public float waitTime;

	private RectTransform rectTransform;

	private bool isAnimating;

	private Stopwatch stopWatch;

	// Use this for initialization
	void Start () {
	
		rectTransform = gameObject.GetComponent<RectTransform> ();

		stopWatch = new Stopwatch ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isAnimating)
		{
			if (stopWatch.ElapsedMilliseconds < expandTime)
			{
				rectTransform.localScale = Vector3.Lerp (startSize, largeSize, stopWatch.ElapsedMilliseconds / expandTime);

				rectTransform.anchoredPosition = Vector3.Lerp (startPos, largePos, stopWatch.ElapsedMilliseconds / expandTime);
			}
			else
			{
				if (stopWatch.ElapsedMilliseconds >= expandTime + waitTime)
				{
					rectTransform.localScale = Vector3.Lerp (largeSize, startSize, (stopWatch.ElapsedMilliseconds - waitTime - expandTime) / expandTime);

					rectTransform.anchoredPosition = Vector3.Lerp (largePos, startPos, (stopWatch.ElapsedMilliseconds - waitTime - expandTime) / expandTime);
				}
			}

			if (stopWatch.ElapsedMilliseconds >= (2*expandTime) + waitTime)
			{
				isAnimating = false;

				stopWatch.Reset ();
			}
		}

	}

	public void ChangeToYourTurn()
	{
		turnText.text = "Your Turn";

		isAnimating = true;

		stopWatch.Start ();
	}

	public void ChangeToEnemyTurn()
	{
		turnText.text = "Enemy Turn";

		isAnimating = true;

		stopWatch.Start ();
	}
}
