using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

    public bool gameStateWon;
    public int movesNeeded;
    private bool showWindow;
    private LevelData[] levels;
    private int currentLevel;

    #region UNITY Runtime
    // Functions for classes within the Unity Environment

    // Called before Start -> Classic Loading Screen Function.
    void Awake() {
        JSONParser.UpdateLevelData();
    }


    // Use this for initialization
    void Start() {
        showWindow = true;
        gameStateWon = false;

        // I'd prefer to work with Lists, but Unity's GUI doesn't work with them.
        if (JSONParser.LevelsAvailable != null) {
            levels = new LevelData[JSONParser.LevelsAvailable.Count];
            int i = 0;
            foreach (LevelData lvl in JSONParser.LevelsAvailable) {
                levels[i] = lvl;
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }

    // This is called once to thrice a frame.
    void OnGUI() {
        if (showWindow) {
            DisplayWindow(gameStateWon);
        }
        else {
            if (GUI.Button(new Rect(Screen.width - 110, 10, 100, 25), "Options")) {
                showWindow = true;
            }
        }

        if (gameStateWon) {
            Rect victoryWindow = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            victoryWindow = GUI.Window(1, victoryWindow, DrawVictoryWindow, "Y O U     W O N ! ! ! 1 ");
        }
    }

    #endregion

    #region GUI Functionality
    // I'd usually write this into a seperate class, but this would require some data consistency effort I'd rather avoid right now.

    private void DisplayWindow(bool win) {
        if (JSONParser.LevelsAvailable != null) {
            int buttonCount = JSONParser.LevelsAvailable.Count;
            Rect windowFrame = new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500);
            windowFrame = GUI.Window(0, windowFrame, DrawWindow, HeadlineText(win));
        }
        else {

        }
    }

    private string HeadlineText(bool win) {
        if (win)
            return "Congratulations, you won the video game!";
        else
            return "Welcome to the lightbulb game!";
    }

    private void DrawWindow(int windowID) {
        int marginTop = 100;
        int marginLeft = 10;
        int defaultMargin = 10;
        int seperator = 200;
        int buttonWidth = 100;
        int buttonHeight = 25;

        GUI.Label(new Rect(marginLeft, marginTop, 480, 25), "Please Choose a Level: ");
        for (int i = 0; i < levels.Length; i++) {
            if (i % 4 == 0) {
                marginTop += defaultMargin + buttonHeight;
                marginLeft = 10;
            }

            if (GUI.Button(new Rect(marginLeft, marginTop, buttonWidth, buttonWidth), levels[i].name)) {
                currentLevel = i + 1;
                StartLevel(levels[i]);
            }
            marginLeft += defaultMargin + buttonWidth;
        }
        marginLeft = 10;
        marginTop += defaultMargin + buttonHeight + seperator;

        if (GUI.Button(new Rect(marginLeft, marginTop, buttonWidth, buttonHeight), "Update Levels")) {
            UpdateLevels();
        }
        marginLeft += 2 * defaultMargin + buttonWidth;

        if (GUI.Button(new Rect(marginLeft, marginTop, buttonWidth, buttonHeight), "Restart Level")) {
            if (currentLevel != 0) {
                RestartCurrent(levels[currentLevel - 1]);
            }
        }
        marginLeft += 2 * defaultMargin + buttonWidth;

        if (GUI.Button(new Rect(marginLeft, marginTop, buttonWidth, buttonHeight), "Close Window")) {
            showWindow = false;
        }
        marginLeft += 2 * defaultMargin + buttonWidth;

        if (GUI.Button(new Rect(marginLeft, marginTop, buttonWidth, buttonHeight), "Exit Game")) {
            Application.Quit();
        }
    }

    private void DrawVictoryWindow(int winID) {
        GUI.Label(new Rect(10, 50, 380, 40), "You won the video game needing " + movesNeeded.ToString() + " moves. Well done!");
        if (GUI.Button(new Rect(10, 100, 100, 25), "Main Menu")) {
            gameStateWon = false;
            showWindow = true;
            movesNeeded = 0;
        }
    }

    #region Functions
    private void StartLevel(LevelData lvl) {
        showWindow = false;
        GameManager gm = Camera.main.GetComponent<GameManager>();
        gm.DeconstructLevel();
        gm.ConstructLevel(lvl);
        gm.MoveCount = 0;
    }

    private void UpdateLevels() {
        showWindow = false;
        GameManager gm = Camera.main.GetComponent<GameManager>();
        gm.DeconstructLevel();
        JSONParser.UpdateLevelData();
        showWindow = true;
    }

    private void RestartCurrent(LevelData lvl) {
        showWindow = false;
        GameManager gm = Camera.main.GetComponent<GameManager>();
        gm.DeconstructLevel();
        gm.ConstructLevel(lvl);
        gm.MoveCount = 0;
    }
    #endregion

    #endregion
}
