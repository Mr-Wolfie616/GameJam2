using UnityEngine;

public class Organ : MonoBehaviour
{
    public OrganType type;
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
