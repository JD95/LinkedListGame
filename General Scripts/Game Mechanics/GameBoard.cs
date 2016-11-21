﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine.UI;

public class LightColors{
	public static Color green = new Color (0,1,0,1);
	public static Color grey = new Color (0.5f, 0.5f, 0.5f, 1f);
	public static Color purple = new Color (1,0,1,1);
    public static Color yellow = new Color(1, 0.5f, 0, 1);

}

public class Backlog {
    public List<string> log;
    public Text ui;
    public Backlog() {
        log = new List<string>();
    }

    public void outputLog(int size) {
        string[] output = log.ToArray();
        int length = output.Length;
        //Array.Reverse(output);
        string outString = "";
        ui.text = "";

        for (int i = length-size; i < length; i++) {
            if (i >= length)
                break;
 
            if (i == 0)
                outString = "\n";

            //Debug.Log(output[i]);
            outString = outString + "\n" + output[i];
        }
        ui.text = outString;
    }
}

public class GameBoard : MonoBehaviour {
    public static List<GameNode> newElements = new List<GameNode>();

    public static GameLinkedList nodes = new GameLinkedList();
    public static GameNode drawCube = null;
    public static bool boardGen = false;
    public static int actionCount;
	public static bool inErrorState = false;
    public static Backlog log = new Backlog();

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
    public AudioSource invalidSound;
    public bool fillBoard = false;
	public ButtonBlink undoButtonBlink;
    public Text blog;
    public ProgressMessages progressMessages;

    // Use this for initialization
    void Start()
    {
		board.game = this;
        boardGen = !fillBoard;
        log.ui = blog;

        if (!boardGen)
        {
            genNodes();

            currentPointer.pointToAndActivate(nodes.first);

            nextPointer.pointTo(currentPointer.node.next);
            nextPointer.togglePointer();

            nextNextPointer.pointTo(currentPointer.node.next.next);
            nextNextPointer.togglePointer();
        }
            
        nullPointer.pointTo(nullCube.GetComponent<GameNode>());
    }

    void Update()
    {
        adjustLighting();

		if (!inErrorState && !board.noLeaks (nodes, newElements)) { // check for memory leaks 
			Debug.Log("Memory leaks detected.");
			//Time.timeScale = 0;
			popupText.makePopup ("A memory leak error detected. You can press Undo button to fix it or restart the level.");
			inErrorState = true;
			undoButtonBlink.blink = true;
		}

    }

    void adjustLighting() {

        adjustLight(currentPointer, LightColors.yellow);

        adjustLight(nextPointer, LightColors.green);

        adjustLight(nextNextPointer, LightColors.purple);

    }

    void adjustLight(NodePointer pointer, Color color)
    {
        if (pointer.isActive && pointer.node != null && !pointer.node.deleted)
        {
            pointer.setNodeActive(true);
            pointer.spotlight.GetComponent<Light>().color = color;
        }

        if (pointer.node == null)
        {
            pointer.spotlight.GetComponent<Light>().color = LightColors.grey;
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
            /*In the case that node points to a new node...*/
            var lineNode = nodes.find(drawCube.gameObject, newElements);
            var selectedNode = nodes.find(selectedCube.transform.gameObject, newElements);

            if (lineNode != null && selectedNode != null)
            {
                Debug.Log("Nodes connected!");
				newElements.Remove (selectedNode);
                actionCount++;
                /*
                lineNode.nextStack.Push((GameNode) selectedNode);
				lineNode.oldID.Push (lineNode.actionID);
                lineNode.actionID = actionCount;
                */
                Action temp = new Action(Action_Type.POINT_AT, actionCount, lineNode.next);
                lineNode.next = selectedNode;
                lineNode.actionStack.Push((Action) temp);
                log.log.Add(lineNode.nodeValue + " points to " + selectedNode.nodeValue);
                //Debug.Log(actionCount);
                log.outputLog(3);
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
		board.spaces[x, z].GetComponent<GameNode> ().nodeValue = val.ToString();
        nodeText.text = val.ToString();

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
                nodes.first = addNewListNodeReturn(false, false);
            }
            else
            {
                addNewListNodeReturn(false, false);
            }
        }

        boardGen = true;
    }

	public GameNode addNewNodeReturn(bool active, bool display_message){

        var node = createNewRandomNode().GetComponent<GameNode>();

        node.value.SetActive(active);
		newElements.Add(node);

		actionCount++;
        //node.actionID = actionCount;
        //Action temp = new Action(Action_Type.NEWNODE, actionCount, null);

        if (display_message)
        {
            popupText.makePopup("You created a new node!");
            node.newElementSound.Play();
        }

		return node;
	}

    public GameNode addNewListNodeReturn (bool active, bool display_message)
    {
        var node = addNewNodeReturn(active, display_message);

        if(nodes.last != null)
            nodes.last.next = node;
        nodes.last = node;

        return node;
    }

    public void addNewNode()
    {
		addNewNodeReturn (true, true);
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
			popupText.makePopup ("You advanced current pointer by one!");
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
			popupText.makePopup ("You toggled next pointer!");
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
			popupText.makePopup ("You toggled next next pointer!");
        }
    }

	public void undoAction(){
        //For the undo funcction to work, there must be an "action stack" 
		/*if(inErrorState) inErrorState = false;
		undoButtonBlink.blink = false;
        */

        if (actionCount == 0)
            return;

        GameNode current = nodes.first;

        while (current != null) //goes through all of the nodes and undo them accordingly.
        {
            current.undo(ref actionCount, log);
            current = current.next;
        }
    }

    public void printList()
    {
        Debug.Log(nodes.ToString());
    }

    public void progress()
    {
        if (level.canProgress())
        {
            level.progress();

            //if (level.win()) Time.timeScale = 0;
        }
        else
        {
            invalidSound.Play();
            progressMessages.playError();
        }
    }
}
