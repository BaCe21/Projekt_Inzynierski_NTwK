namespace TrikProjekt56.Models
{
    public static class Help
    {
        public static string GetTypeName(string fullType)
        {
            string myString = "";
            try
            {
                int lastIndex = fullType.IndexOf('.') + 1;
                myString = fullType.Substring(lastIndex, fullType.Length - lastIndex);
            }
            catch
            {
                myString = fullType;
            }
            return myString;
        }
    }
}
