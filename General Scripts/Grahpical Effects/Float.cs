using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    public float period = 1;
    public float amp = 1;
    public float offset = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(0, (amp * 0.0001f) * Mathf.Sin(period * Time.time + offset), 0);
	}

}
