namespace Grader.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public double TheoricalTotal { get; set; }

        // Self calculated from here
        public double TotalPoints = 0;
        public double OwnedPoints = 0;
        public double LostPoints = 0;
        public double UndecidedPoints = 0;
        public double UnassignedPonits = 0;

        public double OwnedPerc = 0;
        public double LostPerc = 0;
        public double UndecidedPerc = 0;
        public double UnassignedPerc = 0;

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

            this.UndecidedPoints = this.TheoricalTotal - this.OwnedPoints - this.LostPoints;

            this.OwnedPerc = this.OwnedPoints * 100 / this.TheoricalTotal;
            this.LostPerc = this.LostPoints * 100 / this.TheoricalTotal;
            this.UndecidedPerc = this.UndecidedPoints * 100 / this.TheoricalTotal;

            this.UnassignedPonits = this.TheoricalTotal - this.TotalPoints;
            this.UnassignedPerc = this.UnassignedPonits * 100 / this.TheoricalTotal;
        }

        public void addGrade(string name, double grade)
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
