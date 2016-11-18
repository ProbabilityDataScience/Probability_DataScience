using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class AnimationManager : MonoBehaviour {

	public RectTransform topPanel;
	public RectTransform boardPanel;
	public RectTransform bottomPanel; 

	// Use this for initialization
	void Awake () {
		topPanel.anchoredPosition = new Vector2(0, 770f);
		boardPanel.anchoredPosition = new Vector2(800f, 202f);
		bottomPanel.anchoredPosition = new Vector2(0, -870f);
	}
	
	// 애니메이션 실행
	public void Play()
	{
		topPanel.DOAnchorPosY(600.75f, 0.5f).SetEase(Ease.OutBack);
		boardPanel.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutBack);
		bottomPanel.DOAnchorPosY(-302f, 0.5f).SetEase(Ease.OutBack);
	}
}
