using UnityEngine;

public class OrganJar : MonoBehaviour
{
    public OrganType wantedOrgan;
    public bool full = false;
    public bool correctFill = false;
    private Organ currentOrgan = null;
    public void SetWantedOrgan(OrganType organType)
    {
        wantedOrgan = organType;
        Debug.Log("Jar is now set to accept: " + wantedOrgan.ToString());
    }

    public void FillJar(Organ organ)
    {
        if (full)
        {
            Debug.Log("Jar is already full. Cannot add another organ.");
            return;
        }

        full = true;

        currentOrgan = organ;
        OrganType type = currentOrgan.type;
        currentOrgan.inJar = true;
        currentOrgan.gameObject.SetActive(false); // Hide the organ in the jar

        if (type == wantedOrgan)
        {
            correctFill = true;
            Debug.Log("Jar filled with the correct organ: " + type.ToString());
        }
        else
        {
            correctFill = false;
            Debug.Log("Incorrect organ. Jar remains empty.");
        }
    }

    public void TakeOutJar()
    {
        if (!full)
        {
            Debug.Log("Jar is empty. Cannot take out an organ.");
            return;
        }
        full = false;
        correctFill = false;

        currentOrgan.gameObject.SetActive(true); // Show the organ again

        Debug.Log("Organ taken out of the jar.");
    }
}
