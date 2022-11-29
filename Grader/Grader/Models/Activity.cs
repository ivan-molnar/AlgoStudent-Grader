namespace Grader.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public int TheoricalTotal { get; set; }

        // Self calculated from here
        public int TotalPoints = 0;
        public int OwnedPoints = 0;
        public int LostPoints = 0;
        public int UndecidedPoints = 0;

        public int OwnedPerc = 0;
        public int LostPerc = 0;
        public int UndecidedPerc = 0;

        public void init()
        {
            this.TotalPoints = 0;
            this.OwnedPoints = 0;
            this.LostPoints = 0;

            foreach (var category in Categories)
            {
                category.init(this.TheoricalTotal);

                TotalPoints += category.MaxPoints;

                if(category.Grades.Count != 0)
                {
                    this.OwnedPoints += category.Grade;
                    this.LostPoints += category.MaxPoints - category.Grade;
                }
            }

            this.UndecidedPoints = this.TotalPoints - this.OwnedPoints - this.LostPoints;

            this.OwnedPerc = this.OwnedPoints * 100 / this.TotalPoints;
            this.LostPerc = this.LostPoints * 100 / this.TotalPoints;
            this.UndecidedPerc = this.UndecidedPoints * 100 / this.TotalPoints;
        }

        public void addGrade(string name, int grade)
        {
            string actId = this.Name.Substring(0, this.Name.IndexOf(" "));
            string gradeId = name.Substring(0, name.IndexOf("-"));

            if(actId == gradeId)    // Définir does not go to its place
            {
                // We substring to the identifier to avoid special characters
                this.Categories
                    .Where(categ => categ.Name.StartsWith(name.Substring(0, name.IndexOf(" "))))
                    .ToList()
                    .ForEach(categ => categ.Grades.Add(grade));
            }
        }
    }
}
