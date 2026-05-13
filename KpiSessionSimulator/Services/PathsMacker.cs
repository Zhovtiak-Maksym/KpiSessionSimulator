namespace KpiSessionSimulator.Services
{
    public static class PathsMacker
    {
        public static readonly string ProjectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

        public static readonly string DataFolder = Path.Combine(ProjectRoot, "Data");

        public static readonly string Profiles = Path.Combine(DataFolder, "profiles.json");
        public static readonly string OpQuestions = Path.Combine(DataFolder, "op_questions.json");
        public static readonly string AsdQuestions = Path.Combine(DataFolder, "asd_questions.json");
        public static readonly string MatanQuestions = Path.Combine(DataFolder, "matan_questions.json");

        public static void EnsureDataFolderExists()
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }
        }
    }
}
