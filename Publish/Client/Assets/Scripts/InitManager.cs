﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour {

	public Image TitleImage;
	public Image UIPanel;

	private enum State { NotLogin, Loginning, Logined };
	private State state;
	private NetworkSession network;

	void Awake()
	{
		network = this.GetComponent<NetworkSession>();

		state = State.NotLogin;
		// DOTween 초기화
		DOTween.Init();

		// 페북 SDK 초기화
		FB.Init(InitFB_Complete);
	}

	// 페북 초기화 성공
	private void InitFB_Complete()
	{
		FB.ActivateApp();
		// 계정 정보를 불러옴
		DataManager.LoacAccountData();

		// 로그인 시도
		state = State.Loginning;
		switch(DataManager.accountType) {
			case "Facebook":
				// Facebook 로그인 시도
				FB.LogInWithReadPermissions(new List<string> { "public_profile" }, FBLogin_Callback);
				break;

			case "NoData":
				break;

			default:
				break;
		}
	}
	// 페북 로그인 콜백
	private void FBLogin_Callback(ILoginResult result)
	{
		if(FB.IsLoggedIn) { // 로그인됨

			Debug.Log("Successful login with facebook");
			Debug.Log("TokenID : " + AccessToken.CurrentAccessToken.UserId);

			// 서버로 로그인 보내줌
			network.datas.Add(AccessToken.CurrentAccessToken.UserId); // 토큰값
			if(DataManager.accountType == "NoData") // 처음 로그인 했는가?
				network.datas.Add("Yes");
			else
				network.datas.Add("No");
			network.type = RequestType.POST;
			network.protocol = RequestProtocol.FacebookLogin;
			network.Request();

			// 돈 정보를 받아옴
			//DataManager.currentMoney = int.Parse(network.ReadData().datas[0]);

			// 계정 정보를 Facebook으로 하고 저장
			DataManager.accountType = "Facebook";
			DataManager.SaveAccountData();

			Login_Complete();
		} else {
			state = State.NotLogin;
			Debug.Log("Fail to login with facebook");
		}
	}

	// 로그인 완료
	private void Login_Complete()
	{
		state = State.Logined;

		UIPanel.rectTransform.DOAnchorPosX(-750f, 0.7f);
	}

	// ----- Button Function -----

	// 페북으로 로그인
	public void LoginWithFacebook()
	{
		// Facebook 로그인 시도
		FB.LogInWithReadPermissions(new List<string> { "public_profile" }, FBLogin_Callback);
	}

	// 다음 Scene으로 넘어감
	public void MoveNextScene()
	{
		SceneManager.LoadScene("MainScene");
	}
	private IEnumerator MoveNextScene_ani()
	{
		yield return new WaitForSeconds(2f);
	}

	// Guest버튼 누름
	public void ClickGuestBtn()
	{
		DataManager.accountType = "Guest";
		Debug.Log("Successful login with guest");

		Login_Complete();
	}
}
