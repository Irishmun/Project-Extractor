namespace ProjectExtractor.Util
{
    /*
    *NOTES ON DOCUMENT VARIATIONS:
    *2016 & 2017 are written with spaces as spacing values, (remove those)
    *2018 is written with key and value on seperate lines (use value of next array entry)
    *2022 is written as regular document, with tabs/whitespace for spacing of values (automatically removed)
    */
    public enum ProjectLayoutRevision
    {
        REVISION_ONE = 1,//Projects using the old layout, starting with a page number and using tables
        REVISION_TWO = 2,//projects using the current layout, starting with an image (which is not read from by the reader)
        UNKNOWN_REVISION = 3//new or unknown project type
    }
    class ProjectRevisionUtil
    {
        /// <summary>Tries to determine which project layout revision the given document uses. done through the first line</summary>
        public static ProjectLayoutRevision TryDetermineProjectLayout(string firstline)
        {    //TODO: Update this method to better determine project type and to determine new projects
             //it's all guesswork here, best to try and get a better way to check for the layout type (get the file's ACTUAL original creation date?)
            string firstWord = firstline.Split(" ")[0];
            bool firstLineIsInt = int.TryParse(firstWord, out int res);
            //if firstLineIsInt is true and it's the number 1, it would most likely be RevisionOne
            if (firstLineIsInt)
            {
                if (res == 1)//would be 1 as the first line should be the page number of page 1
                {
                    return ProjectLayoutRevision.REVISION_ONE;
                }
                else
                {
                    return ProjectLayoutRevision.UNKNOWN_REVISION;//using a unknown layout as of now
                }
            }
            //if it's not a number, the first line would most likely be that of the second revision, using an image at the top of the document
            return ProjectLayoutRevision.REVISION_TWO;
        }
    }
}
