using UnityEngine;

public class EngranajesController : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }
    public enum RotationDirection { Horario, Antihorario }

    [Header("Configuración de rotación")]
    public RotationAxis ejeRotacion = RotationAxis.Y;
    public RotationDirection direccionRotacion = RotationDirection.Horario;
    public float velocidadRotacion = 100f;

    [Header("Configuración de colisión")]
    public string tagDeCilindro = "Engranaje";

    private bool enContactoConCilindro = false;

    void OnCollisionEnter(Collision collision)
    {
        // Si toca otro cilindro, marcamos el cilindro como en contacto
        if (collision.gameObject.CompareTag(tagDeCilindro))
        {
            enContactoConCilindro = true;
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

            // Dependiendo del eje de rotación seleccionado
            switch (ejeRotacion)
            {
                case RotationAxis.X:
                    eje = Vector3.right;
                    break;
                case RotationAxis.Y:
                    eje = Vector3.up;
                    break;
                case RotationAxis.Z:
                    eje = Vector3.forward;
                    break;
            }

            // Cambiar la dirección de rotación
            float sentido = (direccionRotacion == RotationDirection.Horario) ? 1f : -1f;

            // Rotamos
            transform.Rotate(eje, velocidadRotacion * sentido * Time.deltaTime);
        }
    }
}
