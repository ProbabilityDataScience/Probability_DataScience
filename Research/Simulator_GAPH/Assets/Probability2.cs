﻿using UnityEngine;
using System.Collections;

public class Probability2 : MonoBehaviour
{

	int[,] dataDisplay = new int[3, 3];
	int[,] dataDisplay2 = new int[3, 3];
	int[,] dataDisplay3 = new int[3, 3];

	/* dataset-1 ( very hight return )
	 *   1              2           3          4             5      
     *   1              5          20         40            200
	 *  0.2 ,          0.1,       0.25,      0.25,          0.2       Dataset Probability 
	 * 0.008 ,        0.001 ,   0.015625 ,  0.015625 ,     0.008      Dataset Probability 3X
	 * 24,000        15,000      937,500    1,875,000 ,   4,800,000   Dataset money output
     */
	//int[] dataSet = { 1,1,1,1,1,1,1,1  ,2,2,2,2  ,3,3,3,3,3,3,3,3,3,3  ,4,4,4,4,4,4,4,4,4,4  ,5,5,5,5,5,5,5,5 };
	int[] dataSet = { 3,4,1,5,3,4,2,4,5,3,1,1,3,5,4,2,5,4,3,1,1,3,5,4,2,4,5,3,1,1,3,5,4,2,4,5,3,1,4,3 };

	/* dataset-2 ( hight return )
    *   1              2           3          4             5      
    *   1              5          20         40            200
    *  0.3 ,          0.2,       0.2,      0.15,          0.15         Dataset Probability 
    * 0.027 ,        0.008 ,    0.008 ,   0.003375 ,     0.003375      Dataset Probability 3X
    * 81,000        120,000,    960,000,  1,920,000 ,   2,025,000      Dataset money output
    */
	//int[] dataSet2 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5 };
	int[] dataSet2 = { 3,1,2,1,4,3,5,4,3,5,1,2,1, 2  ,3, 1, 2, 1, 4, 5, 4, 3, 5, 1, 2, 1, 2   ,3, 1, 2, 1, 4, 3, 5, 4, 3, 5, 1, 2, 1  };

	/* dataSet-3 ( middle return )
     *   1              2           3          4             5      
     *   1              5          20         40            200
     * 0.25 ,         0.225,       0.2,       0.2,         0.125    Dataset Probability 
     * 0.015625 , 0.011390625 ,   0.008 ,    0.008 ,   0.001953125  Dataset Probability 3X
     * 15,626        56,953      160,000    320,000 ,     1,171,875   Dataset money output
    */
	//int[] dataSet3 = { 1,1,1,1,1,1,1,1,1,1  ,2,2,2,2,2,2,2,2,2  ,3,3,3,3,3,3,3,3  ,4,4,4,4,4,4,4,4  ,5,5,5,5,5}; //dataset original
	int[] dataSet3 = { 1, 3, 4, 2, 1, 3, 5, 4, 2, 1, 3, 4, 1, 5, 2, 3, 4, 1, 2, 3, 5, 2, 4, 1, 1, 3, 4, 1, 2, 3, 5, 4, 2, 1, 2, 3, 4, 5, 1, 2 }; //dataset change 1
	//int[] dataSet3 = { 1,3,2,1,4,5,5,4,3,2,1,1,3,4,2,3,1,4,5,2,3,2,4,1,1,3,1,2,4,3,2,2,1,4,5,5,4,3,1,2}; //dataset change 2

	/* dataSet-4 ( low return )
    *   1              2           3          4             5      
    *   1              5          20         40            200
    *  0.3 ,          0.25,      0.25,       0.1,          0.1     Dataset Probability 
    * 0.027 ,      0.015625 ,  0.015625 ,   0.001 ,       0.001    Dataset Probability 3X
    * 81,000        234,375     937,500    120,000 ,     600,000   Dataset money output
    */
	int[] dataSet4 = { 2,1,2,1,3,4,3,5,3,1,2,1  ,1,2,1,3,4,3,5,3,1,2,1  ,1,2,1,3,4,3,5,3,1,2,1  ,1,2,1,3,4,3,5,3,1,2,1,3,2 }; //dataset change 1

	/* dataSet-5 ( very low return )
    *   1              2           3          4             5      
    *   1              5          20         40            200
    *  0.4 ,          0.25,      0.20,       0.1,          0.05     Dataset Probability 
    * 0.027 ,      0.015625 ,    0.008 ,    0.001 ,       0.000125    Dataset Probability 3X
    * 192,000        234,375    480,000    120,000 ,      75,000   Dataset money output
    */
	int[] dataSet5 = { 1,2,1,3,4,3,2,1,2,1 ,5, 1, 2, 1, 2, 3, 4, 3, 2, 1, 2, 1  , 1, 2, 1, 2, 3, 4, 3, 2, 1, 2, 1 ,5, 1, 2, 1, 2, 3, 4, 3, 1, 2, 1 }; //dataset change 1


	int circle;
	int money;

	int RepeatCount = 999;

	int intervalOfCube;

	public GameObject cube;

	void Start()
	{
		intervalOfCube = 0;
		circle = 0;
		money = 999;

		StartCoroutine(NewRandomSet());
	}

	IEnumerator NewRandomSet()
	{
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				dataDisplay[i, j] = 0;

		/*
         * DataSet1 DataSet2 DataSet3
         * DataSet2 DataSet4 DataSet3
         * DataSet2 DataSet3 DataSet5
         */

		int randomOfDataSet;

		//[0,0]
		randomOfDataSet = Random.Range(0, dataSet.Length);
		dataDisplay[0, 0] = dataSet[randomOfDataSet];

		//[0,1]
		randomOfDataSet = Random.Range(0, dataSet2.Length);
		dataDisplay[0, 1] = dataSet2[randomOfDataSet];

		//[0,2]
		randomOfDataSet = Random.Range(0, dataSet3.Length);
		dataDisplay[0, 2] = dataSet3[randomOfDataSet];

		//[1,0]
		randomOfDataSet = Random.Range(0, dataSet2.Length);
		dataDisplay[1, 0] = dataSet2[randomOfDataSet];

		//[1,1]
		randomOfDataSet = Random.Range(0, dataSet4.Length);
		dataDisplay[1, 1] = dataSet4[randomOfDataSet];

		//[1,2]
		randomOfDataSet = Random.Range(0, dataSet3.Length);
		dataDisplay[1, 2] = dataSet3[randomOfDataSet];

		//[2,0]
		randomOfDataSet = Random.Range(0, dataSet2.Length);
		dataDisplay[2, 0] = dataSet2[randomOfDataSet];

		//[2,1]
		randomOfDataSet = Random.Range(0, dataSet3.Length);
		dataDisplay[2, 1] = dataSet3[randomOfDataSet];

		//[2,2]
		randomOfDataSet = Random.Range(0, dataSet5.Length);
		dataDisplay[2, 2] = dataSet5[randomOfDataSet];

		yield return null;
		StartCoroutine(Roulette());
	}

	IEnumerator Roulette()
	{
		circle += 1;
		money -= 3;
		RepeatCount -= 1;

		int[] earnMoney = { 0, 0, 0 };

		Debug.Log("Circle : " + circle);
		for (int i = 0; i < 3; i++)
			Debug.Log("(" + circle + ")" + ". " + "Data[" + i + ",0] : " + dataDisplay[i, 0] + " Data[" + i + ",1] : " + dataDisplay[i, 1] + " Data[" + i + ",2] : " + dataDisplay[i, 2]);

		for (int i = 0; i < 3; i++)
		{
			if (dataDisplay[i, 0] == dataDisplay[i, 1] && dataDisplay[i, 0] == dataDisplay[i, 2] && dataDisplay[i, 1] == dataDisplay[i, 2])
			{
				if (dataDisplay[i, 0] == 1)
				{
					money += 1;
					earnMoney[i] = 1;
				}
				else if (dataDisplay[i, 0] == 2)
				{
					money += 5;
					earnMoney[i] = 5;
				}
				else if (dataDisplay[i, 0] == 3)
				{
					money += 20;
					earnMoney[i] = 20;
				}
				else if (dataDisplay[i, 0] == 4)
				{
					money += 40;
					earnMoney[i] = 40;
				}
				else if (dataDisplay[i, 0] == 5)
				{
					money += 200;
					earnMoney[i] = 200;
				}
			}
		}

		Debug.Log("(" + circle + ")" + ". " + "Line 1 EarnMoney : " + earnMoney[0] + " Line 2 EarnMoney : " + earnMoney[1] + " Line 3 EarnMoney : " + earnMoney[2]);
		Debug.Log("(" + circle + ")" + ". " + money);

		if (RepeatCount % 1 == 0)
		{
			Instantiate(cube, new Vector3(2f * intervalOfCube, (float)money, 0), Quaternion.identity);
			intervalOfCube += 1;
		}

		if (RepeatCount > 0)
		{
			yield return null;
			StartCoroutine(NewRandomSet());
		}
		else
			yield break;
	}
}