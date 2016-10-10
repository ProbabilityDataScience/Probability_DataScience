using UnityEngine;
using System.Collections;

public class Probability : MonoBehaviour {

	int[,] dataDisplay = new int[3,3];
	int[,] dataDisplay2 = new int[3,3];
	int[,] dataDisplay3 = new int[3,3];
	/*
	 *   1              2           3          4             5      
	 * 0.25 ,         0.225,       0.2,       0.2,         0.125    Dataset Probability 
	 * 0.015625 , 0.011390625 ,   0.008 ,    0.008 ,   0.001953125  Dataset Probability 3X
	 * 15,626        56,953      160,000    320,000 ,     390,625   Dataset money output
	 * 46,878       170,859      480,000    960,000 ,    1,172,875  Dataset money output 3X
	 * 
	 * Total 2,829,612 money left
	*/ 
	//int[] dataSet = { 1,1,1,1,1,1,1,1,1,1  ,2,2,2,2,2,2,2,2,2  ,3,3,3,3,3,3,3,3  ,4,4,4,4,4,4,4,4  ,5,5,5,5,5}; //dataset original
	int[] dataSet = { 1,3,4,2,1,3,5,4,2,1,3,4,1,5,2,3,4,1,2,3,5,2,4,1,1,3,4,1,2,3,5,4,2,1,2,3,4,5,1,2}; //dataset change 1
	//int[] dataSet = { 1,3,2,1,4,5,5,4,3,2,1,1,3,4,2,3,1,4,5,2,3,2,4,1,1,3,1,2,4,3,2,2,1,4,5,5,4,3,1,2}; //dataset change 2

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

        for (int i = 0; i < 3; i++)
        {
            int Rnd = Random.Range(0, dataSet.Length);
            dataDisplay[1, i] = dataSet[Rnd];
            if (Rnd == 0)
            {
                dataDisplay[0, i] = dataSet[dataSet.Length - 1];
                dataDisplay[2, i] = dataSet[Rnd + 1];
            }
            else if (Rnd == dataSet.Length - 1)
            {
                dataDisplay[0, i] = dataSet[Rnd - 1];
                dataDisplay[2, i] = dataSet[0];
            }
            else
            {
                dataDisplay[0, i] = dataSet[Rnd - 1];
                dataDisplay[2, i] = dataSet[Rnd + 1];
            }
        }
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
			Instantiate (cube, new Vector3 (2f * intervalOfCube, (float)money, 0), Quaternion.identity);
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