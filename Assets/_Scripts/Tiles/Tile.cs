using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;
    public int team; //0 = none; 1 = p1; 2 =p2;
    public bool isFoggy;
    public bool isIcy;
    public bool isPlayer;
    public bool isPickable;
    public bool isStun;
    // Konstruktor
    public Tile()
    {
        team = 0;
        isFoggy = false;
        isIcy = false;
        isPickable = false;
        isPlayer = false;
    }
    public void paintTile(int teamId)
    {
        switch (teamId)
        {
            case 0:
                this.transform.GetComponent<Renderer>().material.color = Color.white;
                Debug.Log("Color white");
                break;
            case 1:
                this.transform.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("Color red");
                break;
            case 2:
                this.transform.GetComponent<Renderer>().material.color = Color.blue;
                Debug.Log("Color blue");
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // Pretpostavka da je sloj 3 rezervisan za igrača
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (this.team != player.team)
                {
                    paintTile(player.team);
                }
            }
        }
    }
}
