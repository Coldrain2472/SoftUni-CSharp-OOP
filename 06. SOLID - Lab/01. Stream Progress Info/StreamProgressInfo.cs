namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private File file;

        public StreamProgressInfo(File file)
        {
            this.file = file;
        }

        public int CalculateCurrentPercent()
        {
            return (this.file.BytesSent * 100) / this.file.Length;
        }
    }
}
