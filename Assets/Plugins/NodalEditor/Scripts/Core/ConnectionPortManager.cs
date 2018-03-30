using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public static class PortManager
{
	private static Dictionary<string, PortDeclaration[]> nodePortDeclarations;

	/// <summary>
	/// Fetches every node connection declaration for each node type for later use
	/// </summary>
	public static void FetchNodeConnectionDeclarations()
	{
		nodePortDeclarations = new Dictionary<string, PortDeclaration[]> ();
		foreach (NodeTypeData nodeData in NodeTypes.getNodeDefinitions ())	
		{
			Type nodeType = nodeData.type;
			List<PortDeclaration> declarations = new List<PortDeclaration> ();
			// Get all declared port fields
			FieldInfo[] declaredPorts = ReflectionUtility.getFieldsOfType (nodeType, typeof(Port));
			foreach (FieldInfo portField in declaredPorts)
			{ // Get info about that port declaration using the attribute
				object[] declAttrs = portField.GetCustomAttributes(typeof(Port.PortAttribute), true);
				if (declAttrs.Length < 1)
				    continue;
				Port.PortAttribute declarationAttr = (Port.PortAttribute) declAttrs[0];
				if (declarationAttr.MatchFieldType(portField.FieldType))
				    declarations.Add(new PortDeclaration(portField, declarationAttr));
				else
				    Debug.LogError("Mismatched " + declarationAttr.GetType().Name + " for " + portField.FieldType.Name + " '" + declarationAttr.Name + "' on " + nodeData.type.Name + "!");
            }
			nodePortDeclarations.Add (nodeData.typeID, declarations.ToArray ());
		}
	}

	/// <summary>
	/// Updates all node connection ports in the given node and creates or adjusts them according to the declaration
	/// </summary>
	public static void UpdatePorts (Node node)
	{
		foreach (PortDeclaration portDecl in GetPortDeclarationEnumerator (node))
		{
			Port port = (Port)portDecl.portField.GetValue (node);
			if (port == null)
			{ // Create new port from declaration
				port = portDecl.portInfo.CreateNew (node);
				portDecl.portField.SetValue (node, port);
			}
			else 
			{ // Check port values against port declaration
				portDecl.portInfo.UpdateProperties (port);
			}
		}
	}

	/// <summary>
	/// Updates the Ports and connectionKnobs lists of the given node with all declared nodes
	/// </summary>
	public static void UpdatePortLists (Node node) 
	{
		// Triggering is enough to update the list
		IEnumerator enumerator = GetPortDeclarationEnumerator(node).GetEnumerator();
		while (enumerator.MoveNext()) { }
	}

	/// <summary>
	/// Returns the PortDeclarations for the given node type
	/// </summary>
	public static PortDeclaration[] GetPortDeclarations (string nodeTypeID) 
	{
		PortDeclaration[] portDecls;
		if (nodePortDeclarations.TryGetValue (nodeTypeID, out portDecls))
			return portDecls;
		else
			throw new ArgumentException ("Could not find node port declarations for node type '" + nodeTypeID + "'!");
	}

	/// <summary>
	/// Returns an enumerator of type PortDeclaration over all port declarations of the given node
	/// </summary>
	public static IEnumerable GetPortDeclarationEnumerator (Node node) 
	{
		List<Port> declaredPorts = new List<Port> ();
		PortDeclaration[] portDecls;
		if (nodePortDeclarations.TryGetValue (node.GetType().FullName, out portDecls))
		{
			foreach (PortDeclaration portDecl in portDecls)
			{ // Iterate over each connection port and yield it's declaration
				yield return portDecl;
				Port port = (Port)portDecl.portField.GetValue (node);
				if (port != null)
					declaredPorts.Add(port);
			}
		}
	}
}

public class PortDeclaration
{
	public FieldInfo portField;
	public Port.PortAttribute portInfo;

	public PortDeclaration (FieldInfo field, Port.PortAttribute attr) 
	{
		portField = field;
		portInfo = attr;
	}
}