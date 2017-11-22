using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    [SerializeField]
    Player PlayerSc = null;

    public Image FlashImg = null;

    public Text FlashCoolText = null;

    float fFlashCool = 0.0f;

    private void Start()
    {
        fFlashCool = 5f;
    }

    private void Update()
    {
        FlashCoolText.text = PlayerSc.nFlashCool.ToString();
        if (FlashImg.fillAmount < 1f)
        {
            FlashImg.fillAmount += (1f / fFlashCool) / 60f;
        }
    }
}
