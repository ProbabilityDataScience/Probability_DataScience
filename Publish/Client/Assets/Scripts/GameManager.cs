using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Board m_Board;

	void Start () {
		m_Board.Square_Get_Data ();
		//m_Board.Print_Square_Number ();
		m_Board.Check_All_Data ();
	}
}
