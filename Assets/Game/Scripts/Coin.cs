using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private AudioClip _pickupSfx;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player._hasCoin = true;
                AudioSource.PlayClipAtPoint(_pickupSfx, transform.position, 1f);
                Destroy(gameObject);
            }
        }
    }
}
