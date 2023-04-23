using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    //functions variables
    [Header("Arrow")]
    public Arrow Arrow;

    [Space, Header("Numbers in wheel")]
    public List<int> Numbers = new List<int>();

    [Space, Header("Toggles")]
    public List<Toggle> Toggles = new List<Toggle>();

    [Space, Header("Ui variables")]
    public TMP_Text nowScoreText;
    public TMP_Text setMoney;
    public TMP_InputField TextInput;
    public GameObject NotEnothMoneyText;

    //score variables
    private float Score = 4200;
    private float losedMoney = 0;
    private float SetMoney = 0;

    //variables for functions
    private List<int> Indexes;
    private int SatNumber = 3;

    //Start
    private void Start()
    {
        setMoney.text = "Wagered money: ";
    }

    //Update
    private void Update()
    {
        nowScoreText.text = "Your money: " + Score.ToString();

        for (int i = 0; i < Toggles.Count; i++)
        {
            if (Toggles[i].isOn)
            {
                switch (i)
                {
                    case 0:
                        SatNumber = 2;
                        break;
                    case 1:
                        SatNumber = 3;
                        break;
                    case 2:
                        SatNumber = 5;
                        break;
                    case 3:
                        SatNumber = 8;
                        break;
                    case 4:
                        SatNumber = 10;
                        break;
                    case 5:
                        SatNumber = 15;
                        break;
                    case 6:
                        SatNumber = 20;
                        break;
                    case 7:
                        SatNumber = 50;
                        break;

                    default:
                        break;
                }
                break;
            }
        }
    }

    public void StartWheel()
    {
        if (TextInput.text == "") return;
        if(Score-int.Parse(TextInput.text) < 0)
        {
            OnNotEnothMoney();

            return;
        }

        Wheel.RotateWheel(720.0f + (ChoosOneRandom(Numbers, GetNeedNumber()) * (360.0f / Numbers.Count)) + 360.0f / Numbers.Count / 2);

        SetMoney = int.Parse(TextInput.text);
        Score -= SetMoney;
        setMoney.text = "Wagered money: " + TextInput.text;
    }
    public void OnWheelStop()
    {
        if (Arrow.nowNumber == SatNumber)
        {
            losedMoney = 0;
            Win();
        }
        else
        {
            losedMoney += SetMoney;
            SetMoney = 0;
            TextInput.text = "";
        }
        TextInput.text = "";
        setMoney.text = "Wagered money: ";
    }


    //ather functions
    public void Win()
    {
        Score += (SetMoney * SatNumber);
    }

    public void OnNotEnothMoney()
    {
        TextInput.text = "";
        LeanTween.scale(NotEnothMoneyText, new Vector2(1, 1), 1).setOnComplete(OnNotEnothMoneyFinish);
    }
    public void OnNotEnothMoneyFinish()
    {
        LeanTween.scale(NotEnothMoneyText, new Vector2(0, 0), 2);
    }


    //Ui variables
    public void IncreaseSetedMoney()
    {
        TextInput.text = (int.Parse(TextInput.text) + 1).ToString();
    }
    public void DicrieseSetedMoney()
    {
        TextInput.text = (int.Parse(TextInput.text) - 1).ToString();
    }
    public void AllIn()
    {
        TextInput.text = Score.ToString();
    }

    //wheel logic
    public float ChoosOneRandom(List<int> List, int Need)
    {
        Indexes = new List<int>();

        for(int i = 0; i<List.Count; i++)
        {
            if(List[i] == Need)
            {
                Indexes.Add(i);
            }
        }

        return Indexes[UnityEngine.Random.Range(0, Indexes.Count)];
    }

    public int GetNeedNumber()
    {
        float I = UnityEngine.Random.Range(0, 100);

        if (I * DateTime.Now.Minute / 60 < losedMoney/10)
        {
            return SatNumber;
        }
        else
        {
            return Numbers[UnityEngine.Random.Range(0, Numbers.Count)];
        }
    }

    public bool random(float rate)
    {
        int i = UnityEngine.Random.Range(0, 100);

        if (i * DateTime.Now.Minute / 60 < rate)
        {
            Debug.Log("true");
            return true;
        }
        Debug.Log("false");
        return false;
    }

    public void SetNumber(int Number)
    {
        SatNumber = Number;
    }
}
