using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;

    public float walkSpeed = 1.0f;   // Parametar kojim se definiše brzina kretanja
    public float turnSpeed = 20f;    // Parametar kojim se definiše brzina promjene pravca

    Rigidbody rb;    // Referenca na Rigidbody komponentu
    Animator anim;    // Referenca na Animator komponentu
    Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();    // Preuzimanje RigidBody komponente
        anim = GetComponent<Animator>();     // Preuzimanje Animator komponente
        moveAction.Enable();    // Uključena InputAvtion komponenta
    }

    // Očitavanje ulaznih uređaja i aktiviranje anmimacija u svakom frejmu animacije
    void Update()
    {
        input = moveAction.ReadValue<Vector2>();    // Očitavanje ulaza

        bool moving = !Mathf.Approximately(input.magnitude, 0f);    // Lik se kreće ako intenzitet vektora kretanja nije približno jednak nuli
        anim.SetBool("IsWalking", moving);    // Podesi parametar animacije, tako da odgovara tome da li se lik kreće ili stoji
    }

    // Rad sa fizikom u fiksnoj petlji
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0f, input.y);  // Kreiranje vektora pravca, na osnovu očitanih kontrola
        movement.Normalize ();    // Normalizacija vektora, tako da ima intenzitet 1, tako da vrijednosti kontrola ne utiču na brzinu kretanja lika

        Vector3 direction = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);    // Odredi željeni pravac u kom treba okrenuti lika, u zavisnosti od proteklog vremena, ali ne brže od zadatog parametra
        Quaternion rotation = Quaternion.LookRotation(direction);    // Odredi ugao rotacije na osnovu izračunatog pravca

        rb.MovePosition(rb.position + movement * walkSpeed * Time.deltaTime);      // Pomjeri lika u željenom pravcu, za dužinu definisanu njegovim root motionom
        rb.MoveRotation(rotation);    // Postavi željeni ugao za koji je lik okrenut
    }
}
