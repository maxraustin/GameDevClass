using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controller for weapons UI displays.
/// </summary>
public class WeaponDisplayController : MonoBehaviour
{
    Text primaryWeaponDisplay, secondaryWeaponDisplay, tertiaryWeaponDisplay;
    bool hasInitialized;

    void Start()
    {
        if (!hasInitialized)
            Initialiaze();
    }

    void Initialiaze()
    {
        if (!hasInitialized)
        {
            primaryWeaponDisplay = transform.Find("PrimaryWeaponDisplay").GetComponent<Text>();
            secondaryWeaponDisplay = transform.Find("SecondaryWeaponDisplay").GetComponent<Text>();
            tertiaryWeaponDisplay = transform.Find("TertiaryWeaponDisplay").GetComponent<Text>();

            hasInitialized = true;
        }
    }

    public void SetPrimaryWeaponCount(int count)
    {
        if (!hasInitialized)
            Initialiaze();

        primaryWeaponDisplay.text = "Weapon1: " + count;
    }

    public void SetPrimaryWeaponCount(bool isInfinite)
    {
        if (!hasInitialized)
            Initialiaze();

        if (isInfinite)
            primaryWeaponDisplay.text = "Weapon1: Inf";
    }

    public void SetPrimaryWeaponEnabled(bool isEnabled)
    {
        if (!hasInitialized)
            Initialiaze();

        primaryWeaponDisplay.gameObject.SetActive(isEnabled);
    }

    public void SetSecondaryWeaponCount(int count)
    {
        if (!hasInitialized)
            Initialiaze();

        secondaryWeaponDisplay.text = "Weapon2: " + count;
    }

    public void SetSecondaryWeaponCount(bool isInfinite)
    {
        if (!hasInitialized)
            Initialiaze();

        if (isInfinite)
            secondaryWeaponDisplay.text = "Weapon2: Inf";
    }

    public void SetSecondaryWeaponEnabled(bool isEnabled)
    {
        if (!hasInitialized)
            Initialiaze();

        secondaryWeaponDisplay.gameObject.SetActive(isEnabled);
    }

    public void SetTertiaryWeaponCount(int count)
    {
        if (!hasInitialized)
            Initialiaze();

        tertiaryWeaponDisplay.text = "Weapon3: " + count;
    }

    public void SetTertiaryWeaponCount(bool isInfinite)
    {
        if (!hasInitialized)
            Initialiaze();

        if (isInfinite)
            tertiaryWeaponDisplay.text = "Weapon3: Inf";
    }

    public void SetTertiaryWeaponEnabled(bool isEnabled)
    {
        if (!hasInitialized)
            Initialiaze();

        tertiaryWeaponDisplay.gameObject.SetActive(isEnabled);
    }
}
