using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    private GameController gc;
    private PlayerController pc;
    [SerializeField]
    public int newlife;
    public float newfireRate;
    public int lifetimeBonus;
    
    // Use this for initialization
    void Start () {
        GameObject objectGame = GameObject.FindWithTag("GameController");
        gc = objectGame.GetComponent<GameController>();
        GameObject objectPlayer = GameObject.FindWithTag("Player");
        pc = objectPlayer.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("BonusLife")) {
            lifeBonus(newlife);
            Destroy(collider.gameObject);
        } else if (collider.CompareTag("BonusFireRate")){
            fireRateBonus(newfireRate);
            Destroy(collider.gameObject);
        } else if (collider.CompareTag("BonusInvincible")) {
            invincibilityBonus();
            Destroy(collider.gameObject);

        }
    }
    void lifeBonus(int lifeValue) {
        gc.setLife(lifeValue);
    }

    void fireRateBonus(float newFireRate) {
        float oldfireRate = pc.getfireRate();
        pc.setFireRate(newFireRate);
        pc.addBonus(new PlayerBonus("firerate", lifetimeBonus));
    }

    void invincibilityBonus() {
        gc.setInvisibility(true);
        pc.addBonus(new PlayerBonus("invincibility", lifetimeBonus));
    }
}
