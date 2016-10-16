using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public class Tuple<X,Y>
{
	public X first;
	public Y second;

	public Tuple(X first, Y second)
	{
		this.first = first;
		this.second = second;
	}
}


public class GameLinkedList<T>
{
    public GameNode<T> first;
    public GameNode<T> last;
}

public class GameNode<T>
{
    public T value { get; set; }
    public GameNode<T> next { get; set; }

    public GameNode(T val)
    {
        value = val;
    }
}

public class GameBoard : MonoBehaviour {

	private GameObject[,] board = new GameObject[10, 10];
    private System.Random rand = new System.Random();

    private GameObject newElement;

    public GameObject newElementLight;
    public GameObject currentPointerLight;
    public GameObject nextPointerLight;
    public GameObject nextNextPointerLight;
    public GameObject nullPointerLight;

    public AudioSource newElementSound;
    public AudioSource currentPointerSound;
    public AudioSource nextPointerSound;
    public AudioSource nextNextPointerSound;
    public AudioSource nullPointerSound;


    private GameLinkedList<GameObject> nodes = new GameLinkedList<GameObject>();
    private GameNode<GameObject> currentNode = null;

    GameObject createNodeAt(int x, int z)
	{
		// load the prefab "NodeCube" and create one on the board
		board [x, z] = Instantiate(Resources.Load("NodeCube")) as GameObject;
		board [x, z].transform.position = new Vector3 (x-5,0,z-5);

        return board[x, z];
	}

	Tuple<int,int> genRandCoord(System.Random rand)
	{
		return new Tuple<int,int> (rand.Next (0, 10), rand.Next (0, 10));
	}

    GameObject createNewRandomNode()
	{
		var newCoord = genRandCoord (rand);

		while (board [newCoord.first, newCoord.second] != null) {
			newCoord = genRandCoord (rand);
		}

		return createNodeAt (newCoord.first, newCoord.second);
	}

    private void genNodes()
    {
        for (int i = 0; i < 10; i++)
        {
            if (nodes.first == null)
            {
                var newNode = createNewRandomNode();
                nodes.first = new GameNode<GameObject>(newNode);
                nodes.last = nodes.first;
            }
            else
            {
                nodes.last.next = new GameNode<GameObject>(createNewRandomNode());
                nodes.last = nodes.last.next;
            }
        }
    }

	// Use this for initialization
	void Start () {

        genNodes();

        currentNode = nodes.first;
        moveLight(currentPointerLight, currentNode.value.transform.position);

        nextPointerLight.SetActive(false);
        nextNextPointerLight.SetActive(false);
        nullPointerLight.SetActive(false);
        newElementLight.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void moveLight(GameObject light, Vector3 position)
    {
        light.transform.position = new Vector3(position.x, 5.55f, position.z);
    }

    public void addNewNode()
    {
        var node = createNewRandomNode();
        newElementLight.SetActive(true);
        moveLight(newElementLight, node.transform.position);
        newElementSound.Play();
        newElement = node;
    }

    private void activateNullLight()
    {
        nullPointerLight.SetActive(true);
    }


    private Action nullOperation(Action act)
    {
        return () => {
            act();
            activateNullLight();
        };
    }

    private void pointToNull(GameNode<GameObject> node, GameObject previousLight)
    {
        nullOperation(() => {
            previousLight.SetActive(false);
            node.next = null;
        });
    }

    private void moveToNull()
    {
        nullOperation(() => {
            currentNode = null;
            currentPointerLight.SetActive(false);
        });
    }

    public void moveCurrentPointer()
    {
        if (currentNode.next == null) moveToNull();

        currentNode = currentNode.next;
        moveLight(currentPointerLight, currentNode.value.transform.position);
        currentPointerLight.SetActive(true);
        currentPointerSound.Play();
    }

    public void pointCurrentAtNewElement()
    {
        currentNode.next = new GameNode<GameObject>(newElement);
        newElement = null;
        newElementLight.SetActive(false);
    }

    public void pointCurrentAtNextElement()
    {

    }

    public void pointCurrentAtNextNextElement()
    {

    }
}
