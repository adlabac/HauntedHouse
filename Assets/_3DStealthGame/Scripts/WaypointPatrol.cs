using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    public float moveSpeed = 1.0f;    // Parametar kojim se definiše brzina kretanja
    public float turnSpeed = 10.0f;    // Parametar kojim se definiše brzina promjene pravca

    public Transform[] waypoints;    // Tačke koje odre]uju putanju

    private Rigidbody rb;    // Referenca na Rigidbody komponentu
    int currentWaypointIndex = 0;    // Indeks tačke ka kojoj se trenutno kreće

    void Start ()
    {
        rb = GetComponent<Rigidbody>();    // Preuzimanje Rigidbody komponente
    }

    void FixedUpdate ()
    {
        Transform waypoint = waypoints[currentWaypointIndex];    // Pozicija ciljne tačke ka kojoj se trenutno kreće
        Vector3 targetDirection = waypoint.position - rb.position;    //  Vektor od trenutne pozicije lika do ciljne tačke

        if (targetDirection.magnitude < 0.1f)    // Da li je lik došao blizu ciljne tačke?
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;  // Odredi indeks sljedeće tačke
        }

        Vector3 ghostDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0f);    // Odredi željeni ugao okretanja lika, u zavisnosti od proteklog vremena, ali ne brže od zadatog parametra
        Quaternion forwardRotation = Quaternion.LookRotation(ghostDirection);    // Odredi ugao rotacije lika na osnovu izračunatog pravca

        rb.MovePosition(rb.position + targetDirection.normalized * moveSpeed * Time.deltaTime);    // Pomjeri lika u željenom pravcu, u skladu sa zadatom brzinom
        rb.MoveRotation(forwardRotation);    // Postavi željeni ugao za koji je lik okrenut
    }
}
