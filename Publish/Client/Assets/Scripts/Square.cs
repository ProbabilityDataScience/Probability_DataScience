using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	//이 상자의 보석 번호
	private int square_Data;

	//데이터셋에서 번호 가져오기
	public void Get_Data()
	{
		square_Data = DataSet.DataSet_Get (1);
	}

	//보석 번호 반환 해주기
	public int Get_Square_Data()
	{
		return square_Data;
	}

}
