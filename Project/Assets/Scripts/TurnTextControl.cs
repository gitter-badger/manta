using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

public class TurnTextControl : MonoBehaviour {


	public Vector3 largeSize;
	public Vector3 largePos;

	private Vector3 startSize;
	private Vector3 startPos;

	public Text turnText;

	public float expandTime;
	public float waitTime;

	private RectTransform rectTransform;

	private bool isAnimating;

	private Stopwatch stopWatch;

	private Vector3 worldSpaceStartPos;

	// Use this for initialization
	void Start () {
	
		rectTransform = gameObject.GetComponent<RectTransform> ();

		worldSpaceStartPos = rectTransform.position;

		stopWatch = new Stopwatch ();

		rectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
		rectTransform.anchorMax = new Vector2 (0.5f, 0.5f);

		rectTransform.position = worldSpaceStartPos;

		startSize = rectTransform.localScale;
		startPos = rectTransform.anchoredPosition;
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
