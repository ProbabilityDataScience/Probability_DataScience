using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Player player;
	public Machine machine;

	public Graph graphPanel;
	public bool useRateGraph = false;
	public bool useMoneyGraph = true;

	private int startMoney;
	private int endMoney;
	private int currentCount;

	private bool isShowGraph = false;

	void Start()
	{
		machine.Init();
		graphPanel.GetComponent<CanvasGroup>().alpha = 0;

		StartSimulation();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(isShowGraph == true) {
				graphPanel.GetComponent<CanvasGroup>().alpha = 0;
				isShowGraph = false;
			} else {
				graphPanel.GetComponent<CanvasGroup>().alpha = 1;
				isShowGraph = true;
			}
		}
	}

	// 시뮬 시작
	public void StartSimulation()
	{
		player.money = 30000;

		// 그래프 초기화
		if(useRateGraph)
			graphPanel.Init(80.0f, 110.0f, 0, 10000, 20);
		if(useMoneyGraph)
			graphPanel.Init(20000, 40000, 0, 10000, 20);

		startMoney = player.money;
		currentCount = 0;
	}

	// 시뮬함
	public void Simulate(int count)
	{
		for(int i = 0; i < count; i++) {
			machine.Run(player);
			currentCount++;

			if(useRateGraph)
				graphPanel.DrawGraph(currentCount, (float)(player.money) / (float)startMoney * 100.0f);
			if(useMoneyGraph)
				graphPanel.DrawGraph(currentCount, player.money);
		}
		Debug.Log("Current Money: " + player.money);
		Debug.Log("Current Collect Rate: " + ((float)player.money / (float)startMoney * 100.0f));
	}
}
