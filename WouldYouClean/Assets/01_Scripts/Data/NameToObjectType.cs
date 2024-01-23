using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameToObjectType : MonoBehaviour
{
	public SerializableDictionary<string, ObjectType> nameToObj = new SerializableDictionary<string, ObjectType>();

	public List<ObjectType> objects = new List<ObjectType>(); // SO ���� ���⿡ �� ���ֱ�

	private void Awake()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			nameToObj.Add(objects[i].name, objects[i]);
		}
	}

	public ObjectType FindType(string name)
	{
		return nameToObj[name];
	}
}
