using Microsoft.EntityFrameworkCore;


namespace LibraryManager.Data.Utils
{
    public static class DatabaseFunctions
    {
        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public static string FuzzySearch(string queryString)
        {
            throw new NotImplementedException();
        }
    }
}
