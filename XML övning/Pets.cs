using System;
using System.Collections.Generic;
using System.Text;

namespace XML_övning
{
    public class Pets
    {
        public string petName;
        public string petDesc;
        public int petSpeed;
        int petLifeSpan = new LifeSpan().LifeSpanGenerator();
    }
    class LifeSpan
    {
        public int LifeSpanGenerator()
        {
            Random generator = new Random();
            int lifeSpan = generator.Next(60,4321);
            return lifeSpan;
        }
    }
}
