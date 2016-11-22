using UnityEngine;
using System.Collections.Generic;

public class NodePointer : MonoBehaviour
{
    public NodePointer nullPointer;
    public GameNode node;
    public GameObject spotlight;
    public AudioSource sound;
    public bool isCurrentPointer = false;
    public bool isActive = false;
    public Stack<GameNode> pointerMoves = new Stack<GameNode>();

    public void Start()
    {

    }

    public void Update()
    { }

    public NodePointer()
    { }

    public NodePointer(GameNode ptr, GameObject light, AudioSource sound)
    {
        this.node = ptr;
        this.spotlight = light;
        this.sound = sound;
    }

    public void setNodeActive(bool active)
    {
		if (node != null) {
            Debug.Log("Setting " + gameObject.name + "'s node to be " + active);
			spotlight.SetActive (active);
			node.value.SetActive (active);		
		}
    }

    public void toggleNode()
    {     
        setNodeActive(!isActive);
    }

    public void togglePointer()
    {
        if (node == null)
        {
            nullPointer.togglePointer();
            setNodeActive(false);
        }
        else
        {
            toggleNode();
        }

        isActive = !isActive;
    }

    public void pointTo(GameNode otherNode)
    {
        if (node != null)
        {
            setNodeActive(false); // Turn off the current node
            if (isCurrentPointer) node.value.GetComponent<GameNode>().setNodeButtonActive(true);
        }

        otherNode.value.GetComponent<GameNode>().newElementLight.SetActive(false);

        var position = otherNode.value.transform.position;
        spotlight.transform.position = new Vector3(position.x, 5.55f, position.z);

        node = otherNode;

        setNodeActive(true);
        if (isCurrentPointer) node.value.GetComponent<GameNode>().setNodeButtonActive(false);
    }

    public void pointToAndActivate(GameNode otherNode)
    {
        pointTo(otherNode);
    }


}