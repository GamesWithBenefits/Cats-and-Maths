using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public void OnClick()
    {
        GetComponent<Button>().interactable = false;
    }
}
