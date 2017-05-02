using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
    void OnTriggerExit(Collider collider) {
        Destroy(collider.gameObject);
    }
}
