using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;
    [SerializeField]
    private Image _coinImage;
    private void Start()
    {
        _ammoText.text = "Ammo: 50";
    }
    public void UpdateAmmo(int count)
    {

        _ammoText.text = "Ammo: " + count;
    }
   public void CollectedCoin()
    {
        _coinImage.gameObject.SetActive(true);
    }
    public void RemoveCoin()
    {
        _coinImage.gameObject.SetActive(false);
    }
}
