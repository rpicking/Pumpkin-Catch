using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PumpkinJam
{
	// The input method each player is using
	public enum ControlMethod {
		KEYBOARD,
		CONTROLLER
	}
	
	// Which role the player is playing
	public enum Role {
		UNDEFINED,
		HUNTER,
		PREY
	}
	
	// Class containing information about a player
	public class PlayerData {

		// List of all players in the game
		public static List<PlayerData> players;

		// Initialization function
		public static void init() {
			// Initialize the list of players
			players = new List<PlayerData>();

			players.Add(new PlayerData() {
				keyHorizAxis = "p1HorizKeys",
				keyVertAxis = "p1VertKeys",
				contHorizAxis = "p1HorizAxis",
				contVertAxis = "p1VertAxis",
				abilityButton1 = "p1Ability1",
				abilityButton2 = "p1Ability2"
			});
			players.Add(new PlayerData() {
				keyHorizAxis = "p2HorizKeys",
				keyVertAxis = "p2VertKeys",
				contHorizAxis = "p2HorizAxis",
				contVertAxis = "p2VertAxis",
				abilityButton1 = "p2Ability1",
				abilityButton2 = "p2Ability2"
			});
		}

		// Get the hunter from the list of players
		public static PlayerData getHunter() {
			return players.Find(x => x.playerRole == Role.HUNTER);
		}

		// Get a list of all the preys from the list of players
		public static PlayerData getPrey() {
			return players.Find(x => x.playerRole == Role.PREY);
		}

		// Return booleans based on the player inputs
		// Usage: if (PlayerData.getHunter().left())
		public bool left() {
			return (Input.GetAxis(keyHorizAxis) < 0 || Input.GetAxis(contHorizAxis) < 0);
		}

		public bool right() {
			return (Input.GetAxis(keyHorizAxis) > 0 || Input.GetAxis(contHorizAxis) > 0);
		}

		public bool up() {
			return (Input.GetAxis(keyVertAxis) > 0 || Input.GetAxis(contVertAxis) > 0);
		}

		public bool down() {
			return (Input.GetAxis(keyVertAxis) < 0 || Input.GetAxis(contVertAxis) < 0);
		}

		public bool ability1() {
			return (Input.GetButton(abilityButton1));
		}

		public bool ability2() {
			return (Input.GetButton(abilityButton2));
		}

		// Method of input
		public ControlMethod input = ControlMethod.KEYBOARD;
		
		// 1 = undefined, 0 = hunter, 2 = prey
		public int menuPosition = 1;
		
		// This player's role
		public Role playerRole = Role.UNDEFINED;
		
		// Did the player just move their icon?
		public bool iconJustMoved = false;

		// Configurations for different input methods
		private string keyHorizAxis;
		private string keyVertAxis;
		private string contHorizAxis;
		private string contVertAxis;
		private string abilityButton1;
		private string abilityButton2;
	}
}
