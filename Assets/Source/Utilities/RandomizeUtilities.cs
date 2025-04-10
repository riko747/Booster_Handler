using Random = System.Random;

namespace Source.Utilities
{
    public static class RandomizeUtilities
    {
        public static int GetRandomID(int min, int max)
        {
            var randomizer = new Random();
            
            return randomizer.Next(min, max);
        }
    }
}
