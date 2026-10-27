using UnityEngine;

// test only - put this on any object with a collider to check the mouse can interact with it
// it logs each step and turns the object a different colour while held
public class TestInteractable : MonoBehaviour, IInteractable
{
    public Color heldColour = Color.green;

    private Renderer objectRenderer;   // works for 3D meshes and 2D sprites
    private Color startColour;
    private int holdFrames;

    void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();

        if (objectRenderer != null)
        {
            startColour = objectRenderer.material.color;
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked " + name);
        holdFrames = 0;
        SetColour(heldColour);
    }

    public void OnHold()
    {
        holdFrames++;
    }

    public void OnRelease()
    {
        Debug.Log("Let go of " + name + " after holding for " + holdFrames + " frames");
        SetColour(startColour);
    }

    private void SetColour(Color colour)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = colour;
        }
    }
}
