using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrganOrderRecipe : MonoBehaviour
{
    // holds the info for one organ in an order
    private class OrderItem
    {
        public int organID;
        public string organName;
        public bool completed;
    }

    [Header("Order Settings")]
    public int minOrderSize = 1;
    public int maxOrderSize = 3;
    public bool generateOrderOnStart = true;

    [Header("UI")]
    public TMP_Text orderText;
    public TMP_Text warningText;

    private List<OrderItem> organList = new List<OrderItem>();    // every type of organ in the scene
    private List<OrderItem> currentOrder = new List<OrderItem>();  // the organs in the current order

    void Start()
    {
        BuildOrganList();

        if (generateOrderOnStart)
        {
            TryGenerateOrder();
        }
    }

    // finds all the organs in the scene and saves one of each id
    public void BuildOrganList()
    {
        organList.Clear();

        Organ[] organsInScene = FindObjectsByType<Organ>(FindObjectsSortMode.None);

        foreach (Organ organ in organsInScene)
        {
            if (FindItem(organList, organ.organID) == null)
            {
                OrderItem newItem = new OrderItem();
                newItem.organID = organ.organID;
                newItem.organName = organ.organName;
                organList.Add(newItem);
            }
        }

        if (organList.Count == 0)
        {
            Debug.LogError("No organs found in the scene");
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
        // copy the organ list so organs can be removed once picked (stops the same organ coming up twice)
        List<OrderItem> organsLeft = new List<OrderItem>(organList);

        int orderSize = Random.Range(minOrderSize, maxOrderSize + 1);
        orderSize = Mathf.Min(orderSize, organsLeft.Count);

        for (int i = 0; i < orderSize; i++)
        {
            int randomIndex = Random.Range(0, organsLeft.Count);

            OrderItem item = new OrderItem();
            item.organID = organsLeft[randomIndex].organID;
            item.organName = organsLeft[randomIndex].organName;
            item.completed = false;

            currentOrder.Add(item);
            organsLeft.RemoveAt(randomIndex);
        }
    }

    // the jar calls this when it gets filled
    public void CompleteOrganInOrder(int organID)
    {
        OrderItem item = FindItem(currentOrder, organID);

        if (item == null || item.completed)
        {
            return;
        }

        item.completed = true;
        UpdateOrderText();

        if (IsOrderComplete())
        {
            currentOrder.Clear();
            orderText.SetText("Thank you for your order");
        }
    }

    // other scripts can use this to check if an organ is still needed
    public bool IsOrganNeeded(int organID)
    {
        OrderItem item = FindItem(currentOrder, organID);
        return item != null && !item.completed;
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

    // looks through a list for an organ with this id, returns null if it isnt there
    private OrderItem FindItem(List<OrderItem> list, int organID)
    {
        foreach (OrderItem item in list)
        {
            if (item.organID == organID)
            {
                return item;
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
                text += "<s>" + item.organName + "</s>\n"; // crosses out the organ
            }
            else
            {
                text += item.organName + "\n";
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
