namespace BlueMirrorIndexer
{
    public class ChartPoint
    {
        public ChartPoint(long size, string title, ItemInDatabase item) {
            Size = size;
            Title = title;
            Item = item;
        }

        public long Size { get; }
        public string Title { get; }
        public ItemInDatabase Item { get; }
    }
}