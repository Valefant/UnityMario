using System;

namespace Assets.EventSystem
{
    public class SeedSetEvent : IEventable
    {
        private String seed;

        public SeedSetEvent(string seed)
        {
            this.seed = seed;
        }

        public String GetSeed()
        {
            return seed;
        }
        
        public string GetName()
        {
            return "SeedSetEvent";
        }
    }
}