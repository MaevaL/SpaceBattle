using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    private new Rigidbody rigidbody;
    private new AudioSource audio;
    public float speed;
    public float tilt; // inclinaison
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire = 0.0f;
    public float fireRate;
    private float defaultFireRate;
    private List<PlayerBonus> bonus;
    private LifeController lifeController;

    /*
     * Execute update just before updating the frame every frame
     */
    private void Start() {
        GameObject lifeControllerObject = GameObject.FindWithTag("LifeController");
        if (lifeControllerObject != null)
        {
            lifeController = lifeControllerObject.GetComponent<LifeController>();
            if (lifeController == null)
            {
                Debug.Log("Cannot Find 'LifeController' script");
            }
        }

        bonus = new List<PlayerBonus>();
        defaultFireRate = fireRate;
    }
    void Update() {
        if (Input.GetKeyDown("space") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;

             /*
             * Clone and return an instance of an object
             */
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio = GetComponent<AudioSource>(); 
            audio.Play(); // Play the current audio clip
        }

        if(bonus.Count > 0) {
                for(int i = 0; i < bonus.Count; i++) {
                    PlayerBonus currentBonus = bonus[i];

                    DateTime endTime = currentBonus.getStartTime().AddSeconds(currentBonus.getDuration());
                    if (DateTime.Now >= endTime) {
                        if (currentBonus.getType() == "firerate")
                            fireRate = defaultFireRate;
                        else if (currentBonus.getType() == "invincibility")
                            lifeController.setInvicibility(false);

                        bonus.RemoveAt(i);
                    }
                }
        }
        
    }
    /*
     *  Automatically called before fixed physics step
     */
            void FixedUpdate() {
        /*
         * Executed once per physic step
         */

        /*
         * grabs player input
         */
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");


        rigidbody = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;
        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x,boundary.xMin, boundary.xMax), // block a value between a min and max
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt); 
    
    }

    public void setFireRate(float newFireRate) {
       fireRate = newFireRate;
    }


    public float getfireRate() {
        return fireRate;
    }

    public List<PlayerBonus> getBonus() {
        return bonus;
    }

    public void addBonus(PlayerBonus bonus) {
        this.bonus.Add(bonus);
    }
}
