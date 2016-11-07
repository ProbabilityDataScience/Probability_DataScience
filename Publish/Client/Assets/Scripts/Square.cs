using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	//이 상자의 보석 번호
	private int square_Data;
	private int free_Count;

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

}
