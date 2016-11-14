using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Square : MonoBehaviour {

	public Sprite[] symbolSprites;
	public RectTransform symbolList;
	public Image[] symbols;

	//이 상자의 보석 번호
	private int square_Data;
	private int free_Count;

	private Sequence twinSeq;
	private bool isEnd = true;
	private int endNum=0;
	private int currentSymbolIndex = 0;

	void Start()
	{
		// DOTween Sequence 초기화
		twinSeq = DOTween.Sequence();
		twinSeq.Append(symbolList.DOAnchorPosY(-120f, 0.05f).SetEase(Ease.Linear).OnComplete(EndFirstSymbol));
		twinSeq.Append(symbolList.DOAnchorPosY(-240f, 0.05f).SetEase(Ease.Linear).OnComplete(EndSecondSymbol));
		twinSeq.Append(symbolList.DOAnchorPosY(-360f, 0.05f).SetEase(Ease.Linear).OnComplete(EndThirdSymbol));
		twinSeq.SetLoops(-1, LoopType.Restart);

		ChangeSymbol(0, Random.Range(0, symbolSprites.Length));
	}

	//데이터셋에서 번호 가져오기
	public void Get_Data()
	{
		if (free_Count <= 0) 
			square_Data = DataSet.DataSet_Get (1,ref free_Count);
		else
			free_Count--;
	}

	//보석 번호 반환 해주기
	public int Get_Square_Data()
	{
		return square_Data;
	}


	private void ChangeSymbol(int index, int symbolNum)
	{
		if(index == 0)
			symbols[3].sprite = symbolSprites[symbolNum];

		symbols[index].sprite = symbolSprites[symbolNum];
	}

	// --------- 애니메이션 관련 함수들

	// 보석 애니메이션 재생
	public void RunSymbol()
	{
		isEnd = false;

		ChangeSymbol(0, endNum);
		if(!twinSeq.IsActive())
			twinSeq.Play();
		else
			twinSeq.Restart();
	}
	// 보석 애니메이션 중단
	public void StopSymbol(int num)
	{
		isEnd = true;
		endNum = num;
	}

	// 첫 번째 심볼 끝남
	private void EndFirstSymbol()
	{
		currentSymbolIndex = 1;
		if(isEnd == false)
			ChangeSymbol(2, Random.Range(0, symbolSprites.Length));
		else {
			ChangeSymbol(2, endNum);

			twinSeq.Pause();
			symbolList.DOAnchorPosY(-240, 0.35f).SetEase(Ease.OutBack);
		}
	}
	// 두 번째 심볼 끝남
	private void EndSecondSymbol()
	{
		currentSymbolIndex = 2;
		if(isEnd == false)
			ChangeSymbol(0, Random.Range(0, symbolSprites.Length));
		else {
			ChangeSymbol(0, endNum);

			twinSeq.Pause();
			symbolList.DOAnchorPosY(-360, 0.35f).SetEase(Ease.OutBack);
		}
	}
	// 세 번째 심볼 끝남
	private void EndThirdSymbol()
	{
		currentSymbolIndex = 0;
		if(isEnd == false)
			ChangeSymbol(1, Random.Range(0, symbolSprites.Length));
		else {
			ChangeSymbol(1, endNum);

			twinSeq.Pause();
			symbolList.anchoredPosition = new Vector2(0f, 0f);
			symbolList.DOAnchorPosY(-120, 0.35f).SetEase(Ease.OutBack);
		}
	}
}
