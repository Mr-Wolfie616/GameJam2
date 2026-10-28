using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GrabOrgan : MonoBehaviour, IInteractable
{
    private bool overJar = false;
    private Organ organ;
    private OrganJar currentJar;
    private BloodSplatter splatterScript;
    private bool holding = false;

    public bool isGrabbable = true;
    public Transform grabPoint;

    public LayerMask jarLayer;
    private float maxDistance = 100f;

    public List<AudioClip> audioSources;

    private AudioSource audioSource;

    private Collider organCollider;
    private Vector3 grabOffset;
    private void Awake()
    {
        organCollider = GetComponent<Collider>();
        organ = GetComponent<Organ>();
        splatterScript = FindFirstObjectByType<BloodSplatter>();

        audioSources = new List<AudioClip>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!holding || grabPoint == null) return;

        if (organCollider != null)
        {
            transform.position = grabPoint.position + grabOffset;
        }
        else
        {
            transform.position = grabPoint.position;
        }
    }

    public void OnClick()
    {
        //Debug.Log("Clicked on organ: " + organ.type.ToString());
        splatterScript.IncreaseSplatter();
        if (organ.inHand) { return; }
        if (!isGrabbable) { return; }
        organ.inHand = true;
        holding = true;

        GrabHandler.Instance.AttachObject(gameObject, grabPoint);
        grabOffset = transform.position - organCollider.bounds.center;
    }

    public void OnHold() 
    {
        
    }

    public void OnRelease()
    {
        if (!organ.inHand) { return; }
        
        organ.inHand = false;
        holding = false;

        GrabHandler.Instance.LetGo(gameObject, grabPoint);

        currentJar = CheckJar();

        if (currentJar != null)
        {
            overJar = true;
            currentJar.FillJar(organ);
            {
                Debug.Log(Equals(currentJar.wantedOrgan, organ.type));

                audioSource.PlayOneShot(audioSources[Random.Range(0, audioSources.Count)]);
            }
        }
        else
        {
            overJar = false;
        }

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    public void ToggleGrabbable()
    {
        isGrabbable = !isGrabbable;
    }

    public OrganJar CheckJar()
    {
        Ray ray = Camera.main.ScreenPointToRay(CustomCursor.instance.GetCursorScreenPos());
        OrganJar jar = null;
        if (Physics.Raycast(ray, out RaycastHit hit3D, maxDistance, jarLayer))
        {
            jar = hit3D.collider.gameObject.GetComponent<OrganJar>();
        }
        return jar;
    }
}
