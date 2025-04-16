using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using KeyResponses;
using Microsoft.Xna.Framework.Input;
public class XmlUtils{
    public static void saveMappings(Dictionary<Keys, IKeyResponse> dict, string filename){
        TextWriter tw = new StreamWriter(filename);
        XmlSerializer sr = new XmlSerializer(typeof(Dictionary<Keys, IKeyResponse>));
        sr.Serialize(tw, dict);
    }

    public static Dictionary<Keys, IKeyResponse> getMappings(string filename){
        FileStream fs = new FileStream(filename, FileMode.Open);
        XmlSerializer sr = new XmlSerializer(typeof(Dictionary<Keys, IKeyResponse>));
        return (Dictionary<Keys, IKeyResponse>)sr.Deserialize(fs);
    }
}