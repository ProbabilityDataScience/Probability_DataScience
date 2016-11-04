using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	//보드안 보석이 표시되는 상자
	public Square[] Square_Line_1 = new Square[5];
	public Square[] Square_Line_2 = new Square[5];
	public Square[] Square_Line_3 = new Square[5];
	public Square[] Square_Line_4 = new Square[5];
	public Square[] Square_Line_5 = new Square[5];
	//라인 비교 확인 카운트 변수
	int Checked_Line_Count;
	//확인된 라인
	bool[] Checked_Line = new bool[12];

	//라인 비교
	void Check_Data(Square[] Squares)
	{
		bool all_Line_Same = true;

		for (int i = 1; i < 5; i++) 
		{
			if (Squares[i].Get_Square_Data() != 7) 
			{
				if (Squares [i - 1].Get_Square_Data () != Squares [i].Get_Square_Data ()) 
				{
					all_Line_Same = false;
					break;
				}
			}
		}

		if (all_Line_Same)
			Checked_Line [Checked_Line_Count] = true;
		Checked_Line_Count++;
	}

	public void Vertical_Line_Check()
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

	public void Diagonal_Line_Check(Square A, Square B, Square C, Square D, Square E)
	{
		Square[] Squares = new Square[5];
		Squares [0] = A;
		Squares [1] = B;
		Squares [2] = C;
		Squares [3] = D;
		Squares [4] = E;
		Check_Data (Squares);
			
	}

	public void Horizental_Line_Check()
	{
		Square[] Squares;

		int horizental_Line = 0;

		for (int i = 0; i < 5; i++) 
		{
			Squares = Return_Horizental_Line (horizental_Line);
			Debug.Log ("Line " + i + " -> " + Squares [0].Get_Square_Data() + " : " + Squares [1].Get_Square_Data() + " : " + Squares [2]
				.Get_Square_Data()+ " : " + Squares [3].Get_Square_Data() + " : " + Squares [4].Get_Square_Data());
			Check_Data (Squares);
			horizental_Line++;
		}
	}

	//모든 라인 데이터 비교
	public void Check_All_Data()
	{
		Checked_Line_Count = 0;
		Square[] Squares = new Square[5];

		Vertical_Line_Check ();

		//Horizental Line Check
		Horizental_Line_Check();

		//Diagonal Line 1 Check
		Diagonal_Line_Check(Square_Line_1[0],Square_Line_2[1],Square_Line_3[2],Square_Line_4[3],Square_Line_5[4]);

		//Diagonal Line 2 Check
		Diagonal_Line_Check(Square_Line_1[4],Square_Line_2[3],Square_Line_3[2],Square_Line_4[1],Square_Line_5[0]);

		Print_Check_Line ();
	}

	/*
	//각 상자 보석 번호 출력
	public void Print_Square_Number()
	{
		for(int i =  0 ; i < 5 ; i++)
		{
			Debug.Log ("Line " + i + " : " 
				+ Square_Line_1[i].Get_Square_Data() + " : " 
				+ Square_Line_2[i].Get_Square_Data() + " : "
				+ Square_Line_3[i].Get_Square_Data() + " : " 
				+ Square_Line_4[i].Get_Square_Data() + " : " 
				+ Square_Line_5[i].Get_Square_Data());
		}
	}
	*/

	//결과 콘솔 출력
	public void Print_Check_Line()
	{
		for (int i = 0; i < Checked_Line.Length; i++)
			Debug.Log (i+ " : " +Checked_Line [i]);
	}

	//상자 보석 숫자 가져오기
	public void Square_Get_Data()
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

}
