using System; //for Tuple<>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Helpful Resources:
https://forum.unity.com/threads/please-let-a-valuetuple-can-be-serialized.552703/
Idea for user defined tuples:
https://forum.unity.com/threads/please-let-a-valuetuple-can-be-serialized.552703/
*/
    public enum Characters
    {
        NoFace, //narrator for example
        Steve, //house steve
        YoungSteve, //young steve in first cutscene
        XHiker, //"Hiker" will appear as name
        XFormal, //no name will appear
        XRobber, //"Robber" will appear as name
        Connor,
        Gwen,
        Merideth,
        Father, //will have no face
        SergioNoFace, //his name will appear as "X"
        Sergio,
        ThePoliceOfficer, //name will be "The Officer"
        RandomPoliceOfficer, //name will be "Officer"
        Doctor, //mentioned in Level 1 Scene 2
        Bane,    //Final Boss Fight
        OldSteve,
        ConnorPoliceOfficer
    }
public class GameCharacters : MonoBehaviour
{
    public static GameCharacters instance;

    [System.Serializable] //have to do this to show in inspector, as uTuple doesn't inherit from MonoBehaviour
    public struct nameToFaceTuple //as the normal Tuple<> doesn't show in inspector
    {
        public Characters name;
        public Sprite face;
        private nameToFaceTuple((Characters left, Sprite right) pair)
        {
            name = pair.left;
            face = pair.right;
        }
    
        public static implicit operator nameToFaceTuple((Characters left, Sprite right) pair) //converts tuple of two values to uTuple datatype implicitly upon assignment
        {
            return new nameToFaceTuple(pair);
        }
    }

    public nameToFaceTuple[] faces; //can't be static, so that it appears in inspector, that's why we applied singleton principle on GameCharacters class
    public Dictionary<Characters, Sprite> nameToFace; //unity doesn't show Dictionaries in the inspector, that's why "faces" user-defined tuple was created. However, this dictionary is the one that will be used by different classes

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            nameToFace = new Dictionary<Characters, Sprite>();
            foreach(nameToFaceTuple nameAndFace in faces)
            {
                nameToFace[nameAndFace.name] = nameAndFace.face;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
