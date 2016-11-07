using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Board board;
	public int total_Money;
	public int bet_Money;

	void Start () {
		Bet_Button_Function ();
	}

	void Bet_Button_Function()
	{
		if (total_Money > bet_Money) 
		{
			if (DataSet.free_Chance_Count > 0)
				DataSet.free_Chance_Count--;
			else
				total_Money -= bet_Money;
			
			board.Square_Get_Data ();
			board.Print_Square_Number ();
			board.Check_All_Data ();
		}
	}
}
