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
			string input_code_string = element.Name.LocalName;
			InputCode input_code = CowTools.StringToEnum<InputCode>(input_code_string);

            string key_code_string = element.Value;
            KeyCode key_code = CowTools.StringToEnum<KeyCode>(key_code_string);

            map.Add(input_code, key_code);
            string_map.Add(input_code, key_code_string);
		}
    }

    public KeyCode GetKeyCode(InputCode code){return map[code];}
    public string GetString(InputCode code){return string_map[code];}
}
