using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Singleton class that sets and keeps references of ingame UI elements. Should be attached as a component on the Canvas gameObject.
/// </summary>
public class UIElementsTracker : MonoBehaviour {
    static UIElementsTracker instance;

    GameObject menu, hud;

    bool hasInitialized = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (!hasInitialized)
            Initialize();
    }

    void Initialize()
    {
        if (!hasInitialized)
        {
            menu = transform.Find("Menu").gameObject;
            hud = transform.Find("HUD").gameObject;
            hasInitialized = true;
        }
    }

    /// <summary>
    /// Property to get the current UIElementsTracker instance.
    /// </summary>
    public static UIElementsTracker Instance { get { return instance; } }

    public GameObject Hud
    {
        get
        {
            if (!hasInitialized)
                Initialize();

            return hud;
        }
    }

    public GameObject Menu
    {
        get
        {
            if (!hasInitialized)
                Initialize();

            return menu;
        }
    }
}
