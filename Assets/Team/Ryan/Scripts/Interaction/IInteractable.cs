// put this on anything the mouse should be able to interact with (organs, jars, buttons in the world etc)
// the object also needs a collider so the MouseInteractor raycast can hit it
public interface IInteractable
{
    // called once on the frame the mouse is clicked on this object
    void OnClick();

    
    void OnHold();

    
    void OnRelease();
}
