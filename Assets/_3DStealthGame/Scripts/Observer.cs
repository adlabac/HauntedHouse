using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool PlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            PlayerInRange = false;
        }
    }

    void Update()
    {
        if (PlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    Debug.Log("Vidim te!");
                }
            }
        }
    }
}
