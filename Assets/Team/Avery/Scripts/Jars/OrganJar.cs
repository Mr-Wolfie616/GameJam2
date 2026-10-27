using UnityEngine;

public class OrganJar : MonoBehaviour
{
    public OrganType wantedOrgan;
    public bool full = false;
    public bool correctFill = false;

    public void FillJar(OrganType type)
    {
        if (full)
        {
            Debug.Log("Jar is already full. Cannot add another organ.");
            return;
        }

        if (type == wantedOrgan)
        {
            full = true;
            correctFill = true;
            Debug.Log("Jar filled with the correct organ: " + type.ToString());
        }
        else
        {
            correctFill = false;
            Debug.Log("Incorrect organ. Jar remains empty.");
        }
    }
}
