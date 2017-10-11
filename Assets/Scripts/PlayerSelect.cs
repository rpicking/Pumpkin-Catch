using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PumpkinJam;

public class PlayerSelect : MonoBehaviour {
	// Number of players in the game
	public static int numPlayers;

	// Textures for the different controller types
	public Texture2D keyboardTexture;
	public Texture2D controllerTexture;

	// Called as soon as this script is loaded
	void Start() {
		// Initialize our textures
		keyboardTexture = Resources.Load<Texture2D>("player_keyboard");
		controllerTexture = Resources.Load<Texture2D>("player_controller");

		// Initialize the players array
		PlayerData.init();
	}

	// Draw the GUI every frame
	void OnGUI() {
		// Screen Measurements
		int rectHeight = Screen.height / 2;
		int rectWidth = Screen.width / 8;

		// Make two boxes for player select
		GUI.Box(new Rect((Screen.width / 4) - (rectWidth / 2),
		                 (Screen.height / 2) - (rectHeight / 2),
		                 rectWidth,
		                 rectHeight),
		        "Hunter");
		GUI.Box(new Rect((3 * (Screen.width / 4)) - (rectWidth / 2),
		                 (Screen.height / 2) - (rectHeight / 2),
		                 rectWidth,
		                 rectHeight),
		        "Prey");
		        
		// Draw the icons for each player
		for (int i = 0; i < numPlayers; i++) {
			Texture2D texture;

			// Set the texture based on the player input mode
			if (PlayerData.players[i].input == ControlMethod.KEYBOARD) {
				texture = keyboardTexture;
			} else {
				texture = controllerTexture;
			}

			if (Input.GetJoystickNames().Length > i) {
				PlayerData.players[i].input = ControlMethod.CONTROLLER;
			}

			// Update the position of the player icon
			if (PlayerData.players[i].left() && !PlayerData.players[i].iconJustMoved) {
				PlayerData.players[i].iconJustMoved = true;

				if (PlayerData.players[i].menuPosition == 1 && !PlayerData.players.Exists(x => x.playerRole == Role.HUNTER)) {
					PlayerData.players[i].menuPosition--;
					PlayerData.players[i].playerRole = Role.HUNTER;
				} else if (PlayerData.players[i].menuPosition == 2) {
					PlayerData.players[i].menuPosition--;
					PlayerData.players[i].playerRole = Role.UNDEFINED;
				}
			} else if (PlayerData.players[i].right() && !PlayerData.players[i].iconJustMoved) {
				PlayerData.players[i].iconJustMoved = true;

				if (PlayerData.players[i].menuPosition == 1) {
					PlayerData.players[i].menuPosition++;
					PlayerData.players[i].playerRole = Role.PREY;
				} else if (PlayerData.players[i].menuPosition == 0) {
					PlayerData.players[i].menuPosition++;
					PlayerData.players[i].playerRole = Role.UNDEFINED;
				}
			} else {
				PlayerData.players[i].iconJustMoved = false;
			}

			int left_pos;
			int top_pos = (Screen.height / 2) - (rectHeight / 2) + (((rectHeight - 64) / 2) + (100 * i));

			if (PlayerData.players[i].menuPosition == 1)
				left_pos = (Screen.width / 2) - (rectWidth / 2) + ((rectWidth - 64) / 2);
			else if (PlayerData.players[i].menuPosition == 0)
				left_pos = (Screen.width / 4) - (rectWidth / 2) + ((rectWidth - 64) / 2);
			else
				left_pos = (3 * (Screen.width / 4)) - (rectWidth / 2) + ((rectWidth - 64) / 2);

			// Render the texture in a box
			GUI.DrawTexture(new Rect(left_pos, top_pos, 64, 64), texture);
			GUI.Label(new Rect(left_pos + 64, top_pos + 64, 64, 64), (i + 1).ToString());
		}

		// Load the game when we press enter
		if (PlayerData.players.Exists(x => x.playerRole == Role.HUNTER) &&
		    PlayerData.players.Exists(y => y.playerRole == Role.PREY)) {

			GUI.Label(new Rect((Screen.width / 2) - 75, Screen.height / 3, 150, 50), "Press Ability to begin!");
			if (Input.GetKey(KeyCode.Return) || Input.GetButton("p1Ability1") || Input.GetButton("p2Ability1")) {
				Application.LoadLevel("Game");
			}
		}
	}
}
