using System.Collections.Generic;
using UnityEngine;

namespace MyQueenMySelf.Utils
{
	[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
	public class Dialogue : ScriptableObject
	{
		[SerializeField] List<Line> dialogue;

		public List<Line> GetAllLines()
		{	
			List<Line> copy = new();
			foreach (Line line in dialogue)
			{
				copy.Add(new Line { Speaker = line.Speaker, Text = line.Text });
			}
			return copy;
		}
	}

	[System.Serializable]
	public class Line
	{
		public string Speaker;
		public string Text;
	}
}
