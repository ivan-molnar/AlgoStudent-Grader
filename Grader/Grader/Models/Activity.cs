namespace Grader.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }

        // Self calculated from here
        public int TotalPoints = 0;
        public int OwnedPoints = 0;
        public int LostPoints = 0;
        public int UndecidedPoints = 0;

        public void init()
        {
            foreach (var category in Categories)
            {
                category.init();

                TotalPoints += category.MaxPoints;

                if(category.Grades.Count == 0) { this.UndecidedPoints += category.MaxPoints; }
                else
                {
                    this.OwnedPoints += category.Grade;
                    this.LostPoints += category.MaxPoints - category.Grade;
                }
            }
        }

        public void addGrade(string name, int grade)
        {
            string actId = this.Name.Substring(0, this.Name.IndexOf(" "));
            string gradeId = name.Substring(0, name.IndexOf("-"));

            if(actId == gradeId)    // Définir does not go to its place
            {
                this.Categories
                    .Where(categ => String.Equals(categ.Name.Substring(0, categ.Name.IndexOf(" ")), name.Substring(0, name.IndexOf(" ")), StringComparison.Ordinal))
                    .ToList()
                    .ForEach(categ => categ.Grades.Add(grade));
            }
        }
    }
}
