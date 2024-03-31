using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement pMovement;
    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 3) // player layer
        {
            CameraShake.instance.ShakeCamera();
            AudioManager.Instance.PlayBumpSound();
            ParticleManager.Instance.PlayBumpParticle(GameManager.Instance.DreamPlayer.transform.position );
            GameManager.Instance.DreamPlayer.LosePoints();
            GameManager.Instance.NightmarePlayer.LosePoints();

            pMovement.UndoMove();
        }

        // dodati da se na osnovu rotacije gubi određen br poena nek se ovde zove Player.LosePoints(var a) 
        //gde je a direkcija u kojoj se igrač kretao kad su se sudarili, a var vrvt int
        //mozda ne mora ovde da se zove funkcija, to ces ti Srki da odlucis
    }
}
