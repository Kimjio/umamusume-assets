using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviourScript : MonoBehaviour
{
    public Action action = delegate() {
        Console.WriteLine("Hello World!");
    };
    
    // Start is called before the first frame update
    void Start()
    {
        action.Invoke();
        StartCoroutine(TestEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator TestEnumerator()
    {
        Console.WriteLine("1");
        yield return new WaitForEndOfFrame();
        Console.WriteLine("2");
        yield return new WaitForEndOfFrame();
        Console.WriteLine("3");
        yield break;
    }
}
