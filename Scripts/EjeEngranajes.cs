using UnityEngine;

public class EjeEngranajes : MonoBehaviour
{
    
    public bool ocupado = false;

    
    public GameObject objetoActual;

    // Método para anclar un objeto a este eje
    public bool AnclarObjeto(GameObject objeto)
    {
        // Si ya está ocupado, no se puede anclar otro
        if (ocupado)
            return false;

        // Registrar el nuevo objeto
        objetoActual = objeto;
        ocupado = true;

        return true;
    }

    // Método para liberar el eje (si el objeto se retira)
    public void LiberarEje()
    {
        objetoActual = null;
        ocupado = false;
    }
}
