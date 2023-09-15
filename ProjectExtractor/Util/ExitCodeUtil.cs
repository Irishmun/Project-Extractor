namespace ProjectExtractor.Util
{
    public enum ExitCode
    {
        NONE = 0,//all went well
        ERROR = 1,//something went horibly wrong
        NOT_IMPLEMENTED = 2,//this extractor is not implemented yet
        FLAWED = 3,//it finished, but something didn't go right
        NOT_INSTALLED = 159//a required external dependency is not installed
    }

    public class ExitCodeUtil
    {
        /// <summary>Gets the message from the returncode int value</summary>
        public static string GetReturnCode(int code)
        {
            switch ((ExitCode)code)
            {
                case ExitCode.NONE:
                    return string.Empty;
                case ExitCode.ERROR:
                    return "Fatal error.";
                case ExitCode.NOT_IMPLEMENTED:
                    return "Not Implemented yet.";
                case ExitCode.NOT_INSTALLED:
                    return "Program not installed.";
                case ExitCode.FLAWED:
                    return "Non fatal error occured, kept going.";
                default:
                    return "Unknown error.";
            }
        }

        public static string GetReturnCode(ExitCode code)
        {
            return GetReturnCode((int)code);
        }
    }
}
