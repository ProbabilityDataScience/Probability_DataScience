using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Player player;
	public Machine machine;

	public Graph rateGraphPanel;

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
		rateGraphPanel.Init(80.0f, 110.0f, 0, 1000);

		startMoney = player.money;
		for(int i=0; i<1000; i++) {
			machine.Run(player);
			rateGraphPanel.DrawGraph(i+1, (float)(player.money) / (float)startMoney * 100.0f);
		}
		endMoney = player.money;
		Debug.Log("Current Money: " + endMoney);
		Debug.Log("Collect Rate: " + ((float)endMoney / (float)startMoney * 100.0f));
	}
}
