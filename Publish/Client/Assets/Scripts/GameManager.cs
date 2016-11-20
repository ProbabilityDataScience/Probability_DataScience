using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Facebook.Unity;

public class GameManager : MonoBehaviour {

	public static GameManager m;

	public AnimationManager animationManager;
	public Board board;
	public int total_Money;
	public int bet_Money;
	public int bet_Money_Multi;

	public CustomNumber creditNum;
	public CustomNumber betNum;

	public Button betBtn;
	public Button spinBtn;

	void Awake()
	{
		m = this;

		// 돈 정보 업데이트
		total_Money = DataManager.currentMoney;

		bet_Money = 10;
		bet_Money_Multi = 2;

		creditNum.ChangeNum(total_Money);
		betNum.ChangeNum(bet_Money);
	}

	void Start()
	{
		animationManager.Play();
	}

	public void Bet_Button_Function()
	{
		int beforeMoney = total_Money;
		if(board.isRun == false) {
			if(total_Money > bet_Money) {
				//if(DataSet.free_Chance_Count > 0)
				//	DataSet.free_Chance_Count--;
				//else {
					total_Money -= bet_Money;
					creditNum.ChangeNum(total_Money);
				//}

				board.Run();

				// 라인의 수 계산
				int lineCount = 0;
				foreach(bool c in board.checked_Line) {
					if(c == true)
						lineCount++;
				}

				// 데이터 전송
				spinBtn.GetComponent<NetworkSession>().datas.Clear();
				spinBtn.GetComponent<NetworkSession>().datas.Add(DataManager.fbUserID); // 유저 토큰값
				spinBtn.GetComponent<NetworkSession>().datas.Add(beforeMoney.ToString()); // 돌리기전 돈
				spinBtn.GetComponent<NetworkSession>().datas.Add(total_Money.ToString()); // 돌리기후 돈
				spinBtn.GetComponent<NetworkSession>().datas.Add(bet_Money.ToString()); // 배팅금액
				spinBtn.GetComponent<NetworkSession>().datas.Add(DateTime.Now.ToString()); // 클릭시간
				spinBtn.GetComponent<NetworkSession>().datas.Add(lineCount.ToString()); // 결과(라인의 수)
				spinBtn.GetComponent<NetworkSession>().Request();
			}
		}
	}



	public void ChangeBetNum()
	{
		// 배팅 금액 바꿔줌
		if(board.isRun == false) {
			switch(bet_Money) {
				case 10:
					bet_Money = 20;
					bet_Money_Multi = 4;
					break;

				case 20:
					bet_Money = 30;
					bet_Money_Multi = 6;
					break;

				case 30:
					bet_Money = 50;
					bet_Money_Multi = 10;
					break;

				case 50:
					bet_Money = 100;
					bet_Money_Multi = 20;
					break;

				case 100:
					bet_Money = 200;
					bet_Money_Multi = 40;
					break;

				case 200:
					bet_Money = 500;
					bet_Money_Multi = 100;
					break;

				case 500:
					bet_Money = 1000;
					bet_Money_Multi = 200;	
					break;

				case 1000:
					bet_Money = 10;
					bet_Money_Multi = 2;
					break;
			}
			betNum.ChangeNum(bet_Money);
		}
		// 데이터 전송
		betBtn.GetComponent<NetworkSession>().datas.Clear();
		betBtn.GetComponent<NetworkSession>().datas.Add(DataManager.fbUserID);
		betBtn.GetComponent<NetworkSession>().datas.Add(DateTime.Now.ToString());
		spinBtn.GetComponent<NetworkSession>().Request();
	}
}
