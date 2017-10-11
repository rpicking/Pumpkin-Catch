using UnityEngine;
using System.Collections;

public class GUIBehavior : MonoBehaviour {

	public const int MIN_PLAYERS = 2;
	public const int MAX_PLAYERS = 2;

	int numPlayers = 2;
	int timer = 0;

	void OnGUI() {
		timer++;
		if (timer >= 60) {
			timer = 0;
			Debug.Log ("Horiz: " + Input.GetAxis("p1HorizAxis"));
			Debug.Log ("Vert: " + Input.GetAxis("p1VertAxis"));
		}

		int menuLeft = Screen.width / 4;
		int pSLeft = 3 * (Screen.width / 4);
		int menuTop = Screen.height / 3;
		int menuHeight = 100;
		int menuWidth = 90;
		int pSWidth = 60;

		// Menu Button selection box
		GUI.Box(new Rect(menuLeft, menuTop, menuHeight, menuWidth), "Main Menu");

		// Player Number selection box
		GUI.Box(new Rect(pSLeft, menuTop, menuHeight, pSWidth), "Players:");

		// Increase the number of players
		if(GUI.Button(new Rect(pSLeft + 70, menuTop + 30, 20, 20), "+" )) {
			if (numPlayers < MAX_PLAYERS)
				numPlayers++;
		}

		// Decrease the number of players
		if(GUI.Button(new Rect(pSLeft + 10, menuTop + 30, 20, 20), "-" )) {
			if (numPlayers > MIN_PLAYERS)
				numPlayers--;
		}

		// Draw the number of players
		GUI.Label(new Rect(pSLeft + 45, menuTop + 30, 20, 20), "" + numPlayers);

		// Load the Player Selection screen when Play Game is selected
		if(GUI.Button(new Rect(menuLeft + 10, menuTop + 30, 80, 20), "Play Game") ||
		   Input.GetKey(KeyCode.Return) ||
		   Input.GetButton("p1Ability1") ||
		   Input.GetButton("p2Ability1"))
		{
			PlayerSelect.numPlayers = numPlayers;
			Application.LoadLevel("HowToPlay");
		}
		
		// Quit button quits the game
		if(GUI.Button(new Rect(menuLeft + 10, menuTop + 60, 80, 20), "Quit") ||
		   Input.GetButton("p1Ability2") ||
		   Input.GetButton("p2Ability2"))
		{
			Application.Quit();
		}
	}
}
