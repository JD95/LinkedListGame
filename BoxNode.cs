using UnityEngine;
using System.Collections;

public class BoxNode : MonoBehaviour {
	public BoxNode next = null;
	public GameObject nodeManager;
	private bool wasClicked = false;
	private bool lineDrawn = false;
	private float distance = 45.9f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (wasClicked) {
			if (nodeManager.GetComponent<NodeManager> ().currentNode == null) {
				nodeManager.GetComponent<NodeManager> ().currentNode = this;
				nodeManager.GetComponent<NodeManager> ().activeSelection = true;
			}
			if (nodeManager.GetComponent<NodeManager> ().currentNode == this) {
				drawArrow (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance)));
			}
		}

		if (next != null) {
			drawArrow (next.gameObject.transform.position);
		}
	}

	void OnMouseDown(){
		wasClicked = !wasClicked;
		if (nodeManager.GetComponent<NodeManager> ().currentNode == this) {
			nodeManager.GetComponent<NodeManager> ().currentNode = null;
		}
		if (nodeManager.GetComponent<NodeManager> ().currentNode != this && nodeManager.GetComponent<NodeManager> ().currentNode != null) {
			setNextNode(this.GetComponent<BoxNode>());
		}
	}

	/********************
	 * The arrow functions as follows:
	 * If the node is not pointing at another node and if the player selected this node, then have the arrow follow the mouse.
	 * If the node is pointing at another node, then the line stays on that object.
	**********************/

	void drawArrow(Vector3 target){
		//Vector3 end = new Vector3 (mousePos.x, -mousePos.y, 45.9f);
		//Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(target.x, target.y, distance));
		Vector3 end = target;
		LineRenderer lr = this.GetComponent<LineRenderer> ();
		lr.material = new Material (Shader.Find ("Particles/Alpha Blended Premultiply"));
		lr.SetPosition (0, this.transform.position);
		lr.SetPosition (1, end);

		Debug.Log (end);
	}

	void setNextNode(BoxNode hit){
		nodeManager.GetComponent<NodeManager> ().currentNode.next = hit;
		nodeManager.GetComponent<NodeManager> ().currentNode.wasClicked = !wasClicked;
		nodeManager.GetComponent<NodeManager> ().currentNode = null;
	}
}
