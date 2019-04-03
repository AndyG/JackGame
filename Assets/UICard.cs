using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICard : MonoBehaviour
{
 
    private Image image;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
    }
}
