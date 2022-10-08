using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    private Transform trans;
    public bool isBig;
    public bool isSmall;
    private Vector3 fullScale;
    private Vector3 smallestScale;
    private Vector3 normalScale;

    [SerializeField] private AudioSource growSoundEffect;
    [SerializeField] private AudioSource shrinkSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        trans = this.GetComponent<Transform>();
        normalScale = trans.localScale;
        fullScale = new Vector3(2.6f, 2.6f, 1f);
        smallestScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        print("Scale: " + trans.localScale);
        if (isBig)
        {
            trans.localScale = fullScale;
        }
        else if (isSmall)
        {
            trans.localScale = smallestScale;
        }
        else
        {
            trans.localScale = normalScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EatMe")
        {
            growSoundEffect.Play();
            isBig = true;
            isSmall = false;
        }
        else if (other.tag == "DrinkMe")
        {
            shrinkSoundEffect.Play();
            isBig = false;
            isSmall = true;
        }
        if (other.tag == "EatMe" || other.tag == "DrinkMe")
        {
            Destroy(other.gameObject);
        }
        else
        {

        }
    }
    public void gettingHit()
    {
        isBig = false;
        isSmall = false;
    }
}
