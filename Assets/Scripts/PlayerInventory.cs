using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;


    public void Additem(ItemType itemType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Additem(itemType);
        }

    }
    public void Additem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���簳�� : {crystalCount}");
                break;

            case ItemType.Plant:
                plantCount++;
                Debug.Log($"�Ĺ� ȹ�� ! ���簳�� : {plantCount}");
                break;

            case ItemType.Bush:
                bushCount++;
                Debug.Log($"��Ǯ ȹ�� ! ���簳�� : {bushCount}");
                break;

            case ItemType.Tree:
                treeCount++;
                Debug.Log($"���� ȹ�� ! ���簳�� : {treeCount}");
                break;


        }
    }

    public bool Removeitem(ItemType itemType , int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ��� ! ���簳�� : {crystalCount}");
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"�Ĺ� {amount} ��� ! ���簳�� : {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;
                    Debug.Log($"��Ǯ {amount} ��� ! ���簳�� : {bushCount}");
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;
                    Debug.Log($"���� {amount} ��� ! ���簳�� : {treeCount}");
                    return true;
                }
                break;

        }
        return false;
    }

    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
                case ItemType.Plant:
                return plantCount;
                case ItemType.Bush:
                return bushCount;
                case ItemType.Tree:
                return treeCount;   
                default : return 0;
        }
    }

    
    
    
    
    
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("======�κ��丮======");
        Debug.Log($"ũ����Ż: {crystalCount}��");
        Debug.Log($"�Ĺ�: {plantCount}��");
        Debug.Log($"��Ǯ: {bushCount}��");
        Debug.Log($"����: {treeCount}��"); 
        Debug.Log("====================");
    }
}
