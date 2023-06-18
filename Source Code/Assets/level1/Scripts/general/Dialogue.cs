using System; //for Tuple<>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Helpful resources: Tactical Brackeys' video:
https://www.youtube.com/watch?v=_nRzoTzeyxU
Idea for user defined tuples:
https://forum.unity.com/threads/please-let-a-valuetuple-can-be-serialized.552703/
*/

[System.Serializable] //in order for the struct's array to show up in the inspector
public struct nameToSentenceTuple //as the normal Tuple<> doesn't show in inspector
{
    public Characters name;
    public float typeDelay;
    [TextArea(3, 10)] //for [3,10] lines of TextArea to appear in the inspector
    public string sentence;
    private nameToSentenceTuple((Characters left, float mid, string right) tup)
    {
        name = tup.left;
        typeDelay = tup.mid;
        sentence = tup.right;
    }

    public static implicit operator nameToSentenceTuple((Characters left, float mid, string right) tup) //converts tuple of three values to uTuple datatype implicitly upon assignment
    {
        return new nameToSentenceTuple(tup);
    }
}

//Class that holds all information about a single dialogue, and will be used in the DialogueTrigger script
[System.Serializable] //in order for the class's info to show up in the inspector
public class Dialogue //won't be derived from MonoBehaviour as we don't want it sitting on a script (it is just a class)
{
    public nameToSentenceTuple[] sentences;    
}
