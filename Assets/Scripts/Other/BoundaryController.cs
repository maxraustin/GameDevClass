using UnityEngine;
using System.Collections;

public class BoundaryController : MonoBehaviour {

    public delegate void BoundaryPlayerEvent(GameObject boundary);
    public static event BoundaryPlayerEvent OnPlayerEnterBoundary;
    public static event BoundaryPlayerEvent OnPlayerExitBoundary;

    public delegate void BoundaryUnitEvent(GameObject boundary, GameObject unit);
    public static event BoundaryUnitEvent OnUnitEnterBoundary;
    public static event BoundaryUnitEvent OnUnitExitBoundray;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Unit")) {
            if (other.GetComponent<UnitInfo>() != null && other.GetComponent<UnitInfo>().IsPlayerShip)
            {
                try
                {
                    if (OnPlayerEnterBoundary != null)
                        OnPlayerEnterBoundary(gameObject);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (OnUnitEnterBoundary != null)
                        OnUnitEnterBoundary(gameObject, other.gameObject);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Unit"))
        {
            if (other.GetComponent<UnitInfo>() != null && other.GetComponent<UnitInfo>().IsPlayerShip)
            {
                try
                {
                    if (OnPlayerExitBoundary != null)
                        OnPlayerExitBoundary(gameObject);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (OnUnitExitBoundray != null)
                        OnUnitExitBoundray(gameObject, other.gameObject);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
    }
}
