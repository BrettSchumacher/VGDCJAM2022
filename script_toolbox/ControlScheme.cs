using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ControlScheme
{
    Dictionary<InputCode, KeyCode> map;
    Dictionary<InputCode, string> string_map;

    public ControlScheme(Dictionary<InputCode, KeyCode> map)
    {
        this.map = map;
    }

    public ControlScheme(XDocument document)
    {
        map = new Dictionary<InputCode, KeyCode>();
        string_map = new Dictionary<InputCode, string>();

        XElement root = document.Root;

        foreach(XElement element in root.Elements())
		{
			string inputCodeString = element.Name.LocalName;
			InputCode inputCode = CowTools.StringToEnum<InputCode>(inputCodeString);

            string keyCodeString = element.Value;
            KeyCode keyCode = CowTools.StringToEnum<KeyCode>(keyCodeString);

            map.Add(inputCode, keyCode);
            string_map.Add(inputCode, keyCodeString);
		}
    }

    public KeyCode GetKeyCode(InputCode code){return map[code];}
    public string GetString(InputCode code){return string_map[code];}
}
