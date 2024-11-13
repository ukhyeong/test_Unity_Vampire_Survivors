using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Infotype { Exp, Level, Kill, Time, Health } // Infotype
    public Infotype type;

    Text myText;
    Slider mySlider;

    void Awake() 
    {
        this.myText = GetComponent<Text>();
        this.mySlider = GetComponent<Slider>();
    } // Awake

    void LateUpdate() 
    {
        switch (this.type) {
            case Infotype.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                this.mySlider.value = curExp / maxExp;
                break;
            case Infotype.Level:
                // 소수점 없음 F0
                this.myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case Infotype.Kill:
                this.myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case Infotype.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                this.myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case Infotype.Health:
                break;
        } // switch

    } // LatedUpdate

} // end class
