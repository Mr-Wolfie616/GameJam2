using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrganOrderRecipe : MonoBehaviour
{
    // holds the info for one organ in an order
    private class OrderItem
    {
        public OrganType organType;
        public OrganJar jar;       // the jar that holds this organ
        public bool completed;
    }

    [Header("Order Settings")]
    public int minOrderSize = 1;
    public int maxOrderSize = 3;
    public bool generateOrderOnStart = true;

    [Header("UI")]
    public TMP_Text orderText;
    public TMP_Text warningText;

    private OrganJar[] jars;                                       // every jar in the scene
    private List<OrderItem> currentOrder = new List<OrderItem>();  // the organs in the current order

    void Start()
    {
        jars = FindObjectsByType<OrganJar>(FindObjectsSortMode.None);

        if (jars.Length == 0)
        {
            Debug.LogError("No organ jars found in the scene");
        }

        if (generateOrderOnStart)
        {
            TryGenerateOrder();
        }
    }

    void Update()
    {
        if (currentOrder.Count == 0)
        {
            return;
        }

        bool somethingChanged = false;

        // check each jar in the order to see if it has the right organ in it
        foreach (OrderItem item in currentOrder)
        {
            bool jarDone = item.jar != null && item.jar.full && item.jar.correctFill;

            if (jarDone != item.completed)
            {
                item.completed = jarDone; // also un-crosses it if the organ gets taken back out
                somethingChanged = true;
            }
        }

        if (!somethingChanged)
        {
            return;
        }

        if (IsOrderComplete())
        {
            currentOrder.Clear();
            orderText.SetText("Thank you for your order");
        }
        else
        {
            UpdateOrderText();
        }
    }

    // call this from a button or game manager to get a new order
    public void TryGenerateOrder()
    {
        if (currentOrder.Count == 0)
        {
            GenerateOrder();
            UpdateOrderText();
            SetWarning("");
        }
        else
        {
            SetWarning("Complete the current order before generating a new one.");
        }
    }

    private void GenerateOrder()
    {
        // make a list of every organ type that has a jar, organs get removed once picked so none come up twice
        List<OrganType> organsLeft = new List<OrganType>();

        foreach (OrganType type in System.Enum.GetValues(typeof(OrganType)))
        {
            if (FindJar(type) != null)
            {
                organsLeft.Add(type);
            }
        }

        int orderSize = Random.Range(minOrderSize, maxOrderSize + 1);
        orderSize = Mathf.Min(orderSize, organsLeft.Count);

        for (int i = 0; i < orderSize; i++)
        {
            int randomIndex = Random.Range(0, organsLeft.Count);

            OrderItem item = new OrderItem();
            item.organType = organsLeft[randomIndex];
            item.jar = FindJar(item.organType);
            item.completed = false;

            currentOrder.Add(item);
            organsLeft.RemoveAt(randomIndex);
        }
    }

    // other scripts can use this to check if an organ is still needed
    public bool IsOrganNeeded(OrganType organType)
    {
        foreach (OrderItem item in currentOrder)
        {
            if (item.organType == organType && !item.completed)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsOrderComplete()
    {
        foreach (OrderItem item in currentOrder)
        {
            if (!item.completed)
            {
                return false;
            }
        }
        return true;
    }

    // finds the jar that wants this organ, returns null if there isnt one
    private OrganJar FindJar(OrganType organType)
    {
        foreach (OrganJar jar in jars)
        {
            if (jar.wantedOrgan == organType)
            {
                return jar;
            }
        }
        return null;
    }

    private void UpdateOrderText()
    {
        string text = "Order:\n";

        foreach (OrderItem item in currentOrder)
        {
            if (item.completed)
            {
                text += "<s>" + item.organType + "</s>\n"; // crosses out the organ
            }
            else
            {
                text += item.organType + "\n";
            }
        }

        orderText.SetText(text);
    }

    private void SetWarning(string message)
    {
        if (warningText != null)
        {
            warningText.SetText(message);
        }
    }
}
