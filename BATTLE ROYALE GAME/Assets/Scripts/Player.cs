using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {
    public Vector3 positoin;
    public Quaternion rotation;
    public int sessionId;
    bool isMoving;

    public Player(Vector3 positoin, Quaternion rotation, int sessionId)
    {
        this.positoin = positoin;
        this.rotation = rotation;
        this.sessionId = sessionId;
    }
}
