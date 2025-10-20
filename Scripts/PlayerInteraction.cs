using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f; // Distancia máxima de interacción
    public LayerMask interactLayer;     // Capa de objetos interactuables
    public Transform holdPosition;      // Lugar donde se sostiene el objeto

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (heldObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickup();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropObject();
            }
            HoldObject();

            if (Input.GetKeyDown(KeyCode.F))
            {
                TryPlaceOnEje();
            }
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            PickupObject pickup = hit.collider.GetComponent<PickupObject>();
            if (pickup != null)
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                if (heldRb != null)
                {
                    heldRb.useGravity = false;
                    heldRb.freezeRotation = true;
                }

                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void HoldObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = holdPosition.position;
        }
    }

    void DropObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);

        if (heldRb != null)
        {
            heldRb.useGravity = true;
            heldRb.freezeRotation = false;
        }

        heldObject = null;
        heldRb = null;
    }

    void TryPlaceOnEje()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("EjeEng"))
            {
                heldObject.transform.SetParent(null);
                heldObject.transform.position = hit.collider.transform.position;
                heldObject.transform.rotation = hit.collider.transform.rotation;

                // Reactiva la física si quieres que quede fijo
                /*if (heldRb != null)
                {
                    heldRb.useGravity = true;
                    heldRb.freezeRotation = false;
                }*/

                heldObject = null;
                heldRb = null;
            }
        }
    }

}
