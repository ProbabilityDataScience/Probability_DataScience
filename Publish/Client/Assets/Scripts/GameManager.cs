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
                NetworkSession networkSession = spinBtn.GetComponent<NetworkSession>();

                networkSession.protocol = RequestProtocol.SpinButton;
                networkSession.type = RequestType.PUT;

                networkSession.datas.Clear();
                networkSession.datas.Add(DataManager.fbUserID); // 유저 토큰값
                networkSession.datas.Add(beforeMoney.ToString()); // 돌리기전 돈
                networkSession.datas.Add(total_Money.ToString()); // 돌리기후 돈
                networkSession.datas.Add(bet_Money.ToString()); // 배팅금액
                networkSession.datas.Add(DateTime.Now.ToString()); // 클릭시간
                networkSession.datas.Add(lineCount.ToString()); // 결과(라인의 수)
                networkSession.Proc_SpinButton();
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
					bet_Money_Multi = 14;
					break;

				case 20:
					bet_Money = 30;
					bet_Money_Multi = 21;
					break;

				case 30:
					bet_Money = 50;
					bet_Money_Multi = 35;
					break;

				case 50:
					bet_Money = 100;
					bet_Money_Multi = 70;
					break;

				case 100:
					bet_Money = 200;
					bet_Money_Multi = 140;
					break;

				case 200:
					bet_Money = 500;
					bet_Money_Multi = 350;
					break;

				case 500:
					bet_Money = 1000;
					bet_Money_Multi = 700;	
					break;

				case 1000:
					bet_Money = 10;
					bet_Money_Multi = 7;
					break;
			}
			betNum.ChangeNum(bet_Money);
		}

        //// 데이터 전송
        NetworkSession networkSession = betBtn.GetComponent<NetworkSession>();

        networkSession.protocol = RequestProtocol.BattingButton;
        networkSession.type = RequestType.PUT;

        networkSession.datas.Clear();
       networkSession.datas.Add(DataManager.fbUserID);
       networkSession.datas.Add(DateTime.Now.ToString());
       networkSession.Proc_BettingButton();
    }
}
