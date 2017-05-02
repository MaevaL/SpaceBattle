using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBonus {

    private DateTime startTime;
    private int duration;
    private String type;

    public PlayerBonus(String type, int duration)
    {
        this.startTime = DateTime.Now;
        this.duration = duration;
        this.type = type;

    }

    public DateTime getStartTime() {
        return startTime;
    }

    public int getDuration() {
        return duration;
    }

    public String getType() {
        return type;
    }
}
