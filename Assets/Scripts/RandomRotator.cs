using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
    public float tumble;
    private new Rigidbody rigidbody;
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        /*
         * Random vector3 value. Each X,yZ will be randomize individually
         * multiply by tumble
         */ 
        rigidbody.angularVelocity = Random.insideUnitSphere * tumble; 
	}
}
