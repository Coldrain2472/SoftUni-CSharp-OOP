namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            File musicFile = new Music("Linkin Park", "Hybrid Theory", 12, 100);
            File pictureFile = new Picture("Dog", 3, 100);
            StreamProgressInfo streamProgressInfo = new StreamProgressInfo(musicFile);
            streamProgressInfo.CalculateCurrentPercent();
        }
    }
}
