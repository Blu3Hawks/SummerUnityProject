using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterName : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField;
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponent<TMP_InputField>();
        }
    }

    // Trigger the keyboard explicitly when the input field is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        OpenKeyboard();
    }

    private void OpenKeyboard()
    {
        // Only open the keyboard if running on a supported mobile platform
        if (TouchScreenKeyboard.isSupported && Application.isMobilePlatform)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            Debug.Log("Keyboard opened for mobile input.");
        }
        else
        {
            Debug.LogWarning("TouchScreenKeyboard is not supported or not on a mobile platform.");
        }
    }

    private void OnDisable()
    {
        // If keyboard was open, close it on disable
        if (keyboard != null && keyboard.active)
        {
            keyboard.active = false;
        }
    }
}
