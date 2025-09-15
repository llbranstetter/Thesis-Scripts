using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class UnlockItem
    {
        public string itemName;               // e.g., "armor", "sword", "jetpack"
        public Button itemButton;             // The button to click/select
        public MonoBehaviour checkScript;     

        [HideInInspector]
        public bool isUnlocked = false;       // track unlock state
    }

    [Header("Unlockable Items")]
    public List<UnlockItem> unlockItems;      // List of all unlockable UI elements

    [Header("Unlock Button")]
    public Button unlockButton;               // The single shared unlock button

    private UnlockItem currentlySelectedItem;
    private UnlockComponents unlockComponents;

    // Colors
    private readonly Color defaultColor = Color.white;
    private readonly Color selectedColor = Color.blue;
    private readonly Color unlockedColor = Color.green;
    private readonly Color failureColor = Color.red;

    void Start()
    {
        // Replace the deprecated FindObjectOfType with FindFirstObjectByType
        unlockComponents = UnityEngine.Object.FindFirstObjectByType<UnlockComponents>();

        foreach (var item in unlockItems)
        {
            SetButtonColor(item.itemButton, defaultColor);
            item.itemButton.onClick.AddListener(() => OnItemButtonClicked(item));
        }

        unlockButton.onClick.AddListener(OnUnlockButtonClicked);
        // Disable unlock button until an item is selected
        unlockButton.interactable = false;
    }

    void OnItemButtonClicked(UnlockItem item)
    {
        // If item already unlocked, ignore clicks
        if (GetButtonColor(item.itemButton) == unlockedColor || item.isUnlocked)
        {
            Debug.Log($"{item.itemName} is already unlocked.");
            return;
        }

        // Deselect previous
        if (currentlySelectedItem != null && currentlySelectedItem != item)
        {
            SetButtonColor(currentlySelectedItem.itemButton, defaultColor);
        }

        currentlySelectedItem = item;
        SetButtonColor(item.itemButton, selectedColor);

        // Enable unlock button only if the selected item isn’t unlocked
        unlockButton.interactable = !item.isUnlocked;
    }

    void OnUnlockButtonClicked()
    {
        if (currentlySelectedItem == null)
        {
            Debug.Log("No item selected to unlock.");
            return;
        }

        var checkScript = currentlySelectedItem.checkScript as IUnlockCheck;

        if (checkScript == null)
        {
            Debug.LogError($"The script for {currentlySelectedItem.itemName} does not implement IUnlockCheck.");
            return;
        }

        StartCoroutine(RunCheckAndUnlock(checkScript, currentlySelectedItem));
    }

    IEnumerator RunCheckAndUnlock(IUnlockCheck checkScript, UnlockItem item)
    {
        bool result = false;

        yield return StartCoroutine(checkScript.PerformCheck((bool success) =>
        {
            result = success;
        }));

        if (result)
        {
            Debug.Log($"✅ Unlock passed for {item.itemName}.");
            item.isUnlocked = true;  // mark as unlocked
            SetButtonColor(item.itemButton, unlockedColor);
            RunUnlockFunction(item.itemName);

            // Disable unlock button since current item is now unlocked
            unlockButton.interactable = false;

        }
        else
        {
            Debug.Log($"❌ Unlock failed for {item.itemName}.");
            StartCoroutine(FlashFailureColor(item.itemButton));
        }
    }

    void RunUnlockFunction(string itemName)
    {
        switch (itemName.ToLower())
        {
            case "armor":
                unlockComponents.UnlockArmor(); break;
            case "sword":
                unlockComponents.UnlockSword(); break;
            case "jetpack":
                unlockComponents.UnlockJetPack(); break;
            case "helmet":
                unlockComponents.UnlockHelmet(); break;
            case "hood":
                unlockComponents.UnlockHood(); break;
            case "cyborg":
                unlockComponents.UnlockCyborg(); break;
            case "companion":
                unlockComponents.UnlockHelmet(); break; // Replace with your own unlock call
            default:
                Debug.LogWarning($"Unknown item name: {itemName}"); break;
        }
    }

    IEnumerator FlashFailureColor(Button button)
    {
        SetButtonColor(button, failureColor);
        yield return new WaitForSeconds(1f);

        // Restore to selected color if still selected, otherwise default
        if (currentlySelectedItem != null && currentlySelectedItem.itemButton == button)
        {
            SetButtonColor(button, selectedColor);
        }
        else
        {
            SetButtonColor(button, defaultColor);
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        if (button == unlockButton)
            return;
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        cb.disabledColor = color;
        button.colors = cb;
    }

    Color GetButtonColor(Button button)
    {
        return button.colors.normalColor;
    }
}

