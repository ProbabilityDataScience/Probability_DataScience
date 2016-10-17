using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Graph : MonoBehaviour {

	public Sprite image;
	public Text[] heightNumbers;
	public Text[] widthNumbers;

	private float minHeightValue;
	private float maxHeightValue;
	private float minWidthValue;
	private float maxWidthValue;
	private int interval;

	private float width, height;
	private List<GameObject> lines = new List<GameObject>();
	private bool hasPreviousPoint;
	private Vector2 previousPoint;
	private int currentInterval;
	private Vector2 sumPoint;

	void Awake()
	{
		width = this.GetComponent<RectTransform>().sizeDelta.x;
		height = this.GetComponent<RectTransform>().sizeDelta.y;
	}

	// 초기화
	public void Init(float minHeightValue, float maxHeightValue, float minWidthValue, float maxWidthValue, int interval = -1)
	{
		// 선들 다 지움
		foreach(GameObject g in lines) {
			Destroy(g);
		}
		lines.Clear();

		this.minHeightValue = minHeightValue;
		this.maxHeightValue = maxHeightValue;
		this.minWidthValue = minWidthValue;
		this.maxWidthValue = maxWidthValue;
		this.interval = interval;

		// 눈금 수치 표시
		for(int i = 0; i < heightNumbers.Length; i++) {
			heightNumbers[i].text = ((maxHeightValue- minHeightValue) / (heightNumbers.Length - 1) * i + minHeightValue) + " -";
		}
		for(int i = 0; i < widthNumbers.Length; i++) {
			widthNumbers[i].text = "l\n" + ((maxWidthValue - minWidthValue) / (widthNumbers.Length - 1) * i + minWidthValue);
		}

		hasPreviousPoint = false;
		currentInterval = 0;
		sumPoint = Vector2.zero;
	}

	// 그래프를 그림
	public void DrawGraph(float widthValue, float heightValue)
	{
		// 값의 점 계산
		Vector2 point;
		point.x = (widthValue - minWidthValue) / (maxWidthValue - minWidthValue) * width - (width / 2.0f);
		point.y = (heightValue - minHeightValue) / (maxHeightValue - minHeightValue) * height - (height / 2.0f);
		
		sumPoint += point;
		currentInterval++;

		// 주어진 interval을 넘은 경우에만 그려줌
		if(interval == -1 || interval <= currentInterval) {
			// 모아둔 점의 평균을 계산
			Vector2 endPoint = sumPoint;
			if(interval != -1)
				 endPoint /= interval;

			// 선 위치 결정
			if(hasPreviousPoint == true) {
				// 선 GameObject 생성
				GameObject line = new GameObject();
				Image lineImage = line.AddComponent<Image>();
				lineImage.sprite = image;

				line.transform.SetParent(this.transform);
				lines.Add(line);

				// 선 위치 계산
				Vector2 diff = endPoint - previousPoint;
				RectTransform r = lineImage.GetComponent<RectTransform>();

				r.sizeDelta = new Vector2(diff.magnitude, 2f);
				r.pivot = new Vector2(0, 0.5f);
				r.anchoredPosition = previousPoint;
				float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				r.rotation = Quaternion.Euler(0, 0, angle);
			}

			hasPreviousPoint = true;
			previousPoint = endPoint;

			// 모아둔 값 초기화
			sumPoint = Vector2.zero;
			currentInterval = 0;
		}
	}
}
