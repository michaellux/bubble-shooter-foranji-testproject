using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Test
{
    internal class FakeDataGenerators
    {
        public void Generate()
        {
            PlayField.BubbleRow[] bubbleRows = new[]
      {   //1
            new PlayField.BubbleRow(new PlayField.GoalBubble[] {
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
            }),
            //2
            new PlayField.BubbleRow(new PlayField.GoalBubble[] {
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
            }),
            //3
            new PlayField.BubbleRow(new PlayField.GoalBubble[] {
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),
                new PlayField.GoalBubble(new Vector3(3, 4, 5), PlayField.GoalBubbleType.GREEN, true),

            }),
        };

            PlayField.BubbleField bubbleField = new PlayField.BubbleField(bubbleRows);

            PlayField playField = new PlayField(bubbleField);

            Debug.Log(playField);

            string json = JsonConvert.SerializeObject(playField);
            File.WriteAllText(Application.dataPath + "/gamedata.json", json);
        }
    }
}
