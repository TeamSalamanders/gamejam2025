using UnityEngine;

public class EngranajesController : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }

    [Header("Configuración de rotación")]
    public RotationAxis ejeRotacion = RotationAxis.Y;
    public float velocidadRotacion = 100f;

    [Header("Configuración de colisión")]
    public string tagDeCilindro = "Engranaje";
    public string tagDeEje = "EjeEng";

    private bool enContactoConCilindro = false;

    void OnCollisionEnter(Collision collision)
    {
        // Si toca otro cilindro, marcamos el cilindro como en contacto
        if (collision.gameObject.CompareTag(tagDeCilindro))
        {
            enContactoConCilindro = true;
        }

        // Si toca un objeto con tag "Eje", posicionamos el cilindro en el centro de ese objeto
        if (collision.gameObject.CompareTag(tagDeEje))
        {
            Vector3 centroDelEje = collision.collider.bounds.center;

            // Posicionamos el cilindro en el centro (sin modificar la altura Y)
            transform.position = new Vector3(centroDelEje.x, transform.position.y, centroDelEje.z);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Cuando el cilindro sale de contacto con otro cilindro
        if (collision.gameObject.CompareTag(tagDeCilindro))
        {
            enContactoConCilindro = false;
        }
    }

    void Update()
    {
        // Si estamos en contacto con otro cilindro, hacemos la rotación
        if (enContactoConCilindro)
        {
            Vector3 eje = Vector3.zero;

            // Dependiendo del eje de rotación seleccionado, asignamos el eje de rotación adecuado
            switch (ejeRotacion)
            {
                case RotationAxis.X:
                    eje = Vector3.right; // Rotación en el eje X
                    break;
                case RotationAxis.Y:
                    eje = Vector3.up; // Rotación en el eje Y
                    break;
                case RotationAxis.Z:
                    eje = Vector3.forward; // Rotación en el eje Z
                    break;
            }

            // Realizamos la rotación en el eje elegido
            transform.Rotate(eje, velocidadRotacion * Time.deltaTime);
        }
    }
}
