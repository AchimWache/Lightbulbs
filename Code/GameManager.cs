using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

    public int MoveCount {
        get; set;
    }

    private Lightbulb[,] lightsArray;

    private List<GameObject> activeObjectsList;

	// Use this for initialization
	void Start () {
        MoveCount = 0;
        activeObjectsList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2 - 200, 10, 400, 50), "Moves Taken: " + MoveCount);
    }

    public void ConstructLevel(LevelData data) {
        int bulbsInField = 0;
        lightsArray = new Lightbulb[data.rows, data.columns];
        
        for (int Y = 0; Y < data.rows; Y++) {
            for (int X = 0; X < data.columns; X++) {
                GameObject lightbulb = Instantiate(Resources.Load("Lightbulb"),
                                                  new Vector3(X - data.columns / 2, (Y - data.rows / 2) * -1, 0),
                                                  Quaternion.identity) as GameObject;
                Lightbulb lightbulbScript = lightbulb.GetComponent<Lightbulb>() as Lightbulb;
                if (data.on.Contains(bulbsInField)) {
                    lightbulbScript.ToggleState = true;
                }
                lightbulbScript.X = X;
                lightbulbScript.Y = Y;
                lightsArray[X, Y] = lightbulbScript;
                activeObjectsList.Add(lightbulb);
                bulbsInField++;
            }
        }
    }

    public void DeconstructLevel() {
        foreach (GameObject go in activeObjectsList) {
            Destroy(go);
        }
    }

    public void CheckWinCondition() {
        bool checker = false;
        foreach (GameObject go in activeObjectsList) {
            Lightbulb lightbulbScript = go.GetComponent<Lightbulb>() as Lightbulb;
            if (lightbulbScript.ToggleState) {
                checker = true;
            }
        }

        if (!checker) {
            Initializer init = Camera.main.GetComponent<Initializer>() as Initializer;
            init.movesNeeded = this.MoveCount;
            init.gameStateWon = true;
        }
    }

    #region Interaction Events

    public void VonNeumannNeighborhood(int x, int y) {
        if (x + 1 < lightsArray.GetLength(0)) {
            lightsArray[x + 1, y].ToggleState = !lightsArray[x + 1, y].ToggleState;
        }
        if (x - 1 >= 0) {
            lightsArray[x - 1, y].ToggleState = !lightsArray[x - 1, y].ToggleState;
        }
        if (y + 1 < lightsArray.GetLength(1)) {
            lightsArray[x, y + 1].ToggleState = !lightsArray[x, y + 1].ToggleState;
        }
        if (y - 1 >= 0) {
            lightsArray[x, y - 1].ToggleState = !lightsArray[x, y - 1].ToggleState;
        }
    }

    #endregion
}
