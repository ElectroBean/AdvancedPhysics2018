using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRManager : MonoBehaviour {

    public LineRenderer lr;
    public Transform[] positions;

	// Use this for initialization
	void Start () {
        lr.SetPosition(0, positions[0].position);
        lr.SetPosition(1, positions[1].position);
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 direction = lr.GetPosition(1) - lr.GetPosition(0);
        if (Physics.Raycast(lr.GetPosition(0), direction, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Ragdoll>().RagdollOn = true;
            }
        }
	}


}
