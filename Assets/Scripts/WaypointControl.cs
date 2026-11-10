using UnityEngine;

public class WaypointControl : MonoBehaviour
{
    public float moveSpeed = 1.0f;    // Parametar kojim se definiše brzina kretanja
    public float turnSpeed = 5.0f;    // Parametar kojim se definiše brzina promjene pravca

    public Transform[] waypoints;    // Tačke koje određuju putanju kretanja

    private Rigidbody rb;    // Referenca na Rigidbody komponentu
    private int currentWaypoint = 0;    // Indeks tačke ka kojoj se trenutno kreće

    void Start()
    {
        rb = GetComponent<Rigidbody>();    // Preuzimanje Rigidbody komponente
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform waypoint = waypoints[currentWaypoint];    // Pozicija ciljne tačke ka kojoj se trenutno kreće

        Vector3 targetDirection = waypoint.position - rb.position;    //  Vektor od trenutne pozicije lika do ciljne tačke
        rb.MovePosition(rb.position + targetDirection.normalized * moveSpeed * Time.deltaTime);    // Pomjeri lika u željenom pravcu, u skladu sa zadatom brzinom

        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0f);    // Odredi željeni ugao okretanja lika, u zavisnosti od proteklog vremena
        Quaternion rotation = Quaternion.LookRotation(direction);    // Odredi ugao rotacije lika na osnovu izračunatog pravca
        rb.MoveRotation(rotation);    // Postavi željeni ugao za koji je lik okrenut

        if (targetDirection.magnitude < 1f)    // Da li je lik došao blizu ciljne tačke?
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;    // Odredi indeks sljedeće tačke
        }
    }
}
