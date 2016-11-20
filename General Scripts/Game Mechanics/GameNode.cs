using UnityEngine;
using System.Collections;
using UnityEditor;

public enum Action_Type
{
    POINT_AT,
    DELETE,
    TOGGLE,
    NEWNODE,
    NONE
}

public class Action
{
    public Action_Type type;
    public GameNode oldNode;
    public int oldID;

    public Action()
    {
        oldNode = null;
        oldID = 0;
        type = Action_Type.NONE;
    }
    /*
		Action objects are instantiated after every action made as called onto the gameBoard.
		Every game node has a stack of Actions that will pass in the case of...
		
		Point at new Node:
		act = new Action(POINT_AT, actionID, current.next);
		
		Deleting Node:
		act = new Action(DELETE, actionID, null);
		action_stack.push((Action) act)
	*/
    public Action(Action_Type t, int id, GameNode node)
    {
        type = t;
        oldID = id;
        oldNode = node;
    }

    /*Undo Methods:
		All functions accept the current GameNode
		Delete Node (GameNode n) -
		Point Node (GameNode n)- 
		Toggle Node (GameNode n) -
	*/
    public void undoDelete(GameNode n)
    {
        n.value.SetActive(true);
    }
    public void undoPoint(GameNode n)
    {
        //Debug.Log(oldNode.nodeValue);
        n.next = oldNode;
    }
    public void undoToggle(GameNode n)
    {

    }
}

public class GameNode : MonoBehaviour {

    public GameObject value;
    public GameNode next;
	public string nodeValue;
    public bool deleted = false;

    public GameObject deleteButton;
    public GameObject newElementLight;
    public AudioSource newElementSound;

    public float lineWidth = 0.01f;
	public AddPopupText popupText;
    public Transform particle;
	public bool isNull = false;

    private ParticleSystem particles;
    public Stack actionStack;

    void Start () {
        actionStack = new Stack();

        particle = transform.Find("Particle System");
        particles = particle.gameObject.GetComponent<ParticleSystem>();
        
    }
	
	void Update () {

        if (value == null) return;

        if (GameBoard.drawCube == value.GetComponent<GameNode>())
        {
			GetComponent<LineRenderer> ().enabled = true;
            particle.gameObject.SetActive(true);

            RaycastHit hit;

            // Get the point on the terrain where the mouse is
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0F);

            drawArrow(hit.point);
        }
		else if (next != null && next.value != null && next.value.transform.gameObject.activeSelf) {

			GetComponent<LineRenderer> ().enabled = true;
            particle.gameObject.SetActive(true);
            drawArrow (next.value.gameObject.transform.position);
		}
        else {
			GetComponent<LineRenderer> ().enabled = false;
            particle.gameObject.SetActive(false);
        }


    }

	public void deleteNode(bool display_message){
        if (popupText == null) Debug.Log("Popup text is null!");
		if (display_message) popupText.makePopup ("You deleted a node!");
        Action temp = new Action(Action_Type.DELETE, GameBoard.actionCount++, null);
		value.SetActive (false); 
        deleted = true;
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

        // Make particles go only to the edge of the line
        particles.startLifetime = dir.magnitude / particles.startSpeed;
    }


    public void undo(ref int action, Backlog l)
    {
        if (actionStack.Count == 0)
            return;

        Action a = (Action) actionStack.Peek();
        
        if (a.oldID == action) // check if this is the current action to be undone.
        {
            Action_Type type = a.type;

            //if (type.Equals(Action_Type.POINT_AT))
            //a.undoPoint(this);
            
            switch (type)
            {
                case Action_Type.POINT_AT:
                    a.undoPoint(this);
                    break;
                case Action_Type.DELETE:
                    a.undoDelete(this);
                    break;
                case Action_Type.TOGGLE:
                    a.undoToggle(this);
                    break;
                default:
                    break;
            }
            
            actionStack.Pop();
            l.log.RemoveAt(l.log.Count - 1);
            l.outputLog(3);
            action--;
        }
    }
}
