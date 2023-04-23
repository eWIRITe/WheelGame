using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int nowNumber;

    public List<GameObject> Coliders = new List<GameObject>();

    public PolygonCollider2D _pc2D;

    public Transform PosForRay;

    private Ray _ray;

    private void Start()
    {
        _pc2D = GetComponent<PolygonCollider2D>();
        _ray = new Ray(PosForRay.position, Vector3.forward);
    }

    private void Update()
    {
        Debug.DrawRay(_ray.origin, _ray.direction * 10);

        RaycastHit2D hit = Physics2D.Raycast(PosForRay.position, Vector3.forward);
        
        if (hit.collider != null)
        {
            switch (hit.collider.tag)
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
        else Debug.Log("No hit colider");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeanTween.rotateZ(this.gameObject, -15, 0.2f).setOnComplete(ReturnToStandart);
    }
    public void ReturnToStandart()
    {
        LeanTween.rotateZ(this.gameObject, 0, 0.3f);
    }

}
