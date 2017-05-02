using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    private new Rigidbody rigidbody;
    public float speed;
    /*
     * Use this for initialization
     * Executed the very first frame the object is intantiated
     * 
     */
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed; 
    }
	
}
