using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

    [Space, Header("Animation")]
    public Animator _anim;

    [Space, Header("Sounds")]
    public AudioSource _audio;

    public AudioClip loseSound;
    public AudioClip winSound;

    [Space, Header("URL of PHP script on your site")]
    public string url = "http://example.com/script.php";
    public static string _url;

    [Space, Header("ID")]
    public int ID;

    //score variables
    private float Score = 4200;
    private float losedMoney = 0;
    private float SetMoney = 0;

    //variables for functions
    private List<int> Indexes;
    private int SatNumber = 3;

    //Start
    private async void Start()
    {
        setMoney.text = "Wagered money: ";
        _url = url;
        Score = float.Parse(await GetBalanse(ID.ToString()));
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
        if (Score - int.Parse(TextInput.text) < 0)
        {
            OnNotEnothMoney();

            return;
        }

        Wheel.RotateWheel(720.0f + (ChoosOneRandom(Numbers, GetNeedNumber()) * (360.0f / Numbers.Count)) + 360.0f / Numbers.Count / 2);

        SetMoney = int.Parse(TextInput.text);
        Score -= SetMoney;
        setMoney.text = "Wagered money: " + TextInput.text;
    }
    public async void OnWheelStop()
    {
        if (Arrow.nowNumber == SatNumber)
        {
            losedMoney = 0;
            Win();
        }
        else
        {
            _audio.PlayOneShot(loseSound);
            losedMoney += SetMoney;
            await ChangeResultBalanse(ID.ToString(), (SetMoney * -1).ToString(), "1");
            SetMoney = 0;
            TextInput.text = "";
        }
        TextInput.text = "";
        setMoney.text = "Wagered money: ";
    }


    //ather functions
    public async void Win()
    {
        _anim.SetTrigger("Win");
        _audio.PlayOneShot(winSound);
        Score += (SetMoney * SatNumber);
        await ChangeResultBalanse(ID.ToString(), (SetMoney * SatNumber).ToString(), "1");
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


    //Ui functions
    public void IncreaseSetedMoney()
    {
        if (TextInput.text == "") 
        {
            TextInput.text = "1";
            return;
        }
        TextInput.text = (int.Parse(TextInput.text) + 1).ToString();
    }
    public void DicrieseSetedMoney()
    {
        if(TextInput.text == "" || TextInput.text == "1")
        {
            return;
        }
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

        for (int i = 0; i < List.Count; i++)
        {
            if (List[i] == Need)
            {
                Indexes.Add(i);
            }
        }

        return Indexes[UnityEngine.Random.Range(0, Indexes.Count)];
    }

    public int GetNeedNumber()
    {
        float I = UnityEngine.Random.Range(0, 100);

        if (I * DateTime.Now.Minute / 60 < losedMoney / 10)
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

        if (i * DateTime.Now.Second / 60 < rate)
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



    //API requests

    public async Task ChangeResultBalanse(string ID, string BalanseAdding, string Type)
    {
        var httpClient = new HttpClient();

        var values = new Dictionary<string, string>
        {
            { "userID", ID },
            { "balanceOpiration", BalanseAdding },
            { "Type", Type }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await httpClient.PostAsync(_url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        Debug.Log(responseString);
    }

    public static async Task<string> GetBalanse(string ID)
    {
        using var client = new HttpClient();

        using var response = await client.GetAsync(_url + "?" + "ID" + "=" + ID);

        return await response.Content.ReadAsStringAsync();
    }
}
