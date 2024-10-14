namespace ProjectExtractor
{
    public enum WorkerStates
    {
        NONE = 0,
        DATABASE_SEARCH = 1,
        DATABASE_INDEX = 2,
        EXTRACT_BATCH_PROJECT = 3,
        EXTRACT_BATCH_DETAIL = 4,
        EXTRACT_DETAIL = 5,
        EXTRACT_PROJECT = 6,
#if DEBUG
        EXTRACT_DEBUG = 255
#endif
    }
}