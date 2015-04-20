using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TicTacToe : MonoBehaviour {

	private string[] label = new string[9];
	public bool win, playerOne = true, playerTwo = false;

	public GameObject myButton;
	public Vector3 myPosition;
	private Vector2 myPivot;
	public Toggle playerOneToggle;
	public Toggle playerTwoToggle;

	public GameObject window;
	

	// Use this for initialization
	void Start () {
		//GameObject canvas = GameObject.Find ("Canvas");
		GameObject window = GameObject.Find ("Window");
		GameObject buttonPanel = GameObject.Find ("ButtonPanel");

		GameObject newButton = Instantiate (myButton) as GameObject;
		newButton.name = "newGameButton";
		newButton.GetComponent<Button> ().onClick.AddListener (() => {
			restartGame ();});
		RectTransform myTransform = newButton.GetComponent<RectTransform> ();
		Text myLabel = newButton.GetComponentInChildren<Text> ();
		myLabel.text = "New Game";
		myTransform.SetParent (window.transform, false);

		myPivot = new Vector2 (0.5f, 1);
		myPosition = new Vector3 (0, -40f, 0);
		myTransform.pivot = myPivot;
		myTransform.anchorMin = myPivot;
		myTransform.anchorMax = myPivot;
		myTransform.anchoredPosition = myPosition;

		for (int i = 0; i < 9; i++) {
			GameObject gameButton = Instantiate (myButton) as GameObject;
			gameButton.name = "gameButton " + i;
			RectTransform gameButtonTransform = gameButton.GetComponent<RectTransform>();
			gameButtonTransform.SetParent(buttonPanel.transform, false);
			label [i] = "";
			gameButton.GetComponentInChildren<Text>().text = label[i];
			gameButton.GetComponent<Button>().onClick.AddListener( () => {setButtonState (gameButton.name);});
		}
		win = false;
	}
	
	// Update is called once per frame
	void Update () {
		playerState ();
		if (win)
			window.SetActive (true);
	}

	public void CloseWindow() {
		restartGame ();
	}

	public void restartGame() {
		for (int i = 0; i < 9; i++) {
			GameObject gameButton = GameObject.Find ("gameButton " + i);
			label [i] = "";
			//Debug.Log(gameButton.name);
			gameButton.GetComponentInChildren<Text>().text = "";
			playerOneToggle.isOn = true;
			win = false;
		}
	}

	public void setButtonState(string name) {
		GameObject gameButton = GameObject.Find (name);
		string buttonNumberString = name.Substring(name.Length - Mathf.Min(1, name.Length));
		int buttonNumber = int.Parse (buttonNumberString);
		//Debug.Log(buttonNumber);
		string player;
		if (playerOne)
			player = "X";
		else
			player = "O";
		gameButton.GetComponentInChildren<Text> ().text = player;
		label [buttonNumber] = player;
		if (playerOneToggle.isOn) {
			playerTwoToggle.isOn = true;
		}
		else if (playerTwoToggle.isOn) {
			playerOneToggle.isOn = true;
		}
		checkWin ();
	}

	public void playerState() {
		if (playerOneToggle.isOn) {
			playerOne = true;
			playerTwo = false;
		}
		if (playerTwoToggle.isOn) {
			playerTwo = true;
			playerOne = false;
		}
	}



	void checkWin() {
		for (int i = 0; i < 9; i += 3)
			// Check Win Condition per Row
			if((label[i] == "X" && label[i + 1] == "X" && label[i + 1] == "X")||(label[i] == "O" && label[i + 1] == "O" && label[i + 1] == "O"))
				win = true;
		for (int i = 0; i < 3; i++)
			// Check Win Condition per Column
			if ((label[i] == "X" && label[i + 3] == "X" && label[i + 6] == "X") || (label[i] == "O" && label[i + 3] == "O" && label[i + 6] == "O"))
				win = true;
		if ((label[0] == "X" && label[4] == "X" && label[8] == "X") || (label[0] == "O" && label[4] == "O" && label[8] == "O"))
			win = true;
		if ((label[2] == "X" && label[4] == "X" && label[6] == "X") || (label[2] == "O" && label[4] == "O" && label[6] == "O"))
			win = true;
		if (!win) {
			bool tie = true;
			for (int i = 0; i < 9; i++)
				if(label[i] == "")
					tie = false;
			win = tie;
		}
	}
}
