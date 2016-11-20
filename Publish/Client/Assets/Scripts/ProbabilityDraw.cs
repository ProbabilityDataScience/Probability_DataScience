using UnityEngine;
using System.Collections;

public class ProbabilityDraw : MonoBehaviour {

	int[,] dataDisplay = new int[5,5];
	int[,] freeCount = new int[5,5];
	bool[] return_Money = new bool[12];

	int circle;
	int money;

	public int bet_Money;

	int RepeatCount = 1000;

	int intervalOfCube;

	public GameObject cube;

	int return_Money_Count = 0;
	int return_Money_Multi = 0;

	void Start()
	{
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 5; j++)
				freeCount [i, j] = 0;
		
		for (int i = 0; i < 12; i++)
			return_Money[i] = false;
		
		intervalOfCube = 0;
		circle = 0;
		money = 10000;

		StartCoroutine(NewRandomSet());
	}

	IEnumerator NewRandomSet()
	{
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 5; j++)
				dataDisplay[i, j] = 0;

		for (int i = 0; i < 12; i++)
			return_Money[i] = false;

		for (int i = 0; i < 5; i++) 
			for (int j = 0; j < 5; j++) 
				dataDisplay[i,j] = DataSet.DataSet_Get (3, ref freeCount[i,j]);
		
		yield return null;
		StartCoroutine(Roulette());
	}

	bool Check_Line(int data1, int data2, int data3, int data4 ,int data5)
	{
		bool return_value = false;

		if (data1 == data2 || data1 == 6 || data2 == 6) 
		{
			if (data2 == data3 || data2 == 6 || data3 == 6) 
			{
				if (data3 == data4 || data3 == 6 || data4 == 6) 
				{
					if (data4 == data5 || data4 == 6 || data5 == 6)
						return_value = true;
				}
			}
		}
		
		return return_value;	
	}

	void Debug_OutPut()
	{
		for(int i = 0 ; i < 5 ; i++)
			Debug.Log ("Circle " + circle + " -> " + "Line " + i + " : " +  dataDisplay [i, 0] + " : " + dataDisplay [i, 1] + " : " + dataDisplay [i, 2] + " : " + dataDisplay [i, 3] + " : " + dataDisplay [i, 4]);

		Debug.Log ("Circle " + circle + " -> " + " Completed Line : " + return_Money_Count);
	}

	IEnumerator Roulette()
	{
		circle += 1;
		money -= bet_Money;
		RepeatCount -= 1;

		return_Money_Count = 0;
		return_Money_Multi = 0;

		int index = 0;

		for (int i = 0 ; i < 5; i++) 
		{
			if (Check_Line (dataDisplay [i, 0], dataDisplay [i, 1], dataDisplay [i, 2], dataDisplay [i, 3], dataDisplay [i, 4])) 
				return_Money [index] = true;
			index++;
			if (Check_Line (dataDisplay [0, i], dataDisplay [1, i], dataDisplay [2, i], dataDisplay [3, i], dataDisplay [4, i]))
				return_Money [index] = true;
			index++;
		}
			
		if (Check_Line(dataDisplay[0,0], dataDisplay[1,1], dataDisplay[2,2], dataDisplay[3,3], dataDisplay[4,4]))
			return_Money [index] = true;
		index++;

		if (Check_Line(dataDisplay[0,4], dataDisplay[1,3], dataDisplay[2,2], dataDisplay[3,1] , dataDisplay[4,0]))
			return_Money [index] = true;
		index++;

		for (int i = 0; i < return_Money.Length; i++)
			if (return_Money [i] == true)
				return_Money_Count++;

		if (bet_Money == 10)
			return_Money_Multi = 7;
		else if (bet_Money == 20)
			return_Money_Multi = 16;
		else if (bet_Money == 50)
			return_Money_Multi = 40;
		else if (bet_Money == 100)
			return_Money_Multi = 80;
		else if (bet_Money == 200)
			return_Money_Multi = 160;
		else if (bet_Money == 500)
			return_Money_Multi = 400;
		else if (bet_Money == 1000)
			return_Money_Multi = 800;

		money += return_Money_Count * return_Money_Multi;

		if (RepeatCount % 1 == 0) 
		{
			Instantiate (cube, new Vector3 (2f * intervalOfCube, (float)money, 0), Quaternion.identity);
			intervalOfCube += 1;
		}

		Debug_OutPut ();

		if (RepeatCount > 0)
		{ 
			yield return null;
			StartCoroutine(NewRandomSet());
		}
		else
			yield break;
	}
}
