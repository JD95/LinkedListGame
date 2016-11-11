using UnityEngine;
using System.Collections;

public class GameNode : MonoBehaviour {

    public GameObject value;
    public GameNode next;
    
    public GameObject deleteButton;
    public GameObject newElementLight;
    public AudioSource newElementSound;

    public float lineWidth = 0.01f;
	//private float distance = 45.9f;
	public AddPopupText popupText;
    public Stack nextStack;
	public Stack oldID;
    public int actionID;
    public Transform particle;


    void Start () {
        nextStack = new Stack();
		oldID = new Stack ();
        particle = transform.Find("Particle System");
    }
	
	void Update () {
        //Debug.Log("ping");
        if (value == null) return;

        if (GameBoard.drawCube == value.GetComponent<GameNode>())
        {
			GetComponent<LineRenderer> ().enabled = true;
            particle.gameObject.SetActive(true);
            //Debug.Log("Drawing connection line!");
            RaycastHit hit;

            // Get the point on the terrain where the mouse is
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0F);

            //Debug.Log("Hit " + hit.transform.gameObject.name);

            drawArrow(hit.point);
        }
		else if (next != null && next.value != null && next.value.transform.gameObject.activeSelf) {

			GetComponent<LineRenderer> ().enabled = true;
            particle.gameObject.SetActive(true);
            drawArrow (next.value.gameObject.transform.position);
			Debug.Log (next.value.transform.gameObject.name);
		} else {
			// make
			GetComponent<LineRenderer> ().enabled = false;
            particle.gameObject.SetActive(false);
        }


    }

	public void deleteNode(){
        if (popupText == null) Debug.Log("Popup text is null!");
		popupText.makePopup ("You deleted a node!");
		value.SetActive (false);
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
        Vector3 dir = target - transform.position;
        lr.SetWidth(0.5f, lineWidth);
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetPosition(0, value.transform.position);
        lr.SetPosition(1, target);
        particle.rotation = Quaternion.LookRotation(dir);
    }


    public void undo(ref int action)
    {
        Debug.Log("My: " + actionID);
        Debug.Log("Board: " + action);
		if (actionID == action) // check if this is the current action to be undone.
		{
			if (nextStack.Count == 0)
			{ //Basically if you just recently created this object

				action--;
				Destroy(gameObject);
				return;
			}

			nextStack.Pop();

			if (nextStack.Count > 0)
				next = (GameNode)nextStack.Peek(); //point to the previous node.
			else
				next = null;

			action--;
			actionID = (int)oldID.Pop ();

		}

		//nextStack.Pop();
		//next = (GameNode)nextStack.Peek(); //point to the previous node.

	}
}
