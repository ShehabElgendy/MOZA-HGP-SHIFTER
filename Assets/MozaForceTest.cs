using UnityEngine;
using mozaAPI;

public class MozaForceTest : MonoBehaviour
{
    public GEAR gearData;

    void Start()
    {
        Debug.Log($"Initial Gear: {gearData}");
    }

    void Update()
    {
        Debug.Log($"Current Gear: {gearData}");
    }
}