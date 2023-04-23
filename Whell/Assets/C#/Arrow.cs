using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int nowNumber;

    public List<GameObject> Coliders = new List<GameObject>();

    public PolygonCollider2D _pc2D;

    private void Start()
    {
        _pc2D = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coliders.Add(collision.gameObject);

        LeanTween.rotateZ(this.gameObject, -15, 0.2f).setOnComplete(ReturnToStandart);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Coliders.Remove(collision.gameObject);
        if (Coliders.Count > 0)
        {

            switch (Coliders[0].tag)
            {
                case "2":
                    nowNumber = 2;
                    break;

                case "3":
                    nowNumber = 3;
                    break;

                case "5":
                    nowNumber = 5;
                    break;

                case "8":
                    nowNumber = 8;
                    break;

                case "10":
                    nowNumber = 10;
                    break;

                case "15":
                    nowNumber = 15;
                    break;

                case "20":
                    nowNumber = 20;
                    break;

                case "50":
                    nowNumber = 50;
                    break;

                default:
                    break;
            }
        }
    }

    public void ReturnToStandart()
    {
        LeanTween.rotateZ(this.gameObject, 0, 0.3f);
    }

}
