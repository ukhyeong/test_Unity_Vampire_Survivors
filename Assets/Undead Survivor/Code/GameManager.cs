using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime = 0;
    public float maxGameTime = 2f * 10f;

    [Header("# Game Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
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
    public void GetExp()
    {
        this.exp++;

        if (exp == nextExp[level]) {
            level++;
            this.exp = 0;
        } // if

    } // GetExp
} // end class
