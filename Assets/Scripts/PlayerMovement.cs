using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 1.0f;    // Parametar kojim se definiše brzina kretanja
    public float turnSpeed = 10f;    // Parametar kojim se definiše brzina promjene pravca

    Vector2 movingDirection;    // Smjer koji daju ulazne kontrole
    Rigidbody rb;    // Referenca na Rigidbody komponentu
    Animator anim;    // Referenca na Animator komponentu

    void Start()
    {
        rb = GetComponent<Rigidbody>();    // Preuzimanje RigidBody komponente
        anim = GetComponent<Animator>();    // Preuzimanje Animator komponente
    }

    void FixedUpdate()
    {
        // Pomjeranje lika
        Vector3 movement = new Vector3(movingDirection.x, 0f, movingDirection.y);    // Kreiranje 3D vektora pravca, na osnovu očitanih kontrola u 2D
        rb.MovePosition(rb.position + movement * walkSpeed * Time.deltaTime);    // Pomjeri lika u željenom pravcu, za izračunatu dužinu

        // Rotacija lika
        Vector3 direction = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);    // Odredi željeni pravac prema kom treba okrenuti lika, u zavisnosti od proteklog vremena,
        Quaternion rotation = Quaternion.LookRotation(direction);    // Odredi ugao rotacije na osnovu izračunatog pravca okretanja
        rb.MoveRotation (rotation);    // Postavi željeni ugao za koji je lik okrenut
    }

    void OnMove(InputValue value)
    {
        movingDirection = value.Get<Vector2>();    // Očitavanje ulaznih kontrola kada dođe do promjene
        bool moving = !Mathf.Approximately(movingDirection.magnitude, 0f);    // Lik se kreće ako intenzitet vektora kretanja nije približno jednak nul
        anim.SetBool("IsWalking", moving);    // Podesi parametar animacije, tako da odgovara tome da li se lik kreće ili stoji
    }
}
