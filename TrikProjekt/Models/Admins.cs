namespace TrikProjekt56.Models
{
    public class Admins
    {
        public static List<string> admins = new List<string>{
            "jakub",
            "jakubtest"
        };

        public static void ChangeAdmins()
        {
            List<string> admins2 = admins;
            admins2.Add("test");
            admins = new List<string>(admins2);
        }
    }
}
