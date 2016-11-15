using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class InitManager : MonoBehaviour {

	void Awake()
	{
		// 페북 SDK 초기화
		FB.Init(InitFB_Complete);

	}

	// 페북 초기화 성공
	private void InitFB_Complete()
	{
		FB.ActivateApp();
		// 작성중...
	}
}
