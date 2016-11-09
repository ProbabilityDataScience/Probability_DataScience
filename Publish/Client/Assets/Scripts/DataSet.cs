using UnityEngine;
using System.Collections;

public static class DataSet 
{
	//Basic DataSet
	public static int[] dataSet = { 1,1,1,1,1,1,1,1,1,1, 2,2,2,2,2,2,2,2,2,2, 3,3,3,3,3,3,3,3,3,3, 4,4,4,4,4,4,4,4,4,4, 5,5,5,5,5,5,5,5,5,5, 6,6,6,6,6,6,6,6,6,6, 7 };

	public static int[] gem_DataSet_1 = {160,20,20,20,20,20,1};

	public static int free_Chance_Count;

	//인풋된 데이터셋에서 데이터가져오기
	public static int DataSet_Search(int[] input_DataSet)
	{
		//랜덤변수
		int RandomCount = 0;

		//보석숫자
		int GemNumber = 0;

		//데이터셋 총 보석 개수
		int MaxCount = 0;

		//인풋된 데이터셋 모든 보석 개수 가져오기
		for(int i = 0;  i < input_DataSet.Length ; i++)
			MaxCount += input_DataSet [i];

		//0부터 총 보석 개수의 길이중 랜덤으로 숫자 가져오기
		RandomCount = Random.Range (0, MaxCount);

		//데이터셋 서치 시작, 끝
		int StartNumber = 0;
		int EndNumber = 0;

		for (int i = 0; i < input_DataSet.Length; i++) 
		{
			//우선 서치 끝 길이에 반복문순서에 맞춰 보석 개수를 넣어줌
			EndNumber += input_DataSet [i];

			//데이터길이 범위 비교해서 특정순서에 포함되어 있으면 그에 해당하는 보석순서 선택 후 탈출 
			//아닐경우 반복문 진행
			if (Check_Data_Range (RandomCount, StartNumber, EndNumber)) 
			{
				GemNumber = i;
				break;
			}

			//다음 구문 실행 전, 서치 시작 길이에 데이터크기 더해줌
			StartNumber += input_DataSet [i];
		}

		//보석 순서 반환
		return GemNumber;
	}


	//범위에 따라 데이터 서치
	public static bool Check_Data_Range(int RandomCount, int Start, int End)
	{
		//랜덤카운트가 범위에 있을경우 참 반환
		if (RandomCount >= Start && RandomCount <= End)
			return true;
		else
			return false;
	}

	public static int DataSet_Get(int a, ref int b)
	{
		int m_GemNumber = 0;
		if (b <= 0) {
			if (a == 1) 
			{
				m_GemNumber = DataSet_Search (gem_DataSet_1);
				if (m_GemNumber == 7) 
				{
					free_Chance_Count = 5;
					b = 5;
				}
			}
		} else
			m_GemNumber = 7;
		
		return m_GemNumber;
	}
}