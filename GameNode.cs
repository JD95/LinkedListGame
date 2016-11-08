﻿using UnityEngine;
using System.Collections;

public class GameNode : MonoBehaviour {

    public GameObject value;
    public GameNode next;
    
    public GameObject deleteButton;
    public GameObject newElementLight;
    public AudioSource newElementSound;

    public float lineWidth = 0.2f;
	//private float distance = 45.9f;
	public AddPopupText popupText;


	void Start () {	
	}
	
	void Update () {
        //Debug.Log("ping");
        if (value == null) return;

        if (GameBoard.drawCube == value.GetComponent<GameNode>())
        {
            Debug.Log("Drawing connection line!");
            RaycastHit hit;

            // Get the point on the terrain where the mouse is
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0F);

            Debug.Log("Hit " + hit.transform.gameObject.name);

            drawArrow(hit.point);
        }

		if (next != null && next.value != null && next.value.transform.gameObject.activeSelf) {
			drawArrow (next.value.gameObject.transform.position);
			//Debug.Log (next.value.transform.gameObject.name);
			GetComponent<LineRenderer> ().enabled = true;
		} else {
			// make
			GetComponent<LineRenderer> ().enabled = false;
		}


    }

	public void deleteNode(){
		GameObject.Destroy (this.gameObject);

		popupText.makePopup ("You deleted a node!");
	}

    public void setNodeButtonActive(bool b)
    {
        if (deleteButton != null) deleteButton.gameObject.SetActive(b);
    }

    void OnMouseDown()
    {
        GameBoard.lineDrawing(this);
    }

    /********************
 * The arrow functions as follows:
 * If the node is not pointing at another node and if the player selected this node, then have the arrow follow the mouse.
 * If the node is pointing at another node, then the line stays on that object.
**********************/
    void drawArrow(Vector3 target)
    {
        LineRenderer lr = value.GetComponent<LineRenderer>();
        lr.SetWidth(lineWidth, lineWidth);
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetPosition(0, value.transform.position);
        lr.SetPosition(1, target);
    }
}
