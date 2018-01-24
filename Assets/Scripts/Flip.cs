using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

    // Use this for initialization
    public PlatformController pc;
    bool facingright = true;

    void Start () {
        pc = GetComponent<PlatformController>();
    }
	
	// Update is called once per frame
	void Update () {

        foreach (Vector3 x in pc.globalWaypoints)
        {
            if (x.x == transform.position.x)
            {
                facingright = !facingright;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

    }

}

