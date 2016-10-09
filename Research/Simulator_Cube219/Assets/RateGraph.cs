using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RateGraph : MonoBehaviour {

	public Sprite image;

	private int endCount;
	private int interval;

	private float width, height;
	private List<GameObject> lines = new List<GameObject>();
	private bool hasPreviousPoint;
	private Vector2 previousPoint;

	void Awake()
	{
		width = this.GetComponent<RectTransform>().sizeDelta.x;
		height = this.GetComponent<RectTransform>().sizeDelta.y;
		Debug.Log(width);
		Debug.Log(height);

		Init(30000);
		DrawGraph(80.0f, 5000);
	}

	// 초기화
	public void Init(int endCount, int interval = -1)
	{
		// 선들 다 지움
		foreach(GameObject g in lines) {
			Destroy(g);
		}
		lines.Clear();

		this.endCount = endCount;
		this.interval = interval;

		hasPreviousPoint = false;
	}

	// 그래프를 그림
	public void DrawGraph(float rate, int tryCount)
	{
		// 선 GameObject 생성
		GameObject line = new GameObject();
		Image lineImage = line.AddComponent<Image>();
		lineImage.sprite = image;

		line.transform.parent = this.transform;
		lines.Add(line);

		// 값의 점 계산
		Vector2 endPoint;
		endPoint.x = (float)tryCount / (float)endCount * width - (width / 2.0f);
		endPoint.y = rate / 100.0f * height - (height / 2.0f);

		// 선 위치 결정
		if(hasPreviousPoint == true) {
			// line.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

		}

		hasPreviousPoint = true;
		previousPoint = endPoint;

	}
}
