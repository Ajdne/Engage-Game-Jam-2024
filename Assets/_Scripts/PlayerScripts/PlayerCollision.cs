using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement pMovement;


    Player thisplayer;
    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        thisplayer = GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 3) // player layer
        {
            CameraShake.instance.ShakeCamera();
            //AudioManager.Instance.PlayBumpSound();
            EventManager.Instance.startSFXEvent("Bump");
            ParticleManager.Instance.PlayBumpParticle(this.transform.position );
            thisplayer.LosePoints();

            pMovement.UndoMove();
        }

        // dodati da se na osnovu rotacije gubi određen br poena nek se ovde zove Player.LosePoints(var a) 
        //gde je a direkcija u kojoj se igrač kretao kad su se sudarili, a var vrvt int
        //mozda ne mora ovde da se zove funkcija, to ces ti Srki da odlucis
    }
}
