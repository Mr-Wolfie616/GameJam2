using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic ;

public class JarHandler : MonoBehaviour
{
    public OrganJar[] jars;

    public static event Action OnSucceed;
    public static event Action OnFail;

    private List<OrganJar> needyJars = new();
    private void Awake()
    {
        CheckIfThePlayerDidIt();
    }
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

        needyJars.Clear();

        foreach(OrganJar organJar in jars)
        {
            if (organJar.wantedOrgan != OrganType.None)
            {
                needyJars.Add(organJar);
            }
        }

        int numberOfNeedyJars = needyJars.Count;
        int numberOfCorrectlyFilledJars = 0;

        foreach (OrganJar jar in needyJars)
        {
            if (jar.correctFill)
            {
                numberOfCorrectlyFilledJars++;
            }
        }

        Debug.Log($"Number of needy jars: {numberOfNeedyJars}, Number of correctly filled jars: {numberOfCorrectlyFilledJars}");

        if (numberOfNeedyJars == numberOfCorrectlyFilledJars)
        {
            OnSucceed?.Invoke();
        }
        else
        {
            OnFail?.Invoke();
        }
    }
}
