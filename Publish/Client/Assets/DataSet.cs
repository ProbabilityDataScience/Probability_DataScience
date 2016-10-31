using UnityEngine;
using System.Collections;

public static class DataSet 
{
	//Basic DataSet
	public static int[] dataSet = { 1,1,1,1,1,1,1,1,1,1, 2,2,2,2,2,2,2,2,2,2, 3,3,3,3,3,3,3,3,3,3, 4,4,4,4,4,4,4,4,4,4, 5,5,5,5,5,5,5,5,5,5, 6,6,6,6,6,6,6,6,6,6, 7 };

	public static int[] gem_DataSet_1 = {20,20,20,20,20,20,1};

	public static int DataSet_Search(int[] input_DataSet)
	{
		int RandomCount = 0;
		int GemNumber = 0;
		int MaxCount = 0;

		for(int i = 0;  i < input_DataSet.Length ; i++)
			MaxCount += input_DataSet [i];

		RandomCount = Random.Range (0, MaxCount);

		int StartNumber = 0;
		int EndNumber = 0;

		for (int i = 0; i < input_DataSet.Length; i++) 
		{
			EndNumber += input_DataSet [i];
			if (Check_Data_Range (RandomCount, StartNumber, EndNumber)) 
			{
				GemNumber = i + 1;
				break;
			}
			StartNumber += input_DataSet [i];
		}

		return GemNumber;

	}

	public static bool Check_Data_Range(int RandomCount, int Start, int End)
	{
		if (RandomCount > Start && RandomCount < End)
			return true;
		else
			return false;
	}

	public static int DataSet_Get()
	{
		return 1;
	}
}
