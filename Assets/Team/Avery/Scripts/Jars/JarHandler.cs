using UnityEngine;
using UnityEngine.Events;

public class JarHandler : MonoBehaviour
{
    public OrganJar[] jars;
    public static UnityEvent updateJar;

    private void OnEnable()
    {
        updateJar.AddListener(CheckIfThePlayerDidIt);
    }
    private bool CheckAllJarsFilled()
    {
        foreach (OrganJar jar in jars)
        {
            if (!jar.full)
            {
                return false;
            }
        }
        return true;
    }

    public void CheckIfThePlayerDidIt()
    {
        if (!CheckAllJarsFilled()) { return; }

        foreach (OrganJar jar in jars)
        {
            if (!jar.correctFill)
            {
                Debug.Log("Player did not fill all jars correctly.");
                LoseConCheck.PlayerFails.Invoke();
                return;
            }
            else
            {
                Debug.Log("Player filled all jars correctly.");
                LoseConCheck.PlayerSucceeds.Invoke();
            }
        }
    }
}
