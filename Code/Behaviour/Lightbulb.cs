using UnityEngine;
using System.Collections;

public class Lightbulb : MonoBehaviour {
    //true -> light is on
    public bool ToggleState {
        get; set;
    }

    public int X {
        get; set;
    }

    public int Y {
        get; set;
    }

    private Light myLight;
    private GameManager gm;

	// Use this for initialization
	void Start () {
        myLight = GetComponentInChildren<Light>();
        gm = Camera.main.GetComponent<GameManager>();
        myLight.enabled = ToggleState;
	}

    void Update() {
        if(myLight.enabled != ToggleState) {
            myLight.enabled = ToggleState;
        }
    }

    void OnMouseUp() {
        this.Toggle();
    }

    private void Toggle() {
        ToggleState = !ToggleState;
        gm.VonNeumannNeighborhood(this.X, this.Y);
        gm.MoveCount++;
        gm.CheckWinCondition();
    }
}
