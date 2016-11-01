using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	Board m_Board = new Board();

	void Start () {
		m_Board.Square_Init ();
		m_Board.Square_Get_Data ();
		m_Board.Print_Square_Number ();
		m_Board.Check_All_Data ();
	}
}
