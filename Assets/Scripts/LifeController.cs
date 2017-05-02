using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    private int Playerlife = 100;
    private bool isInvincible = false;

    public void setLife(int newValue) {
        Playerlife += newValue;
        if (Playerlife > 100) { Playerlife = 100; }
        if(Playerlife < 0) { Playerlife = 0; }
    }
    public int getLife() {
        return Playerlife;
    }

    public void setInvicibility(bool value) {
        isInvincible = value;
    }

    public bool getInvincible() {
        return isInvincible;
    }
}
