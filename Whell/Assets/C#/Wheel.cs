using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public LeanTweenType WheelMove;
    public static LeanTweenType _wheelMove;

    public static GameObject self;

    private static bool isAbleToMove = true;

    public static bool stopped = false;

    public Arrow _arrow;

    private void Start()
    {
        self = this.gameObject;

        _wheelMove = WheelMove;
    }

    public static void RotateWheel(float intensivity)
    {
        if (isAbleToMove) LeanTween.rotateZ(self, intensivity, 10).setEase(_wheelMove).setOnComplete(OnFinish);
        isAbleToMove = false;
    }

    public static void OnFinish()
    {
        isAbleToMove = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().OnWheelStop();
    }
}
