using UnityEngine;

public class Puntero : MonoBehaviour
{
    public float size = 10f;
    public Texture2D crosshairTexture;

    void OnGUI()
    {
        if (crosshairTexture == null) return;

        float x = (Screen.width - size) / 2;
        float y = (Screen.height - size) / 2;
        GUI.DrawTexture(new Rect(x, y, size, size), crosshairTexture);
    }
}
