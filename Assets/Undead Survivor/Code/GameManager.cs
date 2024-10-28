using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 60f * 0.3f;

    public PoolManager pool;
    public Player player;

    void Awake() 
    {
        GameManager.instance = this;
    } // Awake

    private void Update() {
        this.gameTime += Time.deltaTime;

        if (this.gameTime > this.maxGameTime) {
            this.gameTime = this.maxGameTime;
        } // if

    } // Update

} // end class
