using UnityEngine;
//using System.Numerics;

public class Observer : MonoBehaviour
{
    public Transform player;    // Referenca ka Transform komponenti igrača

    bool playerInRange = false;    // Flag koji govori da li je igrač ušao u vidno polje neprijatelja

    void Update()
    {
        if (playerInRange)    // Da li je igrač u vidnom polju? Ako jeste, treba provjeriti da li ga neki objekat zaklanja
        {
            Vector3 direction = player.position - transform.position + Vector3.up;    // Odredi pravac pogleda posmatrača do tačke koja je 1 m iznad tačke u kojoj igrač trenutno stoji
            Ray ray = new Ray(transform.position, direction);    // Kreiraj polupravu/zrak od tačke pogleda u datom pravcu
            RaycastHit rh;    // Promjenljiva u kojoj će biti rezultat raycastinga

            if (Physics.Raycast(ray, out rh))    // Da li se poluprava pogleda siječe sa nekim objektom?
            {
                if (rh.collider.transform == player)    // Da li je taj objekat igrač?
                {
                    Debug.Log("Vidim te!");    // Ispiši tekst u konzoli
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform == player)    // Da li je objekat koji je ušao u collider igrač?
        {
            playerInRange = true;    // Postavi odgovarajući flag
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform == player)    // Da li je objekat koji je izašao iz collidera igrač?
        {
            playerInRange = true;    // Postavi odgovarajući flag
        }
    }
}
