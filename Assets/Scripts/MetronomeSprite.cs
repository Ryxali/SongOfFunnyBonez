using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeSprite : MonoBehaviour
{

    [SerializeField]
    private GameObject toShow;
    [SerializeField]
    private GameObject specificShow;
    [SerializeField]
    private Beat beat;
    // Start is called before the first frame update
    void Start()
    {
        Metronome.onTick += Metronome_onTick;
    }

    private void Metronome_onTick(Beat obj)
    {
        toShow.SetActive(!toShow.activeSelf);
        specificShow.SetActive(obj == beat);
    }

    private IEnumerator ShowTemp(GameObject o)
    {
        o.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        o.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
