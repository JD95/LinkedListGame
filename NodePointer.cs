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
        spotlight.SetActive(active);
        node.value.SetActive(active);
        isActive = active;
    }

    public void toggleNode()
    {
        setNodeActive(!node.value.activeSelf);
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
    }

    public void pointTo(GameNode otherNode)
    {
        if (isCurrentPointer && node != null)
        {
            node.value.GetComponent<GameNode>().setNodeButtonActive(true);
            node.value.SetActive(false);
        }

        var position = otherNode.value.transform.position;
        spotlight.transform.position = new Vector3(position.x, 5.55f, position.z);

        otherNode.value.GetComponent<GameNode>().newElementLight.SetActive(false);

        node = otherNode;

        if (isCurrentPointer) node.value.GetComponent<GameNode>().setNodeButtonActive(false);
    }

    public void pointToAndActivate(GameNode otherNode)
    {
        pointTo(otherNode);
        node.value.SetActive(true);
    }


}