using UnityEngine;
using System.Collections;
//using System;

public class Machine : MonoBehaviour {

	private int[] line = new int[40];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 초기화
	public void Init()
	{
		// 라인 초기화
		int count1 = 10;
		int count2 = 9;
		int count3 = 9;
		int count4 = 7;
		int count5 = 5;

		// 1의 개수
		for(int i = 0; i < count1; i++) {
			line[i] = 1;
		}
		// 2의 개수
		for(int i = count1; i < count1 + count2; i++) {
			line[i] = 2;
		}
		// 3의 개수
		for(int i = count1 + count2; i < count1 + count2 + count3; i++) {
			line[i] = 3;
		}
		// 4의 개수
		for(int i = count1 + count2 + count3; i < count1 + count2 + count3 + count4; i++) {
			line[i] = 4;
		}
		// 5의 개수
		for(int i = count1 + count2 + count3 + count4; i < count1 + count2 + count3 + count4 + count5; i++) {
			line[i] = 5;
		}
		
		// 숫자들을 섞음
		for(int i=0; i<100; i++) {
			int t1 = Random.Range(0, 40);
			int t2 = Random.Range(0, 40);

			if(t1 != t2) {
				int tmp = line[t1];
				line[t1] = line[t2];
				line[t2] = tmp;
			}
		}
	}

	// 실행
	public void Run(Player p)
	{
		p.money -= 3;

		// 각 라인들의 숫자를 선택
		int[] selectedLineIndices = new int[3];
		selectedLineIndices[0] = Random.Range(0, 40);
		selectedLineIndices[1] = Random.Range(0, 40);
		selectedLineIndices[2] = Random.Range(0, 40);

		// 당첨 여부 확인
		int max = 0;
		int[] indices = new int[3];

		for(int i=-1; i<=1; i++) {
			bool isWin = true;

			for(int j=0; j<3; j++) {
				indices[j] = selectedLineIndices[j] + i;
				if(indices[j] < 0)
					indices[j] += 40;
				if(indices[j] >= 40)
					indices[j] -= 40;
			}

			for(int j=1; j<3; j++) {
				if(line[indices[0]] != line[indices[j]]) {
					isWin = false;
					break;
				}
			}

			if(isWin == true && max < line[indices[0]]) {
				max = line[indices[0]];
			}
		}

		// 상금 줌
		switch(max) {
			case 1:
				p.money += 1;
				break;

			case 2:
				p.money += 5;
				break;

			case 3:
				p.money += 20;
				break;

			case 4:
				p.money += 40;
				break;

			case 5:
				p.money += 200;
				break;
		}
	}
}
