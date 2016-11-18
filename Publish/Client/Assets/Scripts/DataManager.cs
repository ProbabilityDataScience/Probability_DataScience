using UnityEngine;
using System.Collections;

public class DataManager {

	public static string accountType;
	public static string fbUserID;
	public static int currentMoney=0;

	// 계정 정보를 불러옴
	public static void LoacAccountData()
	{
		// 계정 타입을 불러옴
		accountType = PlayerPrefs.GetString("accountType", "NoData");

		switch(accountType) {
			case "Facebook":
				break;

			case "NoData":
				break;

			default:
				break;
		}
	}

	// 계정 정보를 저장함
	public static void SaveAccountData()
	{
		PlayerPrefs.SetString("accountType", accountType);
	}
}
