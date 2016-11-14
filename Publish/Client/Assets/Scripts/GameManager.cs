using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m;

	public Board board;
	public int total_Money;
	public int bet_Money;

	public CustomNumber creditNum;
	public CustomNumber betNum;

	void Awake()
	{
		m = this;

		creditNum.changeText(total_Money);
		betNum.changeText(bet_Money);
	}

	public void Bet_Button_Function()
	{
		if(board.isRun == false) {
			if(total_Money > bet_Money) {
				if(DataSet.free_Chance_Count > 0)
					DataSet.free_Chance_Count--;
				else {
					total_Money -= bet_Money;
					creditNum.changeText(total_Money);
				}

				board.Run();
			}
		}
	}

	public void ChangeBetNum()
	{
		if(board.isRun == false) {
			switch(bet_Money) {
				case 10:
					bet_Money = 20;
					break;

				case 20:
					bet_Money = 30;
					break;

				case 30:
					bet_Money = 50;
					break;

				case 50:
					bet_Money = 100;
					break;

				case 100:
					bet_Money = 200;
					break;

				case 200:
					bet_Money = 500;
					break;

				case 500:
					bet_Money = 1000;
					break;

				case 1000:
					bet_Money = 10;
					break;
			}
			betNum.changeText(bet_Money);
		}
	}
}
