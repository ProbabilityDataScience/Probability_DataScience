using UnityEngine;
using System.Collections;

public class Board {

	//보드안 보석이 표시되는 상자
	Square[,] m_Squares = new Square[5,5];
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

	//모든 라인 데이터 비교
	public void Check_All_Data()
	{
		Checked_Line_Count = 0;
		Square[] Squares = new Square[5];

		//Vertical Line Check
		for (int i = 0; i < 5; i++) 
		{
			for (int j = 0; j < 5; j++)
				Squares [j] = m_Squares [i, j];
			Check_Data(Squares);
		}

		//Horizental Line Check
		for (int i = 0; i < 5; i++) 
		{
			for (int j = 0; j < 5; j++)
				Squares [j] = m_Squares [j, i];
			Check_Data (Squares);
		}

		//Diagonal Line 1 Check
		for (int i = 0; i < 5; i++)
			Squares [i] = m_Squares [i, i];
		Check_Data (Squares);

		//Diagonal Line 2 Check
		int circle_Count = 4;
		for(int i = 0 ; i < 5 ;i++)
		{
			Squares [i] = m_Squares [i, circle_Count];
			circle_Count--;
		}
		Check_Data (Squares);

		Print_Check_Line ();
	}

	//각 상자 보석 번호 출력
	public void Print_Square_Number()
	{
		for(int i =  0 ; i < 5 ; i++)
		{
			Debug.Log ("Line " + i + " : " 
				+ m_Squares[0,i].Get_Square_Data() + " : " 
				+ m_Squares[1,i].Get_Square_Data() + " : "
				+ m_Squares[2,i].Get_Square_Data() + " : " 
				+ m_Squares[3,i].Get_Square_Data() + " : " 
				+ m_Squares[4,i].Get_Square_Data());
		}
	}

	//결과 콘솔 출력
	public void Print_Check_Line()
	{
		for (int i = 0; i < Checked_Line.Length; i++)
			Debug.Log (i+ " : " +Checked_Line [i]);
	}

	//상자 초기
	public void Square_Init()
	{
		for(int i = 0 ; i < 5 ; i++)
			for(int j = 0 ; j < 5 ; j++)
				m_Squares [i, j] = new Square ();
	}

	//상자 보석 숫자 가져오기
	public void Square_Get_Data()
	{
		for (int i = 0; i < 5; i++) 
			for (int j = 0; j < 5; j++) 
				m_Squares [i, j].Get_Data ();
	}

}
