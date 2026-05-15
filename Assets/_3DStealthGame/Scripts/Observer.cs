using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;    // Referenca ka Transform komponenti igrača
    public GameEnding gameEnding;
    bool PlayerInRange = false;    // Flag koji govori da li je igrač ušao u vidno polje neprijatelja

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)    // Da li je objekat koji je ušao u collider igrač?
        {
            PlayerInRange = true;    // Postavi odgovarajući flag
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)    // Da li je objekat koji je izašao iz collidera igrač?
        {
            PlayerInRange = false;    // Postavi odgovarajući flag
        }
    }

    void Update()
    {
        if (PlayerInRange)  // Da li je igrač u vidnom polju? Ako jeste, treba provjeriti da li ga neki objekat zaklanja
        {
            Vector3 direction = player.position - transform.position + Vector3.up;    // Odredi pravac pogleda posmatrača do tačke koja je 1 m iznad tačke u kojoj igrač trenutno stoji
            Ray ray = new Ray(transform.position, direction);    // Kreiraj polupravu/zrak od tačke pogleda u datom pravcu
            RaycastHit raycastHit;    // Promjenljiva u kojoj će biti rezultat raycastinga

            if(Physics.Raycast(ray, out raycastHit))    // Da li se poluprava pogleda siječe sa nekim objektom?
            {
                if (raycastHit.collider.transform == player)    // Da li je taj objekat igrač?
                {
                    gameEnding.CaughtPlayer();    // Proglasi igrača uhvaćenim pozivom odgovarajuće metode iz klase GameEnding
                }
            }
        }
    }
}
