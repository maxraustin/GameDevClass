using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {

    public Texture2D texture;
    public float scale = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI() {
        // If not paused, removes crosshair on pause
        if (Time.timeScale != 0) {
            if (texture != null)
                GUI.DrawTexture(new Rect((Screen.width - texture.width * scale) / 2, (Screen.height - texture.height * scale) / 2, texture.width * scale, texture.height * scale), texture);
            else
                Debug.Log("No crosshair texture found");
        }
    }
}
