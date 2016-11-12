using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine.UI;

public class LightColors{
	public static Color green = new Color (0,1,0,1);
	public static Color grey = new Color (0.5f, 0.5f, 0.5f, 1f);
	public static Color purple = new Color (1,0,1,1);

}

public class GameBoard : MonoBehaviour {


    private static List<GameNode> newElements = new List<GameNode>();
    private static GameLinkedList nodes = new GameLinkedList();

    public static GameNode drawCube = null;
    public static bool boardGen = false;
    public static int actionCount;

    private System.Random rand = new System.Random();
    private const int numStartingNodes = 10;

	public Board board = new Board();
    public NodePointer currentPointer;
    public NodePointer nextPointer;
    public NodePointer nextNextPointer;
    public NodePointer nullPointer;
    public GameObject nullCube;
	public AddPopupText popupText;
    public WinCondition level;
    public bool fillBoard = false;

    // Use this for initialization
    void Start()
    {
        boardGen = !fillBoard;

        if (!boardGen)
            genNodes();

        currentPointer.pointToAndActivate(nodes.first);

        nextPointer.pointTo(currentPointer.node.next);

        nextNextPointer.pointTo(currentPointer.node.next.next);

        nullPointer.pointTo(nullCube.GetComponent<GameNode>());
    }

    void Update()
    {
        if (level.win())
        {
            Time.timeScale = 0;
            Debug.Log("You Win!");
        }
        else if (level.canProgress())
        {
            level.progress();
        }

        adjustLighting();
    }

    void adjustLighting() { 

		if (nextPointer.isActive && nextPointer.node != null){
			nextPointer.setNodeActive (true);
			nextPointer.spotlight.GetComponent<Light> ().color = LightColors.green;
		}

		if (nextPointer.node == null)
		{
			nextPointer.spotlight.GetComponent<Light> ().color = LightColors.grey;
		}

		if (nextNextPointer.isActive && nextPointer.node != null) {
			nextNextPointer.setNodeActive (true);
			nextNextPointer.spotlight.GetComponent<Light> ().color = LightColors.purple;

		}

		if (nextNextPointer.node == null) {
			nextNextPointer.spotlight.GetComponent<Light> ().color = LightColors.grey;
		}

    }

    public static void lineDrawing(GameNode selectedCube)
    {
        Debug.Log("Clicked");

        if (selectedCube == null) return;

        //Debug.Log("Selected Something!");
        if (drawCube == null)
        {
            Debug.Log("Drawing cube selected!");
            drawCube = selectedCube.GetComponent<GameNode>();
        }
        else if (drawCube == selectedCube)
        {
            Debug.Log("Drawing Cube deselected!");
            drawCube = null;
        }
        else if (drawCube != selectedCube && drawCube != null)
        {
            var lineNode = nodes.find(drawCube.gameObject, newElements);
            var selectedNode = nodes.find(selectedCube.transform.gameObject, newElements);

            if (lineNode != null && selectedNode != null)
            {
                Debug.Log("Nodes connected!");
                lineNode.next = selectedNode;
                actionCount++;
                lineNode.nextStack.Push((GameNode) selectedNode);
				lineNode.oldID.Push (lineNode.actionID);
                lineNode.actionID = actionCount;
            }

            drawCube = null;
        }

    }

    GameObject createNodeAt(int val, int x, int z)
	{
		// load the prefab "NodeCube" and create one on the board
		board.spaces [x, z] = Instantiate(Resources.Load("NodeCube")) as GameObject;
		board.spaces[x, z].transform.position = new Vector3 (x-5,0,z-5);
        board.spaces[x, z].transform.parent = gameObject.transform;

        var nodeText = board.spaces[x, z].GetComponentInChildren<Text>() as Text;

		board.spaces[x, z].GetComponent<GameNode> ().popupText = popupText;
		board.spaces[x, z].GetComponent<GameNode> ().nodeValue = val;
        nodeText.text = val.ToString();
        board.spaces[x, z].SetActive(false);

        return board.spaces[x, z];
	}

	Tuple<int,int> genRandCoord(System.Random rand)
	{
		return new Tuple<int,int> (rand.Next (0, 10), rand.Next (0, 10));
	}

    GameObject createNewRandomNode()
	{
		var newCoord = genRandCoord (rand);

		while (board.spaces[newCoord.first, newCoord.second] != null) {
			newCoord = genRandCoord (rand);
		}

		return createNodeAt(rand.Next(0,20), newCoord.first, newCoord.second);
	}

    private void genNodes()
    {
        for (int i = 0; i < numStartingNodes; i++)
        {
            if (nodes.first == null)
            {
                var newNode = createNewRandomNode();
                nodes.first = newNode.GetComponent<GameNode>();
                nodes.last = nodes.first;
            }
            else
            {
                nodes.last.next = createNewRandomNode().GetComponent<GameNode>();
                nodes.last = nodes.last.next;
            }
        }

        boardGen = true;
    }

	public GameNode addNewNodeReturn(){
		var node = createNewRandomNode().GetComponent<GameNode>();

		node.value.SetActive(true);
		node.newElementSound.Play();
		nodes.last = node;
		newElements.Add(node);

		actionCount++;
		node.actionID = actionCount;
		popupText.makePopup ("You created a new node!");

		return node;
	}

    public void addNewNode()
    {
		addNewNodeReturn ();
    }

    private void moveToNull()
    {
        currentPointer.togglePointer();
        nullPointer.toggleNode();
		//popupText.makePopup ("Null - End of list!");
    }

    public void moveCurrentPointer()
    {
        if (currentPointer.node.next == null)
        {
            moveToNull();
			Debug.Log ("move to null");
        }
        else
        {
            currentPointer.pointToAndActivate(currentPointer.node.next);
            currentPointer.sound.Play();
			//popupText.makePopup ("You advanced current pointer by one!");
        }
    }

    public void toggleNextPointer()
    {
        if(currentPointer.node.next == null)
        {
            nullPointer.togglePointer();
        }
        else
        {
            nextPointer.pointTo(currentPointer.node.next);
            nextPointer.togglePointer();
            nextPointer.sound.Play();
			//popupText.makePopup ("You toggled next pointer!");
        }
    }

    public void toggleNextNextPointer()
    {
        if(currentPointer.node.next == null || currentPointer.node.next.next == null)
        {
            nullPointer.togglePointer();
        }
        else
        {
            nextNextPointer.pointTo(currentPointer.node.next.next);
            nextNextPointer.togglePointer();
            nextNextPointer.sound.Play();
			//popupText.makePopup ("You toggled next next pointer!");
        }
    }

	public void undoAction(){
        //For the undo funcction to work, there must be an "action stack" 

        if (actionCount == 0)
            return;

		for (int i = newElements.Count - 1; i > 0; i--) {
			if (newElements [i].actionID == actionCount) {
				newElements [i].undo (ref actionCount);
				break;
			}
		}


        GameNode current = nodes.first;
        /*foreach (GameNode element in newElements)
        {
            element.undo(ref actionCount);
            //newElements.Remove(element);
        }
		*/

        while (current != null) //goes through all of the nodes and undo them accordingly.
        {
            current.undo(ref actionCount);
            current = current.next;
            //actionCount--;
        }
    }
}
