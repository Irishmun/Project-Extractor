using System;

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
        /// <summary>Project using the layout made before 2017 (V1.X)</summary>
        REVISION_ONE = 0,
        /// <summary>Project using the layout made after 2017 and before 2022 (V2.X)</summary>
        REVISION_TWO = 1,
        /// <summary>project using the layout made after 2022 and before 2026 (V3.X)</summary>
        REVISION_THREE = 2,
        /// <summary>project using the layout made after 2026 (V4.X)</summary>
        REVISION_FOUR = 3,
        /// <summary>project using an unknown layout</summary>
        UNKNOWN_REVISION = 99
    }
    class ProjectRevisionUtil
    {
        /// <summary>Returns a <see cref="ProjectLayoutRevision"/> equivalent to the given integer</summary>
        /// <param name="selectedIndex">A positive integer</param>
        /// <returns>A <see cref="ProjectLayoutRevision"/></returns>
        /// <remarks>negative values will also return <see cref="ProjectLayoutRevision.UNKNOWN_REVISION"/> </remarks>
        public static ProjectLayoutRevision GetProjectRevision(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return ProjectLayoutRevision.REVISION_ONE;
                case 1:
                    return ProjectLayoutRevision.REVISION_TWO;
                case 2:
                    return ProjectLayoutRevision.REVISION_THREE;
                case 3:
                    return ProjectLayoutRevision.REVISION_FOUR;
                default:
                    return ProjectLayoutRevision.UNKNOWN_REVISION;
            }
        }
    }
}
