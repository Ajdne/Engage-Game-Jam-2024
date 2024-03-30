using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // Pretpostavka da je sloj 3 rezervisan za igrača
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                OnPickUp(player); // Pozivamo apstraktnu metodu OnPickUp
                this.gameObject.SetActive(false); // Deaktiviramo pickable objekat
            }
        }
    }
    protected abstract void OnPickUp(Player player); // Apstraktna metoda koja će biti pregažena u izvedenim klasama
}
