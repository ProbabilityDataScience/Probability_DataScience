using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Player player;
	public Machine machine;

	public RateGraph rateGraphPanel;

	private int startMoney;
	private int endMoney;

	void Start()
	{
		machine.Init();
	}

	// 시뮬 시작
	public void StartSimulation()
	{
		player.money = 30000;
		// 그래프 초기화
		rateGraphPanel.Init(10000, 10, 80.0f, 110.0f);

		startMoney = player.money;
		for(int i=0; i<10000; i++) {
			machine.Run(player);
			rateGraphPanel.DrawGraph((float)player.money / (float)startMoney * 100.0f, i+1);
		}
		endMoney = player.money;
		Debug.Log("Current Money: " + endMoney);
		Debug.Log("Collect Rate: " + ((float)endMoney / (float)startMoney * 100.0f));
	}
}
