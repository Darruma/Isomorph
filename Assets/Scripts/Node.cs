using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public List<GameObject> nextNodes;
	public int value;
	public bool connected;
	public bool chainEnd;
	void Start () {
		
	}

	void Update () {
		
	}

	public GameObject getNextNode()
	{
		return nextNodes[0];
	}
	public void setNextNode(GameObject _node)
	{
		nextNodes.Add (_node);
	}

}
