using System;
using UnityEngine;

public class JarHandler : MonoBehaviour
{
    public OrganJar[] jars;

    private void OnEnable()
    {
        OrganJar.UpdateJar += CheckIfThePlayerDidIt;
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
        Debug.Log("Checking if the player filled all jars correctly...");

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
