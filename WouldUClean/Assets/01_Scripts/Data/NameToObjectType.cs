using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameToObjectType : MonoBehaviour
{
	public SerializableDictionary<string, ObjectType> nameToObj = new SerializableDictionary<string, ObjectType>();

	public List<ObjectType> objects = new List<ObjectType>(); // SO 세팅 여기에 다 해주기

	private void Awake()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			nameToObj.Add(objects[i]._ObjectName, objects[i]);
		}
	}

	public ObjectType FindType(string name)
	{
		if (nameToObj.ContainsKey(name))
		{
			return nameToObj[name];
		}
		else
		{
			print("Can't Find");
			return null; 
		}
	}
}
