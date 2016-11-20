using UnityEngine;
using System.Collections;

public static class DataSet 
{
	public const int gem_SelectSet_Count = 1;
	public const int gem_DataSet_Count = 3;

	//------------------------------------- Gem Select Sets -----------------------------------------
	/*-- () Select_Set 1 () --
	 *  0.17 0.17 0.17 0.17 0.16 0.16
	 */
	public static int[] gem_SelectSet_1 = { 17, 17, 17, 17, 16, 16 };

	/*-- () Select_Set 2 () --
	 *  0.25 0.15 0.15 0.15 0.15 0.15
	 */
	public static int[] gem_SelectSet_2 = { 25, 15, 15, 15, 15, 15 };

	/*-- () Select_Set 3 () --
	 *  0.3 0.3 0.2 0.1 0.05 0.05
	 */
	public static int[] gem_SelectSet_3 = { 30, 30, 20, 10, 5, 5 };
	//-----------------------------------------------------------------------------------------------
	public static int[] gem_DataSet_1 = {578000,71000,71000,71000,71000,71000,1000};
	public static int[] gem_DataSet_5 = {578000,71000,71000,71000,71000,71000,1000};
	/*-- () Data_Set 2 () --
	 * 
	 *    0.71		 0.0578		 	 0.0578	 		    0.0578			0.0578			0.0578			0.001		//Probability of each gems
	 * 
	 * Total : 41038000 count Data set
	 */
	public static int[] gem_DataSet_2 = {578000,71000,71000,71000,71000,71000,1000};

	/*-- () Data_Set 3 () --
	 * 
	 *	 0.6363..		 0.07239..		0.07239..		0.07239..		0.07239..		0.07239..		0.001
	 * 
	 * Total : 28054000 count Data set
	 */
	public static int[] gem_DataSet_3 = {378000,43000,43000,43000,43000,43000,1000};

	/*-- () Data_Set 4 () --
	 * 
	 *    0.58			0.0838		 	0.0838	 		0.0838			0.0838			0.0838			0.001		//Probability of each gems
	 * 
	 * Total : 48604000 count Data set
	 */
	public static int[] gem_DataSet_4 = {838000,58000,58000,58000,58000,58000,1000};

	//-------------------------------------------------------------------------------------------------

	public static int free_Chance_Count;
	public static int selected_Gem;

	//인풋된 데이터셋에서 데이터가져오기
	public static int DataSet_Search(int[] input_DataSet)
	{
		//랜덤변수
		int RandomCount = 0;

		//리턴숫자
		int Number = 0;

		//데이터셋 총 개수
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
			//우선 서치 끝 길이에 반복문순서에 맞춰 개수를 넣어줌
			EndNumber += input_DataSet [i];

			//데이터길이 범위 비교해서 특정순서에 포함되어 있으면 그에 해당하는 순서 선택 후 탈출 
			//아닐경우 반복문 진행
			if (Check_Data_Range (RandomCount, StartNumber, EndNumber)) 
			{
				Number = i;
				break;
			}

			//다음 구문 실행 전, 서치 시작 길이에 데이터크기 더해줌
			StartNumber += input_DataSet [i];
		}

		//순서 반환
		return Number;
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

	//데이터셋에서 데이터 가져오기
	public static int DataSet_Get(int a, ref int b)
	{
		int m_GemNumber = 0;
		if (b <= 0) {
			if (a == 1)
				m_GemNumber = DataSet_Search (gem_DataSet_1);
			else if (a == 2)
				m_GemNumber = DataSet_Search (gem_DataSet_2);
			else if (a == 3)
				m_GemNumber = DataSet_Search (gem_DataSet_3);
			else if (a == 4)
				m_GemNumber = DataSet_Search (gem_DataSet_4);
			else if (a == 5)
				m_GemNumber = DataSet_Search (gem_DataSet_5);

			if (m_GemNumber == 6) {
				free_Chance_Count = 5;
				b = 5;
			}
		} else 
		{
			b--;
			m_GemNumber = 6;
		}
		
		return m_GemNumber;
	}


	//보여줄 보석 순서 가져오기
	public static int SelectSet_Get(int a)
	{
		int m_SelectNumber = 0;
	
		if (a == 1)
			m_SelectNumber = DataSet_Search (gem_SelectSet_1);
		else if (a == 2)
			m_SelectNumber = DataSet_Search (gem_SelectSet_2);
		
		return m_SelectNumber;
	}
}