namespace DatabaseCleaner
{
    internal enum WorkerStates
    {
        NONE = 0,
        DATABASE_GET = 1,
        EXTRACT_PROJECTS = 2,
        GET_DUPLICATES = 3,
        CLEAN_DUPLICATES = 4,
    }
}
