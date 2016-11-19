using UnityEngine;
using System.Collections;

public class NodePointer : MonoBehaviour
{
    public NodePointer nullPointer;
    public GameNode node;
    public GameObject spotlight;
    public AudioSource sound;
    public bool isCurrentPointer = false;
    public bool isActive = false;

    private float distance = 45.9f;

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
            nullPointer.toggleNode();
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
            setNodeActive(false);
            if (isCurrentPointer) node.value.GetComponent<GameNode>().setNodeButtonActive(true);
        }

        var position = otherNode.value.transform.position;
        spotlight.transform.position = new Vector3(position.x, 5.55f, position.z);

        otherNode.value.GetComponent<GameNode>().newElementLight.SetActive(false);

		GameBoard.newElements.Remove (otherNode);

        node = otherNode;

        setNodeActive(true);
        if (isCurrentPointer) node.value.GetComponent<GameNode>().setNodeButtonActive(false);

		//Push to the gamenode stack
    }

    public void pointToAndActivate(GameNode otherNode)
    {
        pointTo(otherNode);
        node.value.SetActive(true);
    }


}