using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {

	public GameObject startNode;
	public GameObject endNode;
	 LineRenderer line;
	void Start () {
		line = GetComponent<LineRenderer> ();
	}
	

	void Update () {
		if(startNode && endNode != null)
		{
		line.SetPosition (0, startNode.transform.position);
		line.SetPosition (1, endNode.transform.position);
		}
	}
	public GameObject getStartEdge()
	{
		return startNode;
	}
	public GameObject getEndNode()
	{
		return endNode;
	}

	public void setStartNode(GameObject _start)
	{
		startNode = _start;
	}
	public void setEndNode(GameObject _end)
	{
		endNode = _end;
	}
}
