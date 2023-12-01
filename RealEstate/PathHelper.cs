namespace RealEstate
{
    public static class PathHelper
    {
        public static string NotFoundImg { get; set;} = "/images/notfound.png";

        public static string GetPathToImage(params string[] pathParts)
        {
            string path = "/images";
            foreach (string part in pathParts)
            {
                path += '/' + part;
            }
            return path + ".png";
        }
    }
}
