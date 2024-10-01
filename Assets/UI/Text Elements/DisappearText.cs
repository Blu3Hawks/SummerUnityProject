using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisappearText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timer;

    private void Start()
    {
        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(timer);
        text.enabled = false;
    }
}
