using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public enum LINEREADERS {Narrirator, Grandma, Cat, Dog, Tree, Rock }

    [System.Serializable]
    public class StoryLine
    {
        [SerializeField] private LINEREADERS lineReader;
        [SerializeField] private string line;
        [SerializeField] private float timeToRead;

        public LINEREADERS getReader()
        {
            return lineReader;
        }

        public void changeLine(string newLineIn)
        {
            line = newLineIn;
        }

        public string getLine()
        {
            return line;
        }

        public float getTimeToRead()
        {
            return timeToRead;
        }
    }
}