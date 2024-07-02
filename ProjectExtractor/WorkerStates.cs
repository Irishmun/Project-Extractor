namespace ProjectExtractor
{
    public enum WorkerStates
    {
        NONE = 0,
        DATABASE_SEARCH = 1,
        DATABASE_INDEX = 2,
        EXTRACT_BATCH = 3,
        EXTRACT_DETAIL = 4,
        EXTRACT_PROJECT = 5,
#if DEBUG
        EXTRACT_DEBUG = 255
#endif
    }
}