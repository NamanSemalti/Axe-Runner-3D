using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRandomze : MonoBehaviour
{
    public List<Transform> buttonpos = new List<Transform>();
    public List<Transform> buttons = new List<Transform>();
    public  List<int> selectednumbers = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <=2 ; i++)
        {
            int n = i;
            int indextnumber = Random.Range(0, buttonpos.Count);
            selectednumbers.Add(indextnumber);
            buttons[n].position = buttonpos[indextnumber].position;
            buttonpos.RemoveAt(indextnumber);
            n++;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
    }
}
