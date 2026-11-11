using System;
using System.Collections.Generic;

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
        /// <summary>project using the layout made after september 2026 (V4.5)</summary>
        REVISION_FOUR_HALF = 4,
        /// <summary>project using the layout made after 2027 (V5)</summary>
        REVISION_FIVE = 5,
        /// <summary>project using an unknown layout</summary>
        UNKNOWN_REVISION = 999
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
                case 4:
                    return ProjectLayoutRevision.REVISION_FOUR_HALF;
                case 5:
                    return ProjectLayoutRevision.REVISION_FIVE;
                default:
                    return ProjectLayoutRevision.UNKNOWN_REVISION;
            }
        }

        public static string[] GetProjectRevisionList() => new string[]{
                "Version 1.0 [01/2016 - 10/2017]",
                "Version 2.0 [01/2018 - 08/2021]",
                "Version 3.0 [09/2021 - 12/2025]",
                "Version 4.0 [01/2026 - 09/2026]",
                "Version 4.5 [09/2026 - 12/2026]",
                "Version 5.0 [01/2027 - ...]"
            };
    }
}
