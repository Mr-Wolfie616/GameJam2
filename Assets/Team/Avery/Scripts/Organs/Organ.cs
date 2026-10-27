using UnityEngine;

public class Organ : MonoBehaviour
{
    public OrganType type;

    public bool insideBody;
    public bool inJar;
    public bool inHand;
}

public enum OrganType {
    Heart,
    Lung,
    Kidney,
    Liver,
    Stomach,
    Intestines,
    Pancreas
}
