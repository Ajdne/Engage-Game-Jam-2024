using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int Id;
    public bool isSheep;
    public bool isStun;

    // Konstruktor
    public Pickable()
    {
        isSheep = false;
        isStun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null && isSheep)
            {
                player.numberOfSheep++; // Povecavamo skor igraca
                this.gameObject.SetActive(false); // Deaktiviramo pickable objekat
            }
            if (player != null && isStun)
            {
                player.Stun(); // Stanujemo igraca
                this.gameObject.SetActive(false); // Deaktiviramo pickable objekat
            }
        }
    }
}
