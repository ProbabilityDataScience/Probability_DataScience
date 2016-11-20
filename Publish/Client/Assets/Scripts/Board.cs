using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Board : MonoBehaviour {

	//보드안 보석이 표시되는 상자
	public Square[] Square_Line_1 = new Square[5];
	public Square[] Square_Line_2 = new Square[5];
	public Square[] Square_Line_3 = new Square[5];
	public Square[] Square_Line_4 = new Square[5];
	public Square[] Square_Line_5 = new Square[5];
	// 라인
	public Image[] horizontalLines;
	public Image[] verticalLines;
	public Image[] crossLines;
	// WinPanel과 EarnNum
	public Image winPanel;
	public CustomNumber earnNum;
	//라인 비교 확인 카운트 변수
	int checked_Line_Count;
	//확인된 라인
	public bool[] checked_Line = new bool[12];
	int run_Count = 0;

	public bool isRun = false;

	void Awake()
	{
		// 라인들 다 안보이게 함
		HideAllLines();
	}

	// 모든 라인들 숨김
	private void HideAllLines()
	{
		foreach(Image l in horizontalLines) {
			l.GetComponent<CanvasGroup>().alpha = 0;
		}
		foreach(Image l in verticalLines) {
			l.GetComponent<CanvasGroup>().alpha = 0;
		}
		foreach(Image l in crossLines) {
			l.GetComponent<CanvasGroup>().alpha = 0;
		}
		winPanel.GetComponent<CanvasGroup>().alpha = 0;
	}

	// 시작
	public void Run()
	{
		if(isRun == false) {
			isRun = true;
			HideAllLines();

			run_Count++;
			Get_Selected_Gem_Number ();
			Square_Get_Data();
			Print_Square_Number();
			Check_All_Data();

			for(int i = 0; i < 5; i++) {
				Square_Line_1[i].RunSymbol();
				Square_Line_2[i].RunSymbol();
				Square_Line_3[i].RunSymbol();
				Square_Line_4[i].RunSymbol();
				Square_Line_5[i].RunSymbol();
			}
			int get = Money_Return() * GameManager.m.bet_Money_Multi;
			GameManager.m.total_Money += get;
			StartCoroutine(Wait_Stop(2));

		}
	}
	private IEnumerator Wait_Stop(float _time)
	{
		yield return new WaitForSeconds(_time);
		Stop();
	}

	public int Money_Return()
	{
		int return_Multi = 0;

		for(int i = 0; i < checked_Line.Length; i++) {
			if(checked_Line[i] == true)
				return_Multi++;
		}

		return return_Multi;
	}

	private void Get_Selected_Gem_Number()
	{
		DataSet.selected_Gem = DataSet.SelectSet_Get (DataSet.gem_SelectSet_Count);
		Debug.Log ("Circle " + run_Count + " selected Gem Number : " + DataSet.selected_Gem);
	}

	// 종료
	private void Stop()
	{
		StartCoroutine(StopAni());
	}
	// 보석들이 종료되는 애니메이션
	private IEnumerator StopAni()
	{
		// 라인1
		for(int i = 0; i < 5; i++) {
			Square_Line_1[i].StopSymbol(Square_Line_1[i].Get_Square_Data());
			yield return new WaitForSeconds(0.06f);
		}
		// 라인2
		for(int i = 0; i < 5; i++) {
			Square_Line_2[i].StopSymbol(Square_Line_2[i].Get_Square_Data());
			yield return new WaitForSeconds(0.06f);
		}
		// 라인3
		for(int i = 0; i < 5; i++) {
			Square_Line_3[i].StopSymbol(Square_Line_3[i].Get_Square_Data());
			yield return new WaitForSeconds(0.06f);
		}
		// 라인4
		for(int i = 0; i < 5; i++) {
			Square_Line_4[i].StopSymbol(Square_Line_4[i].Get_Square_Data());
			yield return new WaitForSeconds(0.06f);
		}
		// 라인5
		for(int i = 0; i < 5; i++) {
			Square_Line_5[i].StopSymbol(Square_Line_5[i].Get_Square_Data());
			yield return new WaitForSeconds(0.06f);
		}
		
		// 라인 애니메이션 실행
		yield return LineAni();
		isRun = false;
	}
	// 라인들이 표시되는 애니메이션
	private IEnumerator LineAni()
	{
		bool[] isShow_horizontal = { false, false, false, false, false };
		bool[] isShow_vertical = { false, false, false, false, false };
		bool[] isShow_cross = { false, false};
		bool isHas = false;

		// 한 배열에 있는 CheckLine을 가로/세로/대각선으로 나눔
		// 가로
		for(int i = 0; i < 5; i++) {
			if(checked_Line[i] == true) {
				isShow_horizontal[i] = true;
				isHas = true;
			}
		}
		// 세로
		for(int i = 5; i < 10; i++) {
			if(checked_Line[i] == true) {
				isShow_vertical[i - 5] = true;
				isHas = true;
			}
		}
		// 대각선
		for(int i = 10; i < 12; i++) {
			if(checked_Line[i] == true) {
				isShow_cross[i - 10] = true;
				isHas = true;
			}
		}
		// 라인이 한 개라도 있는가?
		if(isHas == true) {
			yield return new WaitForSeconds(0.5f);

			// 해당하는 라인 보여줌
			for(int i = 0; i < 5; i++) {
				if(isShow_horizontal[i] == true) {
					horizontalLines[i].GetComponent<CanvasGroup>().DOFade(1f, 0.6f);
				}
				if(isShow_vertical[i] == true) {
					verticalLines[i].GetComponent<CanvasGroup>().DOFade(1f, 0.6f);
				}
			}
			for(int i = 0; i < 2; i++) {
				if(isShow_cross[i] == true)
					crossLines[i].GetComponent<CanvasGroup>().DOFade(1f, 0.6f);
			}
			yield return new WaitForSeconds(0.8f);

			// WinPanel 보여줌
			winPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.4f);
			earnNum.ChangeNum(0, true);
			yield return new WaitForSeconds(0.6f);

			// 얻은 돈을 계산하고 EarnNum에 보여줌
			int get = Money_Return() * GameManager.m.bet_Money_Multi;
			GameManager.m.creditNum.ChangeNum(GameManager.m.total_Money);
			earnNum.ChangeNum(get);
		}
	}

	//라인 비교
	void Check_Data(Square[] Squares)
	{
		bool all_Line_Same = true;

		int firstSqareNum = Squares[0].Get_Square_Data();

		for (int i = 1; i < 5; i++) 
		{
			if (Squares[i].Get_Square_Data() != 6) 
			{
				if(Squares[i].Get_Square_Data() != firstSqareNum) {
					all_Line_Same = false;
					break;
				}
			}
		}

		if (all_Line_Same)
			checked_Line [checked_Line_Count] = true;
		checked_Line_Count++;
	}

	private void Vertical_Line_Check()
	{
		Square[] Squares = new Square[5];

		for (int i = 0; i < 5; i++)
			Squares [i] = Square_Line_1 [i];
		Check_Data (Squares);

		for (int i = 0; i < 5; i++)
			Squares [i] = Square_Line_2 [i];
		Check_Data (Squares);

		for (int i = 0; i < 5; i++)
			Squares [i] = Square_Line_3 [i];
		Check_Data (Squares);

		for (int i = 0; i < 5; i++)
			Squares [i] = Square_Line_4 [i];
		Check_Data (Squares);

		for (int i = 0; i < 5; i++)
			Squares [i] = Square_Line_5 [i];
		Check_Data (Squares);
	}

	Square[] Return_Horizental_Line(int lineNumber)
	{
		Square[] Squares = new Square[5];
		Squares [0] = Square_Line_1 [lineNumber];
		Squares [1] = Square_Line_2 [lineNumber];
		Squares [2] = Square_Line_3 [lineNumber];
		Squares [3] = Square_Line_4 [lineNumber];
		Squares [4] = Square_Line_5 [lineNumber];

		return Squares;
	}

	private void Diagonal_Line_Check(Square A, Square B, Square C, Square D, Square E)
	{
		Square[] Squares = new Square[5];
		Squares [0] = A;
		Squares [1] = B;
		Squares [2] = C;
		Squares [3] = D;
		Squares [4] = E;
		Check_Data (Squares);
			
	}

	private void Horizental_Line_Check()
	{
		Square[] Squares;

		int horizental_Line = 0;

		for (int i = 0; i < 5; i++) 
		{
			Squares = Return_Horizental_Line (horizental_Line);
			Check_Data (Squares);
			horizental_Line++;
		}
	}

	//모든 라인 데이터 비교
	private void Check_All_Data()
	{
		for (int i = 0; i < checked_Line.Length; i++)
			checked_Line [i] = false;
		
		checked_Line_Count = 0;

		Vertical_Line_Check ();

		//Horizental Line Check
		Horizental_Line_Check();

		//Diagonal Line 1 Check
		Diagonal_Line_Check(Square_Line_1[0],Square_Line_2[1],Square_Line_3[2],Square_Line_4[3],Square_Line_5[4]);

		//Diagonal Line 2 Check
		Diagonal_Line_Check(Square_Line_1[4],Square_Line_2[3],Square_Line_3[2],Square_Line_4[1],Square_Line_5[0]);

		Print_Check_Line ();
	}


	//각 상자 보석 번호 출력
	private void Print_Square_Number()
	{
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Line 1 - " + Square_Line_1 [0].Get_Square_Data () + " : " + Square_Line_1 [1].Get_Square_Data () + " : " + Square_Line_1 [2].Get_Square_Data () + " : " +
			Square_Line_1 [3].Get_Square_Data () + " : " + Square_Line_1 [4].Get_Square_Data ());
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Line 2 - " + Square_Line_2 [0].Get_Square_Data () + " : " + Square_Line_2 [1].Get_Square_Data () + " : " + Square_Line_2 [2].Get_Square_Data () + " : " +
			Square_Line_2 [3].Get_Square_Data () + " : " + Square_Line_2 [4].Get_Square_Data ());
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Line 3 - " + Square_Line_3 [0].Get_Square_Data () + " : " + Square_Line_3 [1].Get_Square_Data () + " : " + Square_Line_3 [2].Get_Square_Data () + " : " +
			Square_Line_3 [3].Get_Square_Data () + " : " + Square_Line_3 [4].Get_Square_Data ());
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Line 4 - " + Square_Line_4 [0].Get_Square_Data () + " : " + Square_Line_4 [1].Get_Square_Data () + " : " + Square_Line_4 [2].Get_Square_Data () + " : " +
			Square_Line_4 [3].Get_Square_Data () + " : " + Square_Line_4 [4].Get_Square_Data ());
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Line 5 - " + Square_Line_5 [0].Get_Square_Data () + " : " + Square_Line_5 [1].Get_Square_Data () + " : " + Square_Line_5 [2].Get_Square_Data () + " : " +
			Square_Line_5 [3].Get_Square_Data () + " : " + Square_Line_5 [4].Get_Square_Data ());
	}


	//결과 콘솔 출력
	private void Print_Check_Line()
	{
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Vertical Line - " + checked_Line [0] + " : " + checked_Line [1] + " : " + checked_Line [2] + " : " + checked_Line [3] + " : " + checked_Line [4]);
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Horizental Line - " + checked_Line [5] + " : " + checked_Line [6] + " : " + checked_Line [7] + " : " + checked_Line [8] + " : " + checked_Line [9]);
		Debug.Log ("Circle : " + run_Count + " " + "-> " + "Diagonal Line - " + checked_Line [10] + " : " + checked_Line [11]);
	}

	//상자 보석 숫자 가져오기
	private void Square_Get_Data()
	{
		for (int i = 0; i < 5; i++) 
		{
			Square_Line_1 [i].Get_Data();
			Square_Line_2 [i].Get_Data();
			Square_Line_3 [i].Get_Data();
			Square_Line_4 [i].Get_Data();
			Square_Line_5 [i].Get_Data();
		}
	}

	private int Calculate_Return_Multiple()
	{
		int multiple_Count = 0;

		for (int i = 0; i < checked_Line.Length; i++)
			if (checked_Line [i] == true)
				multiple_Count++;
		
		return multiple_Count;
	}

}
