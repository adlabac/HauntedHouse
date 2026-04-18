using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;

    public float walkSpeed = 1.0f;   // Parametar kojim se definiše brzina kretanja
    public float turnSpeed = 20f;    // Parametar kojim se definiše brzina promjene pravca

    Rigidbody rb;    // Referenca na Rigidbody komponentu

    void Start()
    {
        rb = GetComponent<Rigidbody> ();    // Preuzimanje RigidBody komponente
        moveAction.Enable();    // Uključena InputAvtion komponenta
    }

    void FixedUpdate()
    {
        var pos = moveAction.ReadValue<Vector2>();    // Očitavanje ulaza

        Vector3 movement = new Vector3(pos.x, 0f, pos.y);  // Kreiranje vektora pravca, na osnovu očitanih kontrola
        movement.Normalize ();    // Normalizacija vektora, tako da ima intenzitet 1, tako da vrijednosti kontrola ne utiču na brzinu kretanja lika

        Vector3 direction = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);    // Odredi željeni pravac u kom treba okrenuti lika, u zavisnosti od proteklog vremena, ali ne brže od zadatog parametra
        Quaternion rotation = Quaternion.LookRotation(direction);    // Odredi ugao rotacije na osnovu izračunatog pravca

        rb.MovePosition(rb.position + movement * walkSpeed * Time.deltaTime);      // Pomjeri lika u željenom pravcu, za dužinu definisanu njegovim root motionom
        rb.MoveRotation(rotation);    // Postavi željeni ugao za koji je lik okrenut
    }
}
